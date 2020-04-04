using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPotion : SmallToken
{
  public PhotonView photonView;

  public static HalfPotion Factory() {
    GameObject halfPotionGO = PhotonNetwork.Instantiate("Prefabs/Tokens/HalfPotion", Vector3.zero, Quaternion.identity, 0);
    return halfPotionGO.GetComponent<HalfPotion>();
  }

  public static HalfPotion Factory(string hero)
  {
    HalfPotion halfPotion = HalfPotion.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(halfPotion);
    return halfPotion;
  }

public void OnEnable(){
}

  public void Awake() {
    TokenName = Type;
  }

  public override void UseCell(){
    Debug.Log("Use HalfPotion Cell");
  }

  public override void UseHero(){
    Debug.Log("Use HalfPotion Hero");
  }

  public override void UseEffect(){
      Debug.Log("Use HalfPotion Effect");
}

  public static string Type { get => typeof(HalfPotion).ToString(); }
}
