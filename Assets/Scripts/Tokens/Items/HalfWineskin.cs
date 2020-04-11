using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfWineskin : SmallToken
{

  public static string name = "Half-Wineskin";
  public static string desc = "Each side of the wineskin can be used to advance 1 space without having to move the time marker.";
  public PhotonView photonView;


  public void Awake() {
    TokenName = Type;
  }

  public static HalfWineskin Factory() {
    GameObject halfWineskinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/HalfWineskin", Vector3.zero, Quaternion.identity, 0);
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
    if(!InUse){
      InUse = true;
      EventManager.TriggerFreeMove(this);
    }
    else{
      EventManager.TriggerError(3);
    }
  }

  public override void HowManyFreeMoves(int pathSize){
  EventManager.TriggerFreeMoveUI(this, pathSize);
  }

  public static string Type { get => typeof(HalfWineskin).ToString(); }
}
