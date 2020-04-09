using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Token
{
  public static string name = "GoldCoin";
  public static string desc = "This is a Gold Coin! You can use it to buy things.";
  public PhotonView photonView;

  public void Awake() {
    TokenName = Type;
  }

  public static GoldCoin Factory() {
    GameObject goldCoinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/GoldCoin", Vector3.zero, Quaternion.identity, 0);
    return goldCoinGO.GetComponent<GoldCoin>();
  }

  public static GoldCoin Factory(int cellID)
  {
    GoldCoin goldCoin = GoldCoin.Factory();
    goldCoin.Cell = Cell.FromId(cellID);
    return goldCoin;
  }

  public static GoldCoin Factory(string hero)
  {
    GoldCoin goldCoin = GoldCoin.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(goldCoin);
    return goldCoin;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public static string Type { get => typeof(GoldCoin).ToString(); }

}
