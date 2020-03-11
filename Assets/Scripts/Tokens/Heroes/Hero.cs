using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Sex
{
    Female,
    Male
}

[RequireComponent(typeof(HeroInventory))]

public class Hero : Movable
{
    protected Sex sex = Sex.Female;

    protected string[] names;
    protected int rank;

    //void OnTokenMoveComplete(Token token, Cell c) {
    //  State.cell = c;
    //}

    public string Type { get; protected set; }

    public HeroState State { get; set; }

    public string HeroName
    {
        get
        {
            return names[(int)sex];
        }
    }

    public int[] Dices { get; protected set; }

    public Action Action
    {
        get
        {
            return State.action;
        }
    }

    public Color Color { get; set; }

    protected override Vector3 getWaypoint(Cell cell)
    {
        return cell.HeroesPosition;
    }
}

public class HeroState : ICloneable
{
    public Action action;
    public Cell cell;
    public TimeOfDay TimeOfDay;
    public HeroInventory heroInventory;

    private int freeMove;
    public int Strength { get; set; }
    public int Willpower { get; set; }

    public HeroState(Cell cell, Color color, string heroName, string parentHero)
    {
        this.cell = cell;
        action = Action.None;
        TimeOfDay = new TimeOfDay(color, heroName);
        heroInventory = new HeroInventory(parentHero);
        Strength = 1;
        Willpower = 7;
    }

    public object Clone()
    {
        HeroState hs = (HeroState)this.MemberwiseClone();
        hs.TimeOfDay = (TimeOfDay)TimeOfDay.Clone();
        return hs;
    }

    public void resetTimeOfDay()
    {
        TimeOfDay.reset();
    }

    public void decrementWP(int points)
    {
        if (points >= Willpower) Willpower = 0; else Willpower -= points;
    }
}
