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

        Cell cell = Cell.FromId(cellID);
        // Check if cell is occupied by another monster
        while(cell.Inventory.Enemies.Count != 0 && cell.Index != 0) cell = cell.enemyPath;
        wardrak.Cell = cell;
        
        wardrak.Will = 7;
        wardrak.Strength = 10;
        wardrak.Reward = 6;

        wardrak.Dices = new int[8] {
            1, 1, 1, 1, 1, 1, 1, 
            2
        };
        
        return wardrak;
    }

    public static string Type { get => typeof(Wardrak).ToString(); }
}