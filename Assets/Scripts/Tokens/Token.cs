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
    protected Cell cell;

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
        //GameObject go = new GameObject("New Sprite");
        // renderer = go.AddComponent<SpriteRenderer>();
        //renderer.sprite = Resources.Load("Sprites/dot");
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
            if(cell != null && cell.State != null) cell.State.removeToken(this);

            cell = value;
            gameObject.transform.position = getWaypoint(cell);
            cell.State.addToken(this);

            if(cell.State.cellInventory.Enemies.Count > 0) EventManager.TriggerMonsterOnCell(this);
        }
    }
}
