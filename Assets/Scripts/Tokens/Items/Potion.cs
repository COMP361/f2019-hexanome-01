using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : SmallToken
{
    public static Potion Factory()
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Potion");
        GameObject go = new GameObject("Potion");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        Potion potion = go.AddComponent<Potion>();
        potion.TokenName = Type;

        return potion;
    }

    public static Potion Factory(int cellID)
    {
        Potion potion = Potion.Factory();
        potion.Cell = Cell.FromId(cellID);
        return potion;
    }


    public static string Type { get => typeof(Potion).ToString(); }
}
