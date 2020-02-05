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

    void Update()
    {
        Move();
    }

    // Should we verify that all cells in path are adjacent?
    public void Move(List<Cell> path)
    {
        isMoving = true;
        this.path = path;
    }

    public void Move(Cell c)
    {
        isMoving = true;
        path = new List<Cell>();
        path.Add(c);
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

        if (AtCell(path[0]))
        {
            path.RemoveAt(0);
            return;
        }

        Position = Vector3.MoveTowards(Position, path[0].Waypoint, moveSpeed * Time.deltaTime);
    }

    public bool AtCell(Cell c)
    {
        return Vector3.Distance(Position, c.Waypoint) < 0.5;
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