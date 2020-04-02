using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Token
{
  public PhotonView photonView;

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

public void OnEnable(){
}

  public void Awake() {
    TokenName = Type;
  }

  public override void UseCell(){
    EventManager.TriggerCellGoldClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroGoldClick(this);
  }

  public static string Type { get => typeof(GoldCoin).ToString(); }
}
