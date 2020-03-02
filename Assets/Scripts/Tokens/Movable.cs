using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : Token
{
    public bool IsDone { get; set; }
    int moveSpeed = 20;
    List<Cell> path;
    bool isMoving;
    Sprite icon;
    protected virtual string IconPath => String.Empty;


    protected virtual void Awake() {
        //if (IconPath != String.Empty)

        
    }
    void Update()
    {
        Move();
    }

    protected override Vector3 getWaypoint(Cell cell) {
        return cell.MovablesPosition;
    }

    // Should we verify that all cells in path are adjacent?
    public void Move(List<Cell> path)
    {
        isMoving = true;
        this.path = path;
        EventManager.TriggerMoveStart(this);
    }

    public void Move(Cell c)
    {
        isMoving = true;
        path = new List<Cell>();
        path.Add(c);
        EventManager.TriggerMoveStart(this);
    }

    void Move()
    {
        IsDone = false;

        if (path == null || path.Count == 0)
        {
            IsDone = true;
            isMoving = false;
            return;
        }

        if(AtCell(path[0])) {
            Cell = path[0];
            path.RemoveAt(0);

            if(path.Count == 0) EventManager.TriggerMoveComplete(this);
            return;
        }

        Position = Vector3.MoveTowards(Position, getWaypoint(path[0]), moveSpeed * Time.deltaTime);
    }

    public bool AtCell(Cell c) {
        return Vector3.Distance(Position, getWaypoint(c)) < 0.5;
    }

    public GameObject Token
    {
        get
        {
            return gameObject;
        }
    }



    //public void Position(Cell cell) {
    //    Token.Position = cell.Waypoint;
    //}
}
