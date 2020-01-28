using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]

public class Cell : MonoBehaviour {
    public List<Transform> neighbours = new List<Transform>();
    public Cell enemyPath;
    private int index;
    private Cell parent;

    private float heuristic;
    private float cost;

    private Vector3 waypoint;

    private CellState state;


    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);
    private Color32 hoverColor = new Color(1f,1f,1f,.2f);
    GameManager gm;




    void Awake() {
        waypoint = transform.Find("placeholders/waypoint").position;
        state = new CellState();

    }

    void Start() {
        gm = GameManager.instance;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;

        if (!int.TryParse(this.name, out index)) {
            index = -1;
        }

        state.formatDescription();
    }

    public static Cell FromId(int id) {
      Cell cell = null;
      GameObject go = GameObject.Find("Cells/" + id);
      if(go != null) cell = go.GetComponent<Cell>();

      if(cell == null) {
        var message = string.Format("'{0}' is not a valid cell id.", id);
        throw new ApplicationException(message);
      }

      return cell;
    }

    void OnMouseEnter() {
      //if(gm.state.Action == Action.Move) {
      var color = gm.CurrentPlayer.Color;
      color.a = .4f;
      sprite.color = color;



      GameManager.cellsDescription.GetComponent<Text>().text = state.Description;
      GameManager.cellsDescription.gameObject.SetActive(true);
        //} else {
        //    sprite.color = hoverColor;
        //}
    }

    void OnMouseExit() {
        sprite.color = color;
        GameManager.cellsDescription.gameObject.SetActive(false);
    }

    void OnMouseDown() {
      EventManager.TriggerCellClick(index);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(waypoint, 0.4f);
    }

    public Vector3 Waypoint {
      get {
        return waypoint;
      }
    }

    public int Index {
      get {
        return index;
      }
    }

    public float Heuristic {
      get {
        return heuristic;
      }
      set {
        heuristic = value;
      }
    }

    public float Cost {
      get {
        return cost;
      }
      set {
        cost = value;
      }
    }

    public Cell Parent {
      get {
        return parent;
      }
      set {
        parent = value;
      }
    }

    public float f {
        get { return cost + heuristic; }
    }

    public CellState State {
        get {
            return state;
        }
    }
}

public class CellState : ICloneable
{
  // Pickable
  private String description;
  private List<Token> heroes = new List<Token>();
  private List<Token> enemies = new List<Token>();
  private List<Token> items = new List<Token>();
  private List<Token> golds = new List<Token>();
//  private IEnemy enemy;


  // Should we have well, fog?

  public object Clone() {
    CellState cs = (CellState) this.MemberwiseClone();
    return cs;
  }

  public void addHeroToken(Token token){
    heroes.Add(token);
  }

  public void removeHeroToken(Token token){
    heroes.Remove(token);
  }

  public void formatDescription(){

    this.description = "Heroes: \n";
    foreach (var hero in heroes) {
          this.description = description + "  - " + hero.description + " \n";
      }
    this.description = description + "Item: \n";
    foreach (var item in items) {
          this.description = description + "  - " + item.description + " \n";
      }
    this.description = description + "Monster: \n";
    foreach (var enemy in enemies) {
          this.description = description + "  - " + enemy.description + " \n";
      }
    this.description = description + "Gold: \n";
    foreach (var gold in golds) {
          this.description = description + "  - " + gold.description + " \n";
      }
  }

  public String Description {
      get {
          return description;
      }
  }
/*
  public IEnemy Enemy {
    get {
      return enemy;
    }
    set {
      enemy = value;
    }
  }
  */
}
