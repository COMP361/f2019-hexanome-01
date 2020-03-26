using UnityEngine;

class Geometry {
    public static GameObject Line(Vector3 start, Vector3 end, Color color, float width = 1f) {
        GameObject line = new GameObject();
        line.transform.position = start;
        line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = color;
        lr.endColor = color;
        lr.sortingOrder = 18;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        return line;
    }

    /*public static GameObject Disc(Vector3 center, Color color, float radius = 1.2f) {
        GameObject disc = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        disc.transform.position = center;
        disc.transform.localScale = new Vector3(radius, 0.1f, radius);
        disc.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        Renderer discRenderer = disc.GetComponent<Renderer>();
        discRenderer.material.SetColor("_EmissionColor", color);
        discRenderer.material.shader = Shader.Find("Sprites/Default");

        return disc;
    }*/
}