using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}

public class MoveCommand : MonoBehaviour, ICommand
{
    public enum MoveAction
    {
        DetachFarmer,
        SetDestination
    }
    private int totalFreeMoves;
    private Movable movable;
    private Cell goal;
    private MapPath path;
    private List<Cell> freeCells;

    private List<Cell> extCells;
    private List<Pair<Farmer, Cell>> farmers;
    private int moveCost;
    private List<Cell> stops;
    MoveAction action;
    List<GameObject> farmerTargets;
    private List<Token> freeMoves;
    public PhotonView photonView;

    public int initFreeHours;
    public int FreeHours {
        get {
            return Math.Max(0, initFreeHours - path.Cells.Count + 1 + totalFreeMoves);
        }
    }

    public int initExtHours;
    public int ExtHours {
        get {
            return Math.Max(0, initExtHours + Math.Min(0, initFreeHours + totalFreeMoves - path.Cells.Count + 1));
        }
    }

    public void Init(Movable movable)
    {
        totalFreeMoves = 0;
        farmers = new List<Pair<Farmer, Cell>>();
        action = MoveAction.SetDestination;
        farmerTargets = new List<GameObject>();
        freeMoves = new List<Token>();
        this.movable = movable;
        initFreeHours = movable.MovePerHour * Timeline.GetFreeHours(GameManager.instance.CurrentPlayer.timeline.Index);
        initExtHours = movable.MovePerHour * Timeline.GetExtendedHours(
            GameManager.instance.CurrentPlayer.timeline.Index,
            GameManager.instance.CurrentPlayer.Willpower
        );

        EventManager.CellClick += SetDestination;
        EventManager.CellClick += SetFarmerDestination;
        EventManager.ActionUpdate += Dispose;
        EventManager.MoveComplete += MoveComplete;
        EventManager.PickFarmer += AttachFarmer;
        EventManager.DropFarmer += DetachFarmer;
        EventManager.FarmerDestroyed += FarmerDestroyed;
        EventManager.ClearPath += ClearPath;
        EventManager.FreeMoveCount += AddFreeMoveCount;
        EventManager.ClearFreeMove += ResetFreeMove;

        Reset();
        ShowMovableArea();
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    public bool canMove(){
      return (totalFreeMoves < path.Cells.Count);
    }

    void AddFarmerTarget(Cell c)
    {
        GameObject go = new GameObject("farmerTarget");
        Sprite sprite = Resources.Load<Sprite>("Sprites/icons/farmer-target");
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        go.transform.parent = GameObject.Find("Tokens").transform;
        go.transform.position = c.MovablesPosition;
        farmerTargets.Add(go);
    }

    void Reset()
    {
        goal = movable.Cell;
        path = new MapPath(movable.Cell, Color.red);

        foreach (Pair<Farmer, Cell> farmer in farmers)
        {
            farmer.First.Detach();
        }

        farmers = new List<Pair<Farmer, Cell>>();
        action = MoveAction.SetDestination;

        foreach (GameObject go in farmerTargets)
        {
            GameObject.Destroy(go);
        }

        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
        }

        farmerTargets = new List<GameObject>();
    }

    void ShowMovableArea()
    {
        freeCells = goal.WithinRange(0, FreeHours);
        extCells = goal.WithinRange(FreeHours + 1, FreeHours + ExtHours);

        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
            cell.Disable();
        }

        foreach (Cell cell in freeCells)
        {
            cell.Reset();
        }

        foreach (Cell cell in extCells)
        {
            cell.Extended();
        }

        goal.Disable();
    }

    void ShowDropableArea()
    {
        Farmer farmer = null;
        foreach (Pair<Farmer, Cell> f in farmers)
        {
            if (f.Second == null)
            {
                farmer = f.First;
                break;
            }
        }
        if (farmer == null) return;

        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
        }

        foreach (Cell cell in Cell.cells)
        {
            cell.Disable();
        }

        int i = path.Cells.IndexOf(farmer.Cell);
        if(i != -1) {
            List<Cell> subpath = path.Cells.GetRange(i, path.Cells.Count - i);
            foreach (Cell cell in subpath) {
                cell.Reset();
            }
        }
    }

    int GetDroppableFarmerCount()
    {
        int count = 0;
        foreach (Pair<Farmer, Cell> farmer in farmers)
        {
            if (farmer.Second == null)
            {
                count++;
            }
        }

        return count;
    }

    int GetDetachedFarmerCount()
    {
        int count = 0;
        foreach (Farmer pathFarmer in GetPathFarmer())
        {
            bool found = false;

            foreach (Pair<Farmer, Cell> farmer in farmers)
            {
                if (farmer.First == pathFarmer)
                {
                    found = true;
                    break;
                }
            }

            if (!found) count++;
        }
        return count;
    }

    List<Farmer> GetPathFarmer()
    {
        List<Farmer> pathFarmers = new List<Farmer>();
        foreach (Cell cell in path.Cells)
        {
            foreach (Farmer farmer in cell.Inventory.Farmers)
            {
                pathFarmers.Add(farmer);
            }
        }
        return pathFarmers;
    }

    void AttachFarmer()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("AttachFarmerRPC", RpcTarget.AllViaServer);
        }
        else
        {
            AttachFarmerRPC();
        }
    }

    [PunRPC]
    void AttachFarmerRPC()
    {
        foreach (Farmer pathFarmer in GetPathFarmer())
        {
            bool found = false;

            foreach (Pair<Farmer, Cell> farmer in farmers)
            {
                if (farmer.First == pathFarmer)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                pathFarmer.Attach();
                farmers.Add(new Pair<Farmer, Cell>(pathFarmer, null));
                break;
            }
        }

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    void DetachFarmer()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("DetachFarmerRPC", RpcTarget.AllViaServer);
        }
        else
        {
            DetachFarmerRPC();
        }
    }

    [PunRPC]
    void DetachFarmerRPC()
    {
        action = MoveAction.DetachFarmer;
        if (farmers.Count == 0) return;
        ShowDropableArea();
    }


    void SetFarmerDestination(int cellID, Hero hero)
    {
        if (action == MoveAction.SetDestination || hero != GameManager.instance.CurrentPlayer) return;

        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("SetFarmerDestinationRPC", RpcTarget.AllViaServer, cellID);
        }
        else
        {
            SetFarmerDestinationRPC(cellID);
        }
    }

    [PunRPC]
    void SetFarmerDestinationRPC(int cellID)
    {
        int index = -1;
        for (int i = 0; i < farmers.Count; i++)
        {
            Pair<Farmer, Cell> farmer = farmers[i];
            if (farmer.Second == null)
            {
                index = i;
                break;
            }
        }
        if (index == -1) return;

        if (cellID == farmers[index].First.Cell.Index)
        {
            farmers[index].First.Detach();
            farmers.RemoveAt(index);
        }
        else
        {
            Cell c = Cell.FromId(cellID);
            farmers[index].Second = c;
            AddFarmerTarget(c);
        }

        ShowMovableArea();

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    void FarmerDestroyed(Farmer f)
    {
        foreach (Pair<Farmer, Cell> farmer in farmers)
        {
            if (farmer.First == f)
            {
                farmers.Remove(farmer);
                break;
            }
        }

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    public void Dispose(int action)
    {
        if(Action.FromValue<Action>(action) == Action.None) {
            Dispose();
        }
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        EventManager.CellClick -= SetFarmerDestination;
        EventManager.ActionUpdate -= Dispose;
        EventManager.MoveComplete -= MoveComplete;
        EventManager.PickFarmer -= AttachFarmer;
        EventManager.DropFarmer -= DetachFarmer;
        EventManager.FarmerDestroyed -= FarmerDestroyed;
        EventManager.ClearPath -= ClearPath;
        EventManager.EndDay -= Dispose;
        EventManager.FreeMoveCount -= AddFreeMoveCount;

        ResetFreeMove();
        ClearPath();
        Reset();
        Destroy(this.gameObject);
    }

    void ClearPath()
    {
        if (path != null) path.Dispose();
        Reset();
        ShowMovableArea();
        EventManager.TriggerPathUpdate(0);
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    void SetDestination(int cellID, Hero hero)
    {
        if(hero != GameManager.instance.CurrentPlayer) return;

        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("SetDestinationRPC", RpcTarget.AllViaServer, cellID);
        }
        else
        {
            SetDestinationRPC(cellID);
        }
    }

    [PunRPC]
    void SetDestinationRPC(int cellID)
    {
      if (action == MoveAction.DetachFarmer) return;

      goal = Cell.FromId(cellID);
      path.Extend(goal);
      ShowMovableArea();

      EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
      EventManager.TriggerPathUpdate(path.Cells.Count);
    }

    void UpdateFreeMoves() {
        totalFreeMoves = 0;
        foreach(Token token in freeMoves) {
            totalFreeMoves += token.reserved;
        }
        EventManager.TriggerDisplayFreeMoves(totalFreeMoves);
        ShowMovableArea();
    }

    public void AddFreeMoveCount(Token item){
      if(!freeMoves.Contains(item)) freeMoves.Add(item);
      UpdateFreeMoves();
    }

    public void Execute()
    {
      foreach(Token token in freeMoves) {
        if (token is HalfWineskin && token.reserved == 1){
            GameManager.instance.MainHero.heroInventory.RemoveSmallToken((SmallToken)token);
        } else if(token is Wineskin && token.reserved == 2){
            GameManager.instance.MainHero.heroInventory.RemoveSmallToken((SmallToken)token);
        } else if(token is Wineskin && token.reserved == 1){
            SmallToken halfWineskin = HalfWineskin.Factory();
            GameManager.instance.MainHero.heroInventory.ReplaceSmallToken((SmallToken)token, halfWineskin, true);
        } else if(token is Herb && token.reserved != 0){
            GameManager.instance.MainHero.heroInventory.RemoveSmallToken((SmallToken)token);
        }
      }

      freeMoves = new List<Token>();

      if (!PhotonNetwork.OfflineMode) {
          photonView.RPC("ExecuteRPC", RpcTarget.AllViaServer, new object[] {totalFreeMoves});
      } else {
          ExecuteRPC(totalFreeMoves);
      }
    }

    [PunRPC]
    void ExecuteRPC(int amtFreeMoves)
    {
        this.totalFreeMoves = amtFreeMoves;
        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
        }

        if (path.Cells == null) return;

        stops = new List<Cell>();

        if (farmers.Count > 0)
        {
            foreach (Pair<Farmer, Cell> farmer in farmers)
            {
                stops.Add(farmer.First.Cell);
            }
        }

        stops.Add(goal);
        MoveComplete(movable);
    }

    void MoveComplete(Movable movable)
    {
        if (movable != this.movable) return;

        if (stops.Count == 0)
        {
            Dispose();
        }
        else
        {
            Cell stop = stops[0];
            stops.RemoveAt(0);
            Cell start = movable.Cell;

            int startIndex = path.Cells.IndexOf(start);
            if(startIndex != -1) {
                foreach (Farmer f in start.Inventory.Farmers)
                {
                    foreach (Pair<Farmer, Cell> farmer in farmers)
                    {
                        if (farmer.First == f)
                        {
                            int stopInd = -1;
                            if (farmer.Second != null)
                            {
                                stopInd = path.Cells.IndexOf(farmer.Second);
                            }

                            if (stopInd == -1)
                            {
                                stopInd = path.Cells.Count - 1;
                            }

                            List<Cell> subpathFarmer = path.Cells.GetRange(startIndex, stopInd - startIndex + 1);
                            f.Move(subpathFarmer);
                            break;
                        }
                    }
                }

                int stopIndex = path.Cells.IndexOf(stop, startIndex + 1);
                if(stopIndex != -1) {
                    List<Cell> subpathHero = path.Cells.GetRange(startIndex, stopIndex - startIndex + 1);
                    movable.Move(subpathHero, totalFreeMoves);
                } else {
                    Dispose();
                }
            }
        }
    }

    void ResetFreeMove(){
      foreach(Token token in freeMoves) {
        token.reserved = 0;
      }

      freeMoves.Clear();
    }
}
