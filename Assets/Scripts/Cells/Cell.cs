using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class Cell : MonoBehaviour {
    public static List<Cell> cells = new List<Cell>();
    public List<Transform> neighbours = new List<Transform>(); 
    public Cell enemyPath;
    private GameManager gm;
    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);

    private Color32 hoverColor = new Color(1f,1f,1f,.2f);
    
    public void Deactivate() {
      Active = false;
      Extension = false;
      color = new Color(0f,0f,0f,0.7f);
      sprite.color = color;
    }

    public void Reset() {
      Active = true;
      Extension = false;
      color = new Color(1f,1f,1f,0f);
      sprite.color = color;
    }
    
    public void Extended() {
      Extension = true;
      Active = true;
      color = new Color(1f,0f,0f,0.3f);
      sprite.color = color;
    }

    void Awake() {
        cells.Add(this);
        Active = true;
        Position = transform.position;
        HeroesPosition = transform.Find("positions/heroes").position;
        MovablesPosition = transform.Find("positions/movables").position;
        TokensPosition = transform.Find("positions/tokens").position;
        State = new CellState();
    }

    void Start() {
        gm = GameManager.instance;
        
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
        
        Index = int.Parse(this.name);
    }

    // Return the list of cells between min distance and max distance 
    public List<Cell> WithinRange (int min, int max) {
      Queue<Tuple<int, Cell>> queue = new Queue<Tuple<int, Cell>>();
      HashSet<Cell> visited = new HashSet<Cell>();
      List<Cell> cells = new List<Cell>();

      if(min < 0) min = 0;

      queue.Enqueue(new Tuple<int, Cell>(0, this));
      Tuple<int, Cell> t;
      Cell c;
      int i;

      do {
        t = queue.Dequeue();
        i = t.Item1+1;

        for (int j = 0; j < t.Item2.neighbours.Count; j++) {
          c = t.Item2.neighbours[j].GetComponent<Cell>();  
          if (visited.Add(c)) {
              if(i >= min) cells.Add(c);
              queue.Enqueue(new Tuple<int, Cell>(i, c));
          }
        }
      } while(queue.Count > 0 && t.Item1 < max);

      return cells;
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
      if(!Active) return;

      var color = gm.CurrentPlayer.Color;
      color.a = .4f;
      sprite.color = color;
        //} else {
        //    sprite.color = hoverColor;
        //}
      EventManager.TriggerCellMouseEnter(Index);
    }

    void OnMouseExit() {
      sprite.color = color;
      EventManager.TriggerCellMouseLeave(Index);
    }

    void OnMouseDown() {
      if(!Active) return;
      EventManager.TriggerCellClick(Index);
    }

    void OnDrawGizmos() {
      GameObject positions = transform.Find("positions").gameObject;
      Gizmos.color = Color.blue;

      foreach (Transform child in positions.transform) {
        Gizmos.DrawSphere(child.position, 0.6f);
      }
    }

    public Vector3 HeroesPosition { get; private set; }
    public Vector3 MovablesPosition { get; private set; }
    public Vector3 TokensPosition { get; private set; }
    public Vector3 Position { get; private set; }
    public int Index { get; private set; }
    public bool Active { get; set; }
    public bool Extension { get; set; }
    public float Heuristic { get; set; }
    public float Cost { get; set; }
    public Cell Parent { get; set; }

    public float f {
        get { return Cost + Heuristic; }
    }

    public CellState State { get; private set; }
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