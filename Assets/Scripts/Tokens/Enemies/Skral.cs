using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skral : Enemy
{
    public static Skral Factory(int cellID) {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Skral");
        GameObject go = new GameObject("Skral");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        Skral skral = go.AddComponent<Skral>();
        skral.TokenName = Type;
        Cell cell = Cell.FromId(cellID);
        // Check if cell is occupied by another monster
        while(cell.Inventory.Enemies.Count != 0 && cell.Index != 0)
        {
            cell = cell.enemyPath;
        }
        skral.Cell = cell;


        skral.Will = 6;
        skral.Strength = 6;
        skral.Reward = 4;

        return skral;
    }

    public static string Type { get => typeof(Skral).ToString(); }
}