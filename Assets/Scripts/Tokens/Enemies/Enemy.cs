using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Movable, IComparable<Enemy>
{
    public int CompareTo(Enemy monster)
    {
        return Cell.CompareTo(monster.Cell);
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }
}
