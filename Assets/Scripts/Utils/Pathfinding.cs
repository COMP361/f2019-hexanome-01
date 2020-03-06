using DataStructure;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    //private List<Cell> openList;
    private List<Cell> closedList;
    private Cell start, goal;
    private PriorityQueue<Cell, float> openList;
    int maxIteration;

    public Pathfinding(Cell start, Cell goal) {
        this.start = start;
        start.Parent = null;
        goal.Parent = null;

        this.goal = goal;
        //maxIteration = 15;

        openList = new PriorityQueue<Cell, float>();
        closedList = new List<Cell>();
    }

    public List<Cell> SearchPath() {

        if(start == goal) return new List<Cell>();

        openList.Clear();
        closedList.Clear();
        //int iterations = 0;

        start.Heuristic = (goal.Position - start.Position).magnitude;
        openList.Enqueue(start, start.f);

        while (openList.Count > 0 /*&& iterations < maxIteration*/) {
            var bestCell = openList.Dequeue();

            var neighbours = bestCell.neighbours;
            for (int i = 0; i < neighbours.Count; i++) {
                var curCell = neighbours[i].GetComponent<Cell>();

                if (curCell == null)
                    continue;
                if (curCell == goal) {
                    curCell.Parent = bestCell;
                    return ConstructPath(curCell);
                }

                var g = bestCell.Cost + (curCell.Position - bestCell.Position).magnitude;
                var h = (goal.Position - curCell.Position).magnitude;

                if (openList.Contains(curCell) && curCell.f < (g + h))
                    continue;
                if (closedList.Contains(curCell) && curCell.f < (g + h))
                    continue;

                curCell.Cost = g;
                curCell.Heuristic = h;
                curCell.Parent = bestCell;

                if (!openList.Contains(curCell))
                    openList.Enqueue(curCell, curCell.f);
            }

            if (!closedList.Contains(bestCell))
                closedList.Add(bestCell);

            //iterations++;
        }

        return null;
    }

    private List<Cell> ConstructPath (Cell destination) {
        var path = new List<Cell>() { destination };

        var current = destination;
        while (current.Parent != null)
        {
            current = current.Parent;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}