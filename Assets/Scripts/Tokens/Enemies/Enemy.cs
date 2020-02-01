using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Movable {
    
    protected int Will { get; set; }

    protected int Strength { get; set; }

    protected int Reward { get; set; }
}
