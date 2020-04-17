using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : Token
{
    int moveSpeed = 200;
    List<Cell> path;
    Sprite icon;
    protected virtual string IconPath => String.Empty;
    public int MovePerHour { get; set; }

    void Update() {
        Move();
    }

    protected override Vector3 getWaypoint(Cell cell) {
        return cell.MovablesPosition;
    }

    public void Move(List<Cell> path, int freeMoves = 0) {
        // path first cell is the current cell, remove it
        if(AtCell(path[0])) path.RemoveAt(0);

        this.path = path;
        EventManager.TriggerMove(this, path.Count - freeMoves);
    }

    public void Move(Cell c) {
        if(AtCell(c)) return;

        path = new List<Cell>();
        path.Add(c);
        EventManager.TriggerMove(this, path.Count);
    }

    void Move() {
        if (path == null || path.Count == 0) return;

        if(AtCell(path[0])) {
            Cell = path[0];
            path.RemoveAt(0);

            if(path.Count == 0) {
              EventManager.TriggerMoveComplete(this);
            }
            return;
        }

        Position = Vector3.MoveTowards(Position, getWaypoint(path[0]), moveSpeed * Time.deltaTime);
    }

    public bool AtCell(Cell c) {
        return Vector3.Distance(Position, getWaypoint(c)) < 0.5;
    }

    public int GetMoveCost(int qty) {
        if(MovePerHour == 0) return 0;
        return (int)Math.Ceiling((double)qty/MovePerHour);
    }

    public GameObject Token {
        get {
            return gameObject;
        }
    }
}
