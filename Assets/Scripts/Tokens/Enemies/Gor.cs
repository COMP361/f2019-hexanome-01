using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gor : Movable, IEnemy {
    static Color color = Color.black;

    public static Gor Factory(int cellID) {
        GameObject go = Geometry.Disc(Vector3.zero, color);
        Gor gor = go.AddComponent<Gor>();
        gor.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        gor.Cell = cell;

        cell.State.addEnemy(gor);

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public static string Type { get => typeof(Gor).ToString(); }
}
