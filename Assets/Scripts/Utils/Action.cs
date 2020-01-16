public class Action : Enumeration
{
    private static int CurrentId = 0;
    public static readonly Action None = new Action("None");
    public static readonly SubAction Move = new SubAction(CurrentId++, "Move");
    public static readonly Action MoveThorald = new Action(CurrentId++, "Move Thorald");
    public static readonly Action Fight = new Action("Fight");
    public static readonly Action Skip = new Action("Skip");

    public Action() { }
    public Action(string name) : base(CurrentId++, name) { 
    }
    protected Action(int value, string name) : base(value, name) { 
    }

    //public abstract decimal BonusSize { get; }
}

public class SubAction : Action {
    private static int CurrentId = 0;
    public SubAction SetDest;
    public SubAction AddStop;
    public SubAction FreeMove;

    private SubAction(string name) : base(CurrentId++, name) { }
    public SubAction(int value, string name) : base(value, name) {
        SetDest = new SubAction("SetDest");
        AddStop = new SubAction("AddStop");
        FreeMove = new SubAction("FreeMove");
    }

    /*public override decimal BonusSize 
    {
        get { return 1000m; }
    }*/
}