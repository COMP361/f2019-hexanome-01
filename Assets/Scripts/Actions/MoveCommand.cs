using System.Collections.Generic;

public class MoveCommand : ICommand {
    private readonly Movable movable;
    private Cell goal;
    private MapPath path;
    private List<Cell> reachableCells;

    // Movable ?
    public MoveCommand(Movable movable) {
        EventManager.CellClick += SetDestination;
        this.movable = movable;

        //reachableCells = 
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        if(path != null) path.Dispose();
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

