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
    private int willpower = 7;
    private int strength = 1;
    private int golds;

    public HeroState(Cell cell, Color color, string heroName, string parentHero)
    {
        this.cell = cell;
        action = Action.None;
        TimeOfDay = new TimeOfDay(color, heroName);
        heroInventory = new HeroInventory(parentHero);
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
        if (points >= willpower) willpower = 0; else willpower -= points;
    }

    public int getStrength()
    {
        return strength;
    }

    public int getWP()
    {
        return willpower;
    }

    public void setWP(int hero_wp)
    {
        willpower = hero_wp;
    }

    public void setStrength(int s)
    {
        strength = s;
    }
}
