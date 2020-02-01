using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Movable, IEnemy {
    //int dices;
    static Color color = Color.black;

    public static Troll Factory(int cellID) {
        GameObject go = Geometry.Disc(Vector3.zero, color);
        Troll troll = go.AddComponent<Troll>();
        troll.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        troll.Cell = cell;

        cell.State.addEnemy(troll);

        troll.Will = 12;
        troll.Strength = 14;
        troll.Reward = 6;

        return troll;
    }


    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public static string Type { get => typeof(Troll).ToString(); }
}