using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skral : Enemy
{
    //int dices;
    static Color color = Color.black;

    public static Skral Factory(int cellID)
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Enemies/skral");
        GameObject go = new GameObject("Skral"); //Geometry.Disc(Vector3.zero, color);
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        go.transform.localScale = new Vector3(10, 10, 10);

        Skral skral = go.AddComponent<Skral>();
        skral.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        skral.Cell = cell;

        skral.Will = 6;
        skral.Strength = 6;
        skral.Reward = 4;

        return skral;
    }

    public static string Type { get => typeof(Skral).ToString(); }
}