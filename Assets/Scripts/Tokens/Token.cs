using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------- 
    Graphic represensation of elements on board
    Implement the move functionality
   --------------------------------------------- 
*/

public class Token : MonoBehaviour {
    GameObject go;
    //public string name;
    int moveSpeed;  
    List<Cell> path;  
    bool isDone;

    void Update() {
        Move();
    }
    
    /* --------------------------------------------- 
        Static method
        Create a disk of color as graphical representation
        for the token
       --------------------------------------------- 
    */

    public static Token Factory(string name, Color color) {
        Token token = Factory(name, GameObject.CreatePrimitive(PrimitiveType.Cylinder));
        
        token.transform.localScale = new Vector3(2, 0.1f, 2);
        token.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        Renderer tokenRenderer = token.GetComponent<Renderer>();
        tokenRenderer.material.SetColor("_Color", color);
        tokenRenderer.material.shader = Shader.Find("UI/Default");

        return token;
    }

    /* --------------------------------------------- 
        Static method
        Create the Token by attaching the script Token 
        on the given GameObject go
        Set default values
       --------------------------------------------- 
    */

    public static Token Factory(string name, GameObject go) {
        Token token = go.AddComponent<Token>();
        go.name = name;
        token.go = go;
        token.moveSpeed = 5;
        token.go.transform.parent = GameObject.Find("Tokens").transform;
        
        return token;
    }

    public void Position(Cell cell) {
        go.transform.position = cell.Waypoint;
    }
    
    // Should we verify that all cells in path are adjacent?
    public void Move(List<Cell> path) {
        this.path = path;
    }

    public void Move(Cell c) {
        path = new List<Cell>();
        path.Add(c);
    }

    public void Move() {
        isDone = false;

        if(path == null || path.Count == 0) {
            isDone = true;
            return;
        }
        
        if(AtCell(path[0])) {
          path.RemoveAt(0);
          return;
        }
        
        go.transform.position = Vector2.MoveTowards(go.transform.position, path[0].Waypoint, moveSpeed * Time.deltaTime);
    }
    
    public bool AtCell(Cell c) {
        return Vector3.Distance(go.transform.position, c.Waypoint) < 0.5; 
    }

    public bool IsDone { 
        get { 
            return IsDone; 
        }
    } 
}
