using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : MonoBehaviour, ICommand {
    private Movable movable;
    private Cell goal;
    private MapPath path;
    private List<Cell> freeCells;
    private List<Cell> extCells;
    private List<Farmer> farmers = new List<Farmer>();
    public PhotonView photonView;

    // Movable ?
    public void Init(Movable movable) {
        EventManager.CellClick += SetDestination;
        EventManager.MoveCancel += Dispose;
        EventManager.MoveComplete += FarmersInventoriesUpdate;
        EventManager.PickFarmer += AttachFarmer;
        EventManager.DropFarmer += DetachFarmer;

        this.movable = movable;
        FarmersInventoriesUpdate(movable);

        freeCells = movable.Cell.WithinRange(0, 2);
        extCells = movable.Cell.WithinRange(3, 5); 
        
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

    public int getDetachedFarmerCount() {
        int count = 0;
        Debug.Log("At cell " + movable.Cell.Index);
        foreach(Farmer farmer in movable.Cell.State.Farmers) {
            if(!farmers.Contains(farmer)) {
                count++;
                Debug.Log("Not contained in cell");
            }
        }
        return count;
    }

    public void AttachFarmer() {
        if(movable.Cell.State.Farmers.Count > 0) {
            foreach(Farmer farmer in movable.Cell.State.Farmers) {
                if(!farmers.Contains(farmer)) {
                    farmers.Add(farmer);
                    break;
                }
            }
        }
        
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, getDetachedFarmerCount());
    }

    public void DetachFarmer() {
        if(farmers.Count > 0) {
            farmers.RemoveAt(0);
        }

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, getDetachedFarmerCount());
    }

    /*public bool IsDetachedFarmerOnCell() {
        if(movable.Cell.State.Farmers.Count > 0) {
            foreach(Farmer farmer in movable.Cell.State.Farmers) {
                if(!farmers.Contains(farmer)) return true;
            }
        }
        return false;
    }

    public void IsFarmerOnCell(Movable movable) {
        // Check if the move callback is from us
        if(movable == this.movable) {
            if(movable.Cell.State.Farmers.Count > 0) {
                //EventManager.TriggerFarmerOnCell();
                return;
            }
        }
    }*/

    public void FarmersInventoriesUpdate(Movable movable) {
        // Check if the move callback is from us
        if(movable == this.movable) {
            EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, getDetachedFarmerCount());
        }
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        EventManager.MoveCancel -= Dispose;
        EventManager.MoveComplete -= FarmersInventoriesUpdate;
        EventManager.PickFarmer -= AttachFarmer;
        EventManager.DropFarmer -= DetachFarmer;

        if(path != null) path.Dispose();

        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }
    }

    void SetDestination(int cellID) {
        photonView.RPC("ReceiveSetDestination", RpcTarget.AllBuffered, cellID);
    }
    
    [PunRPC]
    void ReceiveSetDestination(int cellID)
    {
        Debug.Log("Execute Set Destination Reached");
        goal = Cell.FromId(cellID);
        if(path != null) path.Dispose();
        path = new MapPath(movable.Cell, goal);
    }

    public void Execute() {
        if(path.Cells == null) return;
        movable.Move(new List<Cell>(path.Cells));
        foreach(Farmer farmer in farmers) {
            farmer.Move(new List<Cell>(path.Cells));
        }

        path.Cells = null;

        // TODO Update origin
        // Lock while moving ()
        // Hide path
        /*if(gm.CurrentPlayer.Token.IsDone) {
            gm.CurrentPlayer.IsDone = true;
            gm.ui.HidePath();
        }*/
    }
}

