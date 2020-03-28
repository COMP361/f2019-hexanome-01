using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wineskin : Item
{
    public static Wineskin Factory()
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/WineSkin");
        GameObject go = new GameObject("WineSkin");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        Wineskin wineskin = go.AddComponent<Wineskin>();
        wineskin.TokenName = Type;

        return wineskin;
    }

    public static Wineskin Factory(int cellID)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/WineSkin");
        GameObject go = new GameObject("WineSkin");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        Wineskin wineskin = go.AddComponent<Wineskin>();
        wineskin.TokenName = Type;
        wineskin.Cell = Cell.FromId(cellID);

        return wineskin;
    }

    public static string Type { get => typeof(Wineskin).ToString(); }
}
