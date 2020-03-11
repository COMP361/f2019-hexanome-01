using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : Token
{


      public static Well Factory()
      {

          Sprite sprite = Resources.Load<Sprite>("Sprites/icons/well-full");
          GameObject go = new GameObject("Well");
          SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
          renderer.sprite = sprite;
          renderer.sortingOrder = -2;

          Well well = go.AddComponent<Well>();
          well.TokenName = Type;
  //       Cell cell = Cell.FromId(cellID);
  //       well.Cell = cell;

          return well;
      }

      public void useCell(){
        EventManager.TriggerCellWellClick(this);
      }

      public static string Type { get => typeof(Well).ToString(); }

    }
