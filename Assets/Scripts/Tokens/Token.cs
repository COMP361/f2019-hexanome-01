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

    public void Awake() {
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

    public Cell Cell { 
        get {
            return cell;
        } 
        set {
            cell = value;
            gameObject.transform.position = cell.Waypoint;
        }
    }
}