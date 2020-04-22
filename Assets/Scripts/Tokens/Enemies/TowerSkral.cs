using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSkral : Enemy
{
    public static TowerSkral Factory(int cellID, int numPlayers)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/TowerSkral");
        GameObject go = new GameObject("Tower Skral");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        TowerSkral skral = go.AddComponent<TowerSkral>();
        skral.TokenName = Type;
        skral.Cell = Cell.FromId(cellID);

        skral.Will = 6;
        skral.Reward = 4;

        if (numPlayers == 3) {
            skral.Strength = 30;
        } else {
            skral.Strength = 40;
        }

        return skral;
    }

    public static string Type { get => typeof(TowerSkral).ToString(); }
}