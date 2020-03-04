using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pair<T1, T2> {
    public T1 First { get; set; }
    public T2 Second { get; set; }

    public Pair(T1 first, T2 second) {
        First = first;
        Second = Second;
    }
}

public class MoveCommand : MonoBehaviour, ICommand {
    public enum Action {
        DetachFarmer,
        SetDestination
    }
    
    private Hero hero;
    private Cell origin;
    private Cell goal;
    private MapPath path;
    private List<Cell> freeCells;
    private List<Cell> extCells;
    private List<Pair<Farmer, Cell>> farmers;
    private Timeline timeline;
    private List<Cell> stops;
    Action action;
    List<GameObject> farmerTargets;
    public PhotonView photonView;

    // Movable ?
    public void Init(Hero hero) {
        origin = hero.Cell;
        farmers = new List<Pair<Farmer, Cell>>();
        action = Action.SetDestination;
        farmerTargets = new List<GameObject>();

        EventManager.CellClick += SetDestination;
        EventManager.CellClick += SetFarmerDestination;
        EventManager.MoveCancel += Dispose;
        EventManager.MoveComplete += MoveComplete;
        EventManager.PickFarmer += AttachFarmer;
        EventManager.DropFarmer += DetachFarmer;
        EventManager.FarmerDestroyed += FarmerDestroyed;
        EventManager.ClearPath += ClearPath;

        this.hero = hero;
        Reset();
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());  
    }
    
    void AddFarmerTarget(Cell c) {
        GameObject go = new GameObject("farmerTarget");
        Sprite sprite = Resources.Load<Sprite>("Sprites/icons/farmer-target");    
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        go.transform.parent = GameObject.Find("Tokens").transform;
        go.transform.position = c.MovablesPosition;
        farmerTargets.Add(go);
    }

    private void Reset() {
        origin = hero.Cell;
        goal = origin;
        path = new MapPath(origin, Color.red);
        timeline = (Timeline)hero.State.timeline.Clone();
        //ShowMovableArea();
        
        foreach(Pair<Farmer, Cell> farmer in farmers) {
            farmer.First.Detach();
        } 

        farmers = new List<Pair<Farmer, Cell>>();
        action = Action.SetDestination;

        foreach(GameObject go in farmerTargets) {
           GameObject.Destroy(go);
        }

        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }

        farmerTargets = new List<GameObject>();
    }

    private void ShowMovableArea() {
        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }
        
        freeCells = goal.WithinRange(0, timeline.GetFreeHours());
        
        int min = timeline.GetFreeHours() + 1;
        int max = timeline.GetFreeHours() + timeline.GetExtendedHours();
        extCells = goal.WithinRange(min, max);

        foreach (Cell cell in Cell.cells) {
            cell.Deactivate();
        }

        foreach (Cell cell in freeCells) {
            cell.Reset();
        }

        foreach (Cell cell in extCells) {
            cell.Extended();
        }
    }

    private void ShowDropableArea() {
        Farmer farmer = null;
        foreach(Pair<Farmer, Cell> f in farmers) {
            if(f.Second == null) {
                farmer = f.First;
                break;
            }
        }
        if(farmer == null) return;

        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }
        
        foreach (Cell cell in Cell.cells) {
            cell.Deactivate();
        }

        int i = path.Cells.IndexOf(farmer.Cell);
        List<Cell> subpath = path.Cells.GetRange(i, path.Cells.Count - i);
        foreach (Cell cell in subpath) {
            cell.Reset();
        }
    }

    private int GetDroppableFarmerCount() {
        int count = 0;
        foreach(Pair<Farmer, Cell> farmer in farmers) {
            if(farmer.Second == null) {
                count++;
            }
        }

        return count;
    }

    private int GetDetachedFarmerCount() {
        int count = 0;
        foreach(Farmer pathFarmer in GetPathFarmer()) {
            bool found = false;

            foreach(Pair<Farmer, Cell> farmer in farmers) {
                if(farmer.First == pathFarmer) {
                    found = true;
                    break;
                }
            }

            if(!found) count++;
        }
        return count;
    }

    private List<Farmer> GetPathFarmer() {
        List<Farmer> pathFarmers = new List<Farmer>(); 
        foreach(Cell cell in path.Cells) {
            foreach(Farmer farmer in cell.State.cellInventory.Farmers) {
                pathFarmers.Add(farmer);
            }
        }
        return pathFarmers;
    }

    public void AttachFarmer() {
        foreach(Farmer pathFarmer in GetPathFarmer()) {
            bool found = false;

            foreach(Pair<Farmer, Cell> farmer in farmers) {
                if(farmer.First == pathFarmer) {
                    found = true;
                    break;
                }
            }

            if(!found) {
                pathFarmer.Attach();
                farmers.Add(new Pair<Farmer, Cell>(pathFarmer, null));
                break;
            }
        }

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    public void DetachFarmer() {
        action = Action.DetachFarmer;
        if(farmers.Count == 0) return;
        ShowDropableArea();        
    }

    void SetFarmerDestination(int cellID) {
        if(action == Action.SetDestination) return;

        int index = -1;
        for(int i = 0; i < farmers.Count; i++) {
            Pair<Farmer, Cell> farmer = farmers[i];
            if(farmer.Second == null) {
                index = i;
                break;
            }
        }
        if(index == -1) return;

        if(cellID == farmers[index].First.Cell.Index) {
            farmers[index].First.Detach();
            farmers.RemoveAt(0);
        } else {
            Cell c = Cell.FromId(cellID);
            farmers[index].Second = c;
            AddFarmerTarget(c);
        }

        ShowMovableArea();

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }
    
    public void FarmerDestroyed(Farmer f) {
        foreach(Pair<Farmer, Cell> farmer in farmers) {
            if(farmer.First == f) {
                farmers.Remove(farmer);
                break;
            }
        }
        
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        EventManager.MoveCancel -= Dispose;
        EventManager.MoveComplete -= MoveComplete;
        EventManager.PickFarmer -= AttachFarmer;
        EventManager.DropFarmer -= DetachFarmer;
        EventManager.FarmerDestroyed -= FarmerDestroyed;
        EventManager.ClearPath -= ClearPath;

        ClearPath();
    }

    public void ClearPath() {
        if(path != null) path.Dispose();
        Reset();
        EventManager.TriggerPathUpdate(0);
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
    }

    [PunRPC]
    void SetDestination(int cellID) {
        if(action == Action.DetachFarmer) return;

        goal = Cell.FromId(cellID);
        path.Extend(goal);

        // Path contains source cell so substract it
        timeline.Index += path.Cells.Count - 1;
        ShowMovableArea();

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, GetDroppableFarmerCount(), GetDetachedFarmerCount());
        EventManager.TriggerPathUpdate(path.Cells.Count);
    }

    public void Execute() {
        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }

        if(path.Cells == null) return;

        stops = new List<Cell>();

        if(farmers.Count > 0) {
            foreach(Pair<Farmer, Cell> farmer in farmers) {
                stops.Add(farmer.First.Cell);
            }   
        }

        stops.Add(goal);
        MoveComplete(hero);
    }

    private void MoveComplete(Movable movable) {
        if(movable != hero) return;

        if(stops.Count == 0) {
            Dispose();
        } else {
            Cell stop = stops[0];
            stops.RemoveAt(0);
            Cell start = hero.Cell;

            int startIndex = path.Cells.IndexOf(start);
            foreach(Farmer f in start.State.cellInventory.Farmers) {
                foreach(Pair<Farmer, Cell> farmer in farmers) {
                    if(farmer.First == f) {
                        int stopInd = -1;
                        if(farmer.Second != null) {
                            stopInd = path.Cells.IndexOf(farmer.Second);
                        }

                        if(stopInd == -1) {
                            stopInd = path.Cells.Count - 1;
                        }

                        List<Cell> subpathFarmer = path.Cells.GetRange(startIndex, stopInd - startIndex + 1);
                        f.Move(subpathFarmer);
                        break;
                    }
                }
            }

            int stopIndex = path.Cells.IndexOf(stop);
            List<Cell> subpathHero = path.Cells.GetRange(startIndex, stopIndex - startIndex + 1);
            hero.Move(subpathHero);
        }
    }
}
