using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : MonoBehaviour {
  Token token;
  static Color color = Color.white;

  public static Farmer Factory(int cellID) {
    Token token = Token.Factory("Farmer", color);
    token.Position(Cell.FromId(cellID));
    Farmer farmer = token.gameObject.AddComponent<Farmer>();
    farmer.token = token;

    return farmer;
  }
}
