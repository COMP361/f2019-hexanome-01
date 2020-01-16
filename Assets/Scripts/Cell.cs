using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class Cell : MonoBehaviour
    , IPointerClickHandler
    //, IDragHandler
    //, IPointerEnterHandler
    //, IPointerExitHandler
{
    public List<Transform> neighbours = new List<Transform>();
    
    public Cell enemyPath;
    private int index;
    private Cell parent;

    private float heuristic;
    private float cost;
    private Vector3 waypoint;
    //private List<Item> inventory;
    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);
    private Color32 hoverColor = new Color(1f,1f,1f,.2f);
    GameManager gm;

    void Awake() {
        waypoint = transform.Find("placeholders/waypoint").position;
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
      GameObject go = GameObject.Find("Cells/" + id);
      if(go != null) return go.GetComponent<Cell>();
      return null;
    }

    void OnMouseEnter() {
      //if(gm.state.Action == Action.Move) {
      var color = gm.CurrentPlayer.Color;
      color.a = .4f;
      sprite.color = color;
        //} else {
        //    sprite.color = hoverColor;
        //}
    }

    void OnMouseExit() {
        sprite.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      //gm.OnCellClick();
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
}