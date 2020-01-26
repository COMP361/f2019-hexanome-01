using System.Collections.Generic;

public class MoveCommand : ICommand {
    private readonly Token token;
    private readonly Cell origin;
    private Cell goal;
    private Path path;

    // Movable ?
    public MoveCommand(Token token, Cell origin) {
        EventManager.CellClick += SetDestination;
        EventManager.MoveConfirm += Execute;
        
        this.token = token;
        this.origin = origin;
    }

    public void Dispose() {
        EventManager.CellClick -= SetDestination;
        EventManager.MoveConfirm -= Execute;
    }

    void SetDestination(int cellID) {
        goal = Cell.FromId(cellID);
        if(path != null) path.Dispose();
        path = new Path(origin, goal);
    }

    public void Execute() {
        token.Move(path.Cells);

        // Hide path
        /*if(gm.CurrentPlayer.Token.IsDone) {
            gm.CurrentPlayer.IsDone = true;
            gm.ui.HidePath();
        }*/
    }
}
