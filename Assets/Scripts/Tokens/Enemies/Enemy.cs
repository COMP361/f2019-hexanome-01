﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public abstract class Enemy : Movable, IComparable<Enemy>
{
    public int CompareTo(Enemy monster)
    {
        return cell.CompareTo(monster.cell);
    }

    protected int Will { get; set; }
=======
public abstract class Enemy : Movable, IComparable<Enemy>
{
    public int CompareTo(Enemy monster)
    {
        return cell.CompareTo(monster.cell);
    }

    public int Will { get; set; }
>>>>>>> d5da44c... working attack

    public int Strength { get; set; }

    public int Reward { get; set; }
}
