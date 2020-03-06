using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Enemy {
    //int dices;
    static Color color = Color.black;

    public static Troll Factory(int cellID) {
        GameObject go = Geometry.Disc(Vector3.zero, color);
        go.transform.localScale = new Vector3(10, 10, 10);
        
        Troll troll = go.AddComponent<Troll>();
        troll.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        troll.Cell = cell;

        troll.Will = 12;
        troll.Strength = 14;
        troll.Reward = 6;

        return troll;
    }

   public static string Type { get => typeof(Troll).ToString(); }
}