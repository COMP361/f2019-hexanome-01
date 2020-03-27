using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Item
{

      public PhotonView photonView;

      /*public static GoldCoin Factory()
      {

          Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
          GameObject go = new GameObject("GoldCoin");
          SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
          renderer.sprite = sprite;
          renderer.sortingOrder = 2;

          GoldCoin goldCoin = go.AddComponent<GoldCoin>();
          goldCoin.TokenName = Type;

          return goldCoin;
      }*/

      /*public static GoldCoin Factory(int cellID){
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
      }*/

      public void Awake() {
        TokenName = Type;
      }

      public void useCell(){
        EventManager.TriggerCellGoldClick(this);
      }
      public void useHero(){
        EventManager.TriggerHeroGoldClick(this);
      }

      public static string Type { get => typeof(GoldCoin).ToString(); }

    }
