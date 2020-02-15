using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gor : Enemy
{
    static Color color = Color.black;

    public static Gor Factory(int cellID)
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Enemies/gor");
        GameObject go = new GameObject("Gor"); //Geometry.Disc(Vector3.zero, color);
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        Gor gor = go.AddComponent<Gor>();
        gor.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        gor.Cell = cell;

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }

    public static string Type { get => typeof(Gor).ToString(); }
}
