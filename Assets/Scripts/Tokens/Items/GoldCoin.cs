using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : SmallToken
{
  public PhotonView photonView;

  public static SmallToken Factory() {
    GameObject goldCoinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/GoldCoin", Vector3.zero, Quaternion.identity, 0);
    return goldCoinGO.GetComponent<GoldCoin>();
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