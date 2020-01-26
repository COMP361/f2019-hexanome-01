using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Sex {
    Female,
    Male
}

public class Hero : MonoBehaviour
{
  protected Sex sex = Sex.Female;
  protected string[] names;
  protected int rank;
  protected int[] dices;
  protected string type;

  public void Setup(int rank, Color color) {
    this.rank = rank;
    Color = color;
    Cell c = Cell.FromId(rank);

    State = new HeroState(c);
    type = GetType().Name;
    IsDone = false;

    Token = Token.Factory(type, color);
    Token.Position(c);
  }
  
  public void Move(List<Cell> path) {
    Token.Move(path);
  }

  // TODO
  void OnTokenMoveComplete(Token token, Cell c) {
    State.cell = c;
  }

  public bool IsDone { get; set; }
  
  public string Type { 
    get {
      return type;
    } 
  }

  public HeroState State { get; set; }

  public string Name {
    get {
      return names[(int)sex];
    }
  }

  public int[] Dices { get; set; }

  public Action Action {
    get {
      return State.action;
    }
  }

  public Color Color { get; set; }

  public Token Token { get; set; }
}

public class HeroState : ICloneable
{
  public Action action;
  public Cell cell;
  private int freeMove;
  private int willpower;
  private int strength;
  private int golds;

  public HeroState(Cell cell) {
    this.cell = cell;
    action = Action.None;
  }

  public object Clone() {
    HeroState hs = new HeroState(cell);
    hs.action = action;
    hs.freeMove = freeMove;
    hs.willpower = willpower;
    hs.strength = strength;
    hs.golds = golds;

    return hs;
  }     
}