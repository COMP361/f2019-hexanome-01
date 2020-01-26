using UnityEngine;

class Geometry {
    public static GameObject DrawLine(Vector3 start, Vector3 end, Color color, float width = 0.1f) {
        GameObject line = new GameObject();
        line.transform.position = start;
        line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = color;
        lr.endColor = color;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        return line;
    }
}