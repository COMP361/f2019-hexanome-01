using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrak : Enemy {
    
    public static Wardrak Factory(int cellID) {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Wardrak");
        GameObject go = new GameObject("Wardrak");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);
        
        Wardrak wardrak = go.AddComponent<Wardrak>();
        wardrak.TokenName = Type;
        wardrak.Cell = Cell.FromId(cellID);
        
        wardrak.Will = 7;
        wardrak.Strength = 10;
        wardrak.Reward = 6;

        return wardrak;
    }

    public static string Type { get => typeof(Wardrak).ToString(); }
}