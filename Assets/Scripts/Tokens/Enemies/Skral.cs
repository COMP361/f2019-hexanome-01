﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skral : Enemy {
    //int dices;
    static Color color = Color.black;

    public static Skral Factory(int cellID) {
        GameObject go = Geometry.Disc(Vector3.zero, color);
        Skral skral = go.AddComponent<Skral>();
        skral.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        skral.Cell = cell;

        skral.Will = 6;
        skral.Strength = 6;
        skral.Reward = 4;

        return skral;
    }

    public static string Type { get => typeof(Skral).ToString(); }
}