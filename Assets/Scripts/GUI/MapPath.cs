using UnityEngine;
using System.Collections.Generic;

public class MapPath {
    GameObject pathContainer;
    List<GameObject> pathLines;
    Color color;
    float width;
    
    public MapPath(Cell origin, Color color, float width = 1f) {
        Cells = new List<Cell>();
        Cells.Add(origin);
        pathLines = new List<GameObject>();
        pathContainer = GameObject.Find("Paths");
        this.color = color;
        this.width = width;
    }

    public MapPath(Cell origin, Cell goal, Color color, float width = 1f) {
        Cells = new Pathfinding(origin, goal).SearchPath();
        pathLines = new List<GameObject>();
        pathContainer = GameObject.Find("Paths");
        this.color = color;

        if(Cells != null && Cells.Count > 0) {
            for(int i = 0; i < Cells.Count - 1; i++) {
                GameObject line = Geometry.Line(Cells[i].HeroesPosition, Cells[i + 1].HeroesPosition, color, width);
                line.transform.parent = pathContainer.transform;
                pathLines.Add(line);
            }
        }
    }

    public void Extend(Cell goal) {
        Cell origin = Cells[Cells.Count - 1];
        List<Cell> extCells = new Pathfinding(origin, goal).SearchPath();
        
        if(extCells != null && extCells.Count > 0) {
            for(int i = 0; i < extCells.Count; i++) {
                if(i < extCells.Count - 1) {
                    GameObject line = Geometry.Line(extCells[i].HeroesPosition, extCells[i + 1].HeroesPosition, color, width);
                    line.transform.parent = pathContainer.transform;
                    pathLines.Add(line);
                }
                
                // first cell of the path is already stored 
                if(i > 0) {
                    Cells.Add(extCells[i]);
                }  
            }  
        }
    }

    public void Dispose() {
        for(int i = 0; i < pathLines.Count; i++) {
            GameObject.Destroy(pathLines[i]);
        }
    }

    public List<Cell> Cells { get; set; }
}
