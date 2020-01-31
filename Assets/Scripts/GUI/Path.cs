using UnityEngine;
using System.Collections.Generic;

public class MapPath {
    GameObject pathContainer;
    List<GameObject> pathLines;

    public MapPath(Cell origin, Cell goal) {
        Cells = new Pathfinding(origin, goal).SearchPath();
        pathLines = new List<GameObject>();
        pathContainer = GameObject.Find("Paths");
    
        if(Cells != null && Cells.Count > 0) {
            for(int i = 0; i < Cells.Count - 1; i++) {
                GameObject line = Geometry.Line(Cells[i].Waypoint + new Vector3(0, 0, -0.1f), Cells[i + 1].Waypoint + new Vector3(0, 0, -0.1f), Color.red);
                line.transform.parent = pathContainer.transform;
                pathLines.Add(line);
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
