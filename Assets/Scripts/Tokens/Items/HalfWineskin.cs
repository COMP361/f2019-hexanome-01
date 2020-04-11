using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfWineskin : SmallToken
{
  public PhotonView photonView;

  public void Awake() {
    TokenName = Type;
  }

  public static HalfWineskin Factory() {
    GameObject halfWineskinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/WineskinHalf", Vector3.zero, Quaternion.identity, 0);
    return halfWineskinGO.GetComponent<HalfWineskin>();
  }

  public static HalfWineskin Factory(string hero)
  {
    HalfWineskin halfWineskin = HalfWineskin.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(halfWineskin);
    return halfWineskin;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    Debug.Log("Use HalfWineskin Effect");
}

  public static string Type { get => typeof(HalfWineskin).ToString(); }
}
