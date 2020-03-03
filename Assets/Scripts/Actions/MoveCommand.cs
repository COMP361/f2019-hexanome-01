using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand {
    private readonly Movable movable;
    private Cell goal;
    private MapPath path;
    private List<Cell> freeCells;
    private List<Cell> extCells;
    private List<Farmer> farmers = new List<Farmer>();

    // Movable ?
    public MoveCommand(Movable movable) {
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

    public void AttachFarmer() {
        if(movable.Cell.State.cellInventory.Farmers.Count > 0) {
            foreach(Farmer farmer in movable.Cell.State.cellInventory.Farmers) {
                if(!farmers.Contains(farmer)) farmers.Add(farmer);
                break;
            }
        }
        
        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, movable.Cell.State.cellInventory.Farmers.Count);
    }

    public void DetachFarmer() {
        if(farmers.Count > 0) {
            farmers.RemoveAt(0);
        }

        EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, movable.Cell.State.cellInventory.Farmers.Count);
    }

    public void FarmersInventoriesUpdate(Movable movable) {
        // Check if the move callback is from us
        if(movable == this.movable) {
            EventManager.TriggerFarmersInventoriesUpdate(farmers.Count, movable.Cell.State.cellInventory.Farmers.Count);
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
