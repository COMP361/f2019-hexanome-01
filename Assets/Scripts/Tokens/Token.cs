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
    protected Cell cell { get; private set; }

    public void OnEnable() {
        this.transform.parent = GameObject.Find("Tokens").transform;
    }

    public string TokenName {
        get {
            return gameObject.name;        }
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
        if(gameObject.GetComponent<SpriteRenderer>() != null){
          return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else{

            return Resources.Load<Sprite>("Sprites/dot");
        }
    }

    protected virtual Vector3 getWaypoint(Cell cell) {
        return cell.TokensPosition;
    }

    public Cell Cell {
        get {
            return cell;
        }
        set {
            if(cell != null && cell.Inventory != null){
              cell.Inventory.RemoveToken(this);
            }
            cell = value;
            gameObject.transform.position = getWaypoint(cell);
            cell.Inventory.addToken(this);

            EventManager.TriggerCellUpdate(this);
        }
    }

    public void useCell(){
      //EventManager.TriggerCellGoldClick();

    }
    public void useHero(){
      //EventManager.TriggerHeroGoldClick();
    }
}
