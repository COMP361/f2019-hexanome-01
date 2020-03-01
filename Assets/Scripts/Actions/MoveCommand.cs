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
        EventManager.MoveCancel += Dispose;
        EventManager.MoveComplete += IsFarmerOnCell;
        EventManager.PickFarmer += AttachFarmer;
        

        this.movable = movable;
        IsFarmerOnCell(movable);

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
        if(movable.Cell.State.Farmers.Count > 0) {
            foreach(Farmer farmer in movable.Cell.State.Farmers) {
                if(!farmers.Contains(farmer)) farmers.Add(farmer);
                break;
            }
        }

        Debug.Log(farmers);
    }

    public void IsFarmerOnCell(Movable movable) {
        if(movable == this.movable) {
            if(movable.Cell.State.Farmers.Count > 0) {
                EventManager.TriggerFarmerOnCell();
            }
        }
    }

    public void Dispose() {
        EventManager.MoveCancel -= Dispose;
        EventManager.MoveComplete -= IsFarmerOnCell;
        EventManager.PickFarmer -= AttachFarmer;

        if(path != null) path.Dispose();

        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }
    }

    public void SetDestination(int cellID) {
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

