using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gor : Enemy
{
    static Color color = Color.black;

    public static Gor Factory(int cellID)
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Gor");
        GameObject go = new GameObject("Gor");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        Gor gor = go.AddComponent<Gor>();
        gor.TokenName = Type;
        gor.Cell = Cell.FromId(cellID);

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }
/*
    public void awake(){
      gor.TokenName = Type;
      gor.Cell = Cell.FromId(cellID);

      gor.Will = 4;
      gor.Strength = 2;
      gor.Reward = 2;
    }
*/
    public static string Type { get => typeof(Gor).ToString(); }
}
