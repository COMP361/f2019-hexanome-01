using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : Token
{

  public static string name = "Well";
  public static string desc = "This is a Well! Pick it up to get willpower points.";

  public static Well Factory() {

    Sprite sprite = Resources.Load<Sprite>("Sprites/icons/well-full");
    GameObject go = new GameObject("Well");
    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
    renderer.sprite = sprite;
    renderer.sortingOrder = 2;

    Well well = go.AddComponent<Well>();
    well.TokenName = Type;
    return well;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public static string Type { get => typeof(Well).ToString(); }

}
