using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : Token {
  public static string itemName = "Strength";
  public static string desc = "A strength point helps you battle monsters by making you stronger.";

  public PhotonView photonView;

  public static Strength Factory()
  {
    GameObject strengthGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Strength", Vector3.zero, Quaternion.identity, 0);
    return strengthGO.GetComponent<Strength>();
  }

  public static Strength Factory(int cellID)
  {
    Strength strength = Strength.Factory();
    strength.Cell = Cell.FromId(cellID);
    return strength;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = (hero.GetType() == typeof(Dwarf) && hero.Cell.Index == 71) ? 1 : 2;

    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
          hero.heroInventory.RemoveGold(cost);
          hero.setStrength((hero.Strength + 1));
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
