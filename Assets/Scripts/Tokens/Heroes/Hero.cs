using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Sex {
    Female,
    Male
}

public class Hero : Movable {
  protected Sex sex = Sex.Female;
  protected string[] names;
  protected int rank;

  // TODO
  void OnTokenMoveComplete(Token token, Cell c) {
    //State.cell.removeToken(token);
    State.cell = c;
    //State.cell.addToken(token);
  }

  //public bool IsDone { get; set; }
  
  public string Type { get; protected set; }

  public HeroState State { get; set; }

  public string HeroName {
    get {
      return names[(int)sex];
    }
  }

  public int[] Dices { get; protected set; }

  public Action Action {
    get {
      return State.action;
    }
  }

  public Color Color { get; set; }

  protected override Vector3 getWaypoint(Cell cell) {
    return cell.HeroesPosition;
  }
}

public class HeroState : ICloneable {
  public Action action;
  public Cell cell;
  private int freeMove;
<<<<<<< HEAD
  private int willpower;
  private int strength;
=======
  private int willpower { get; set; }
  private int strength { get; set; }
>>>>>>> d5da44c... working attack
  private int golds;
  private Timeline timeline;
  private int hoursOfDay;

  public HeroState(Cell cell) {
    this.cell = cell;
    action = Action.None;
    timeline = new Timeline();
  }

  public object Clone() {
    HeroState hs = (HeroState) this.MemberwiseClone();
    hs.timeline = (Timeline) timeline.Clone();
    return hs;
  }     
<<<<<<< HEAD
=======

    public int getStrength()
    {
        int temp = strength;
        return temp;
    }

    public int getWP()
    {
        int temp = willpower;
        return temp;
    }

    public void setStrength(int value)
    {
        this.strength = value;
    }

    public void setWP(int value)
    {
        this.willpower = value;
    }
>>>>>>> d5da44c... working attack
}
