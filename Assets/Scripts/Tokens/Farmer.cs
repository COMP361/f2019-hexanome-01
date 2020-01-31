using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : Movable {
  static Color color = Color.white;

  public static Farmer Factory(int cellID) {
    GameObject go = Geometry.Disc(Vector3.zero, color);
    Farmer farmer = go.AddComponent<Farmer>();
    farmer.TokenName = Type;

    Cell cell = Cell.FromId(cellID);
    farmer.Cell = cell;

    return farmer;
  }

  public static string Type { get => typeof(Troll).ToString(); }
}
