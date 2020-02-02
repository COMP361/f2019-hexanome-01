using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand {
    private readonly Movable movable;
    private Cell goal;
    private MapPath path;
    private List<Cell> freeCells;
    private List<Cell> extCells;

    // Movable ?
    public MoveCommand(Movable movable) {
        EventManager.CellClick += SetDestination;
        EventManager.MoveCancel += Dispose;

        this.movable = movable;

        freeCells = movable.Cell.WithinRange(0, 2);
        extCells = movable.Cell.WithinRange(3, 4); 
        
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

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        EventManager.MoveCancel -= Dispose;

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

