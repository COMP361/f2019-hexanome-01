using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Token
{

//  protected static GameObject goldCoin;


      public static GoldCoin Factory()
      {

          Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
          GameObject go = new GameObject("GoldCoin");
          SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
          renderer.sprite = sprite;
          renderer.sortingOrder = 2;

          GoldCoin goldCoin = go.AddComponent<GoldCoin>();
          goldCoin.TokenName = "GoldCoin";

      //    Cell cell = Cell.FromId(cellID);
    //      goldCoin.Cell = cell;

          return goldCoin;
      }

    }
