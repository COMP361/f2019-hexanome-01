﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class Cell : MonoBehaviour, IClickHandler {
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
    EventManager em;

    void Awake() {
        waypoint = transform.Find("placeholders/waypoint").position;
        state = new CellState();
    }

    void Start() {
        gm = GameManager.instance;
        em = gm.em;
        
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
    }

    void OnMouseExit() {
        sprite.color = color;
    }

    public void OnClick() {
      em.OnCellClick(index);
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
  GameManager gm;
  // Pickable
  private List<Token> tokens;
  private Enemy enemy;
  private List<Hero> heroes; 

  // Shuld we have well, fog?

  public CellState() {
    gm = GameManager.instance;
  }

  public object Clone() {
    CellState cs = new CellState();
    cs.tokens = tokens;
    cs.enemy = enemy;
    cs.heroes = heroes;

    return cs;
  }
    
  public Enemy Enemy {
    get { 
        return enemy; 
    }
    set {
        enemy = value;
    }
  }
}
