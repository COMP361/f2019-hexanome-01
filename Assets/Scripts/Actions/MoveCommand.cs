using System.Collections.Generic;

public class MoveCommand : ICommand {
    private readonly Movable movable;
    private readonly Cell origin;
    private Cell goal;
<<<<<<< HEAD
    private Path path;
=======
    private MapPath path;
>>>>>>> Game-Network
    private List<Cell> reachableCells;

    // Movable ?
    public MoveCommand(Movable movable, Cell origin) {
        EventManager.CellClick += SetDestination;
<<<<<<< HEAD
        this.token = token;
=======
        this.movable = movable;
>>>>>>> Game-Network
        this.origin = origin;

        //reachableCells = 
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        if(path != null) path.Dispose();
    }

    void SetDestination(int cellID) {
        goal = Cell.FromId(cellID);
        if(path != null) path.Dispose();
        path = new MapPath(origin, goal);
    }

    public void Execute() {
        if(path.Cells == null) return;
<<<<<<< HEAD
        token.Move(new List<Cell>(path.Cells));
=======
        movable.Move(new List<Cell>(path.Cells));
>>>>>>> Game-Network
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
