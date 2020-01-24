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
  protected bool isDone;
  protected int moveSpeed;
  protected string type;
  protected HeroState state;
  protected Sex sex = Sex.Female;
  protected string[] names;
  protected Cell rank;
  protected int[] dices;
  protected GameObject token;
  protected Color color = new Color(0, 0, 0, 1);


  public void Init(Cell rank)
  {
    state = new HeroState(rank);
    type = this.GetType().Name;
    isDone = false;
    moveSpeed = 5;

    token = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    token.transform.position = state.cell.Waypoint;
    token.transform.localScale = new Vector3(2, 0.1f, 2);
    token.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

    var tokenRenderer = token.GetComponent<Renderer>();
    tokenRenderer.material.SetColor("_Color", color);
    tokenRenderer.material.shader = Shader.Find("UI/Default");

    token.transform.parent = gameObject.transform;
    token.name = "Token";
  }

  void Update() {
    Move();
  }

  public bool AtCell(Cell c) {
    return Vector3.Distance(token.transform.position, c.Waypoint) < 0.5;
  }

  public void Move() {
    if(state.path == null || state.path.Count == 0) {
      //IsDone = true;
      return;
    }

    if(AtCell(state.path[0])) {

      state.cell.removeToken(token);
      state.cell = state.path[0];
      state.cell.addToken(token);

      state.path.RemoveAt(0);
      return;
    }

    token.transform.position = Vector2.MoveTowards(token.transform.position, state.path[0].Waypoint, moveSpeed * Time.deltaTime);
  }

  public bool IsDone {
    get {
      return isDone;
    }
    set {
      isDone = value;
    }
  }

  public string Type {
    get {
      return type;
    }
  }

  public HeroState State {
    get {
      return state;
    }
    set {
      state = value;
    }
  }

  public string Name {
    get {
      return names[(int)sex];
    }
  }

  public int[] Dices {get; set;}

  public Action Action {
    get {
      return State.action;
    }
  }

  public Color Color {
    get {
      return color;
    }
  }
}

public class HeroState : ICloneable
{
  public Action action;
  GameManager gm;
  public Cell cell;
  public Cell[] stops;
  public Cell goal;
  public List<Cell> path;
  private int freeMove;
  private int willpower;
  private int strength;
  private int golds;

  public HeroState(Cell cell) {
    this.cell = cell;
    action = Action.None;
    gm = GameManager.instance;
    path = new List<Cell>();
  }

  public object Clone() {
    HeroState hs = new HeroState(cell);
    hs.action = action;
    hs.stops = stops;
    hs.goal = goal;
    hs.path = path;
    hs.freeMove = freeMove;
    hs.willpower = willpower;
    hs.strength = strength;
    hs.golds = golds;

    return hs;
  }

  public List<Cell> Path {
    get {
      return path;
    }
  }

  public Cell Goal {
    get {
      return goal;
    }
    set {
      goal = value;
      path = new Pathfinding(cell, goal).SearchPath();
    }
  }
}
