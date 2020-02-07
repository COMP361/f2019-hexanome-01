using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Movable, IComparable<Enemy>
{
    public int CompareTo(Enemy monster)
    {
        return cell.CompareTo(monster.cell);
    }

    protected int Will { get; set; }

    protected int Strength { get; set; }

    protected int Reward { get; set; }
}
