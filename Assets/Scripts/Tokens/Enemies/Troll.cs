using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Enemy {
    //int dices;
    static Color color = Color.black;

    public static Troll Factory(int cellID) {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Troll");
        GameObject go = new GameObject("Troll");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        Troll troll = go.AddComponent<Troll>();
        troll.TokenName = Type;
        Cell cell = Cell.FromId(cellID);
        // Check if cell is occupied by another monster
        while(cell.Inventory.Enemies.Count != 0 && cell.Index != 0)
        {
            cell = cell.enemyPath;
        }
        troll.Cell = cell;

        troll.Will = 12;
        troll.Strength = 14;
        troll.Reward = 6;

        return troll;
    }

   public static string Type { get => typeof(Troll).ToString(); }
}