using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Sex {
    Female,
    Male
}

[RequireComponent(typeof(Inventory))]

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
  private int willpower;
  private int strength;
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
