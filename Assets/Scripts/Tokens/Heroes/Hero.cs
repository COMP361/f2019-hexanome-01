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
        get {
            return names[(int)sex];
        }
    }

    public int[] Dices { get; protected set; }
    public Action Action { get; set; } = Action.None;
    
    private int _strength = 1;
    public int Strength { 
        get {
            return _strength;
        }
        set {
            if(value < 0) _strength = 0;
            _strength = value;
            EventManager.TriggerUpdateHeroStats(this);
        }
    }
    
    private int _willpower = 7;
    public int Willpower { 
        get {
            return _willpower;
        }
        set {
            if(value < 0) _willpower = 0;
            _willpower = value;
            EventManager.TriggerUpdateHeroStats(this);
        }
    }
 
    protected override Vector3 getWaypoint(Cell cell)
    {
        return cell.HeroesPosition;
    }

    public void Init() {
        timeline = new Timeline(this);
        heroInventory = new HeroInventory(Type.ToString());
        MovePerHour = 1;
    }
}