using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ---------------------------------------------

   ---------------------------------------------
*/
/// <summary>
/// Graphic represensation of elements on board
/// </summary>
public class Token : MonoBehaviour {
    protected string description;
    private Cell _cell;
    public string desc;
    public string itemName;
    public int reserved = 0;
    public int maxUse = 1;

    public Cell Cell {
        get {
            return _cell;
        }
        set {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if(value == null) {
                if(sr != null) sr.sortingOrder = -1;
            } else {
                if(sr != null) sr.sortingOrder = 3;
            }

            if(_cell != null && _cell.Inventory != null) {
              _cell.Inventory.RemoveToken(this);
            }

            _cell = value;

            if(_cell != null && _cell.Inventory != null) {
                gameObject.transform.position = getWaypoint(_cell);
                _cell.Inventory.AddToken(this);
            }

            EventManager.TriggerCellUpdate(this);
        }
    }

    public void OnEnable() {
        this.transform.parent = GameObject.Find("Tokens").transform;
    }

    public string TokenName {
        get {
            return gameObject.name;
          }
        set {
            gameObject.name = value;
        }
    }

    public Vector3 Position {
        get {
            return gameObject.transform.position;
        }
        protected set {
            gameObject.transform.position = value;
        }
    }

    public Sprite getSprite(){
        if(gameObject != null && gameObject.GetComponent<SpriteRenderer>() != null){
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        } else {
            return Resources.Load<Sprite>("Sprites/dot");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected virtual Vector3 getWaypoint(Cell cell) {
        if(cell == null) return Vector3.negativeInfinity;
        return cell.TokensPosition;
    }

    public virtual void UseCell(){
    }

    public virtual void UseHero(){
    }

    public virtual void UseEffect(){
    }
}
