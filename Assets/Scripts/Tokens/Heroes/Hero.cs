using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Sex
{
    Female,
    Male
}

public class Hero : Movable
{
    protected Sex sex = Sex.Female;

    protected string[] names;
    protected int rank;
    public string Type { get; protected set; }
    public Color Color { get; set; }
    
    public Timeline timeline;
    public HeroInventory heroInventory;
    
    public string HeroName {
        get
        {
            return names[(int)sex];
        }
    }

    public int[] Dices { get; protected set; }
    public Action Action { get; set; } = Action.None;
    public int Strength { get; set; } = 1;
    public int Willpower { get; set; } = 7;
 
    protected override Vector3 getWaypoint(Cell cell)
    {
        return cell.HeroesPosition;
    }

    public void Init() {
        timeline = new Timeline(this);
        heroInventory = new HeroInventory(Type.ToString());
        MoveCost = 1;
    } 

    public void decrementWP(int points)
    {
        if(points < 0) return;
        if(points >= Willpower) Willpower = 0; else Willpower -= points;
        EventManager.TriggerCurrentPlayerUpdate(this);
    }
}