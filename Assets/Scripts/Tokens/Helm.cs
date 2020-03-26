using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : Token
{




      public static Helm Factory()
      {

          Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
          GameObject go = new GameObject("Helm");
          SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
          renderer.sprite = sprite;
          renderer.sortingOrder = 2;

          Helm helm = go.AddComponent<Helm>();
          helm.TokenName = Type;

          return helm;
      }

      public static Helm Factory(int cellID){
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
        GameObject go = new GameObject("Helm");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        Helm helm = go.AddComponent<Helm>();
        helm.TokenName = Type;
        helm.Cell = Cell.FromId(cellID);

        return helm;
      }

      public void useCell(){
      }
      public void useHero(){
      }

      public static string Type { get => typeof(Helm).ToString(); }

    }
