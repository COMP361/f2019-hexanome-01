using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------- 
    Graphic represensation of elements on board
   --------------------------------------------- 
*/

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
        }
    }
}
