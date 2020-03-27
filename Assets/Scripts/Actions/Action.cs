using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action : Enumeration {
    private static int CurrentId = 0;
    public static readonly SubAction None = new SubAction(CurrentId++, "None");
    public static readonly SubAction Move = new SubAction(CurrentId++, "Move");
    public static readonly Action MoveThorald = new Action(CurrentId++, "Move Thorald");
    public static readonly Action Fight = new Action("Fight");
    public static readonly Action Skip = new Action("Skip");
    
    public Action() { 
    }

    public Action(string name) : base(CurrentId++, name) { 
    }
    
    protected Action(int value, string name) : base(value, name) { 
    }

    public int GetCost(int qty = 1) {
        if(this == Action.Skip) return 1;
        if(this == Action.Fight) return 1;
        if(this == Action.Move) return GameManager.instance.CurrentPlayer.GetMoveCost(qty);
        if(this == Action.MoveThorald) return Thorald.Instance.GetMoveCost(qty);
        return 0;
    }
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
}