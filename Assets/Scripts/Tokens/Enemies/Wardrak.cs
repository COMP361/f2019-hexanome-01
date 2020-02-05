using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrak : Enemy {
    //int dices;
    static Color color = Color.black;

    public static Wardrak Factory(int cellID) {
        GameObject go = Geometry.Disc(Vector3.zero, color);
        Wardrak wardrak = go.AddComponent<Wardrak>();
        wardrak.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        wardrak.Cell = cell;
        
        wardrak.Will = 7;
        wardrak.Strength = 10;
        wardrak.Reward = 6;

        return wardrak;
    }

    public static string Type { get => typeof(Wardrak).ToString(); }
}