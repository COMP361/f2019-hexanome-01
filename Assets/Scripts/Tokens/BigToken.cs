using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigToken : Token
{




      public static BigToken Factory()
      {

          Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
          GameObject go = new GameObject("BigToken");
          SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
          renderer.sprite = sprite;
          renderer.sortingOrder = 2;

          BigToken bigToken = go.AddComponent<BigToken>();
          bigToken.TokenName = Type;

          return bigToken;
      }

      public static BigToken Factory(int cellID){
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
        GameObject go = new GameObject("BigToken");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        BigToken bigToken = go.AddComponent<BigToken>();
        bigToken.TokenName = Type;
        bigToken.Cell = Cell.FromId(cellID);

        return bigToken;
      }

      public void useCell(){
      }
      public void useHero(){
      }

      public static string Type { get => typeof(BigToken).ToString(); }

    }
