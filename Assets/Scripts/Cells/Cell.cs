using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        //} else {
        //    sprite.color = hoverColor;
        //}
      EventManager.TriggerCellMouseEnter(index);
    }

    void OnMouseExit() {
      sprite.color = color;
      EventManager.TriggerCellMouseLeave(index);
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
  // Should we have well, fog?
  public CellState() {
    Heroes = new List<Hero>();
    Enemies = new List<Enemy>();
    Farmers = new List<Farmer>();
    Tokens = new List<Token>();
    Golds = new List<Token>();
  }

  public void addToken(Token token){
    Type listType;
    
    listType = Heroes.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Heroes.Add((Hero) token); 
      return;
    }

    listType = Enemies.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Enemies.Add((Enemy) token); 
      return;
    }

    listType = Farmers.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Farmers.Add((Farmer) token); 
      return;
    }
  }

  public void removeToken(Token token){
    Type listType;
    
    listType = Heroes.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Heroes.Remove((Hero) token); 
      return;
    }

    listType = Enemies.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Enemies.Remove((Enemy) token); 
      return;
    }

    listType = Farmers.GetListType();
    if(listType.IsCompatibleWith(token.GetType())) {
      Farmers.Remove((Farmer) token); 
      return;
    }
  }
  
  public object Clone() {
    CellState cs = (CellState) this.MemberwiseClone();
    return cs;
  }

  public List<Hero> Heroes { get; private set; }
  public List<Enemy> Enemies { get; private set; }
  public List<Farmer> Farmers { get; private set; }
  public List<Token> Tokens { get; private set;  }
  public List<Token> Golds { get; private set;  }
}