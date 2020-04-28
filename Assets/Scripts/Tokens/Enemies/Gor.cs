using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gor : Enemy
{
    static Color color = Color.black;
    public static bool quitting = false;

    void OnApplicationQuit() {
        quitting = true;
    }

    public static Gor Factory(int cellID)
    {

        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Gor");
        GameObject go = new GameObject("Gor");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        Gor gor = go.AddComponent<Gor>();
        gor.TokenName = Type;
        
        Cell cell = Cell.FromId(cellID);
        // Check if cell is occupied by another monster
        while(cell.Inventory.Enemies.Count != 0 && cell.Index != 0)
        {
            cell = cell.enemyPath;
        }
        gor.Cell = cell;

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }

    protected void OnDestroy() {
        if(quitting) return;
        Cell = null;
        GameManager.instance.gors.Remove(this);
    }

    public static string Type { get => typeof(Gor).ToString(); }
}
