using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallToken : Token
{
  /*public static SmallToken Factory() {

    Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
    GameObject go = new GameObject("SmallToken");
    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
    renderer.sprite = sprite;
    renderer.sortingOrder = 2;

    SmallToken smallToken = go.AddComponent<SmallToken>();
    smallToken.TokenName = Type;

    return smallToken;
  }

  public static SmallToken Factory(int cellID) {
    SmallToken smallToken = SmallToken.Factory();
    smallToken.Cell = Cell.FromId(cellID);

    return smallToken;
  }*/

  public static string Type { get => typeof(SmallToken).ToString(); }

}
