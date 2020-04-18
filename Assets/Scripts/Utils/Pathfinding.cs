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
        
        openList = new PriorityQueue<Cell, float>();
        closedList = new List<Cell>();
    }

    public List<Cell> SearchPath() {

        if(start == goal) return new List<Cell>();

        openList.Clear();
        closedList.Clear(); 

        openList.Enqueue(start, start.Cost);

        while (openList.Count > 0) {
            var bestCell = openList.Dequeue();

            if (bestCell == goal) {
                return ConstructPath(bestCell);
            }

            var neighbours = bestCell.neighbours;
            for (int i = 0; i < neighbours.Count; i++) {
                var curCell = neighbours[i].GetComponent<Cell>();

                if (curCell == null) continue;
 
                var g = bestCell.Cost + 1;
                
                if (openList.Contains(curCell) && curCell.Cost < g) continue;
                if (closedList.Contains(curCell) && curCell.Cost < g) continue;

                curCell.Cost = g;
                curCell.Parent = bestCell;

                if (!openList.Contains(curCell))
                    openList.Enqueue(curCell, curCell.Cost);
            }

            if (!closedList.Contains(bestCell))
                closedList.Add(bestCell);
        }

        return null;
    }

    private List<Cell> ConstructPath(Cell destination) {
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