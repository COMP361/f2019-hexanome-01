using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : BigToken{

  public static string itemName = "Falcon";
  public static string desc = "Two heroes can exchange as many small articles, gold, or gemstones at one time as they like even if they are not standing on the same space.";
  public PhotonView photonView;


  public static Falcon Factory()
  {
    GameObject falconGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Falcon", Vector3.zero, Quaternion.identity, 0);
    return falconGO.GetComponent<Falcon>();
  }

  public static Falcon Factory(int cellID)
  {
    object[] myCustomInitData = {cellID};
    GameObject falconGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Falcon", Vector3.zero, Quaternion.identity, 0, myCustomInitData);
    return falconGO.GetComponent<Falcon>();
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    EventManager.TriggerFalconUseUI(this);
  }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = 2;

    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
        Falcon toAdd = Falcon.Factory();
        if(hero.heroInventory.AddBigToken(toAdd)){
          hero.heroInventory.RemoveGold(cost);
        }
        else{
          return;
        }
      }
      else{
        EventManager.TriggerError(0);
        return;
      }
    }
    else{
      EventManager.TriggerError(2);
      return;
    }
  }
}
