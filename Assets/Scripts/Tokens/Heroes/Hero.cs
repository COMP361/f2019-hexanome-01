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
    //State.cell.removeToken(token);
    State.cell = c;
    //State.cell.addToken(token);
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
  private Timeline timeline;

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
}
