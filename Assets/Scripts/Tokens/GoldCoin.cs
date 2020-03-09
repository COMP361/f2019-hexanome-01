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
          goldCoin.TokenName = Type;

          return goldCoin;
      }

      public static GoldCoin Factory(int cellID){
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
        GameObject go = new GameObject("GoldCoin");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;

        GoldCoin goldCoin = go.AddComponent<GoldCoin>();
        goldCoin.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        goldCoin.Cell = cell;

        return goldCoin;
      }

      public void useCell(){
        EventManager.TriggerCellGoldClick(this);
      }
      public void useHero(){
        Debug.Log("Use GoldCoin on here");
        EventManager.TriggerHeroGoldClick(this);
      }

      public static string Type { get => typeof(GoldCoin).ToString(); }

    }
