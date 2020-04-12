using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BigToken {
  public static string itemName = "Shield";
  public static string desc = "Each side of the shield can be used once to help avoiding losing willpower points after a battle round, or against an event card.";
  public PhotonView photonView;

  public static Shield Factory()
  {
    GameObject shieldGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Shield", Vector3.zero, Quaternion.identity, 0);
    Shield shield = shieldGO.GetComponent<Shield>();
    shield.Cell = null;
    return shield;
  }

  public static Shield Factory(int cellID)
  {
    Shield shield = Shield.Factory();
    shield.Cell = Cell.FromId(cellID);
    return shield;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    BigToken bigToken = GameManager.instance.MainHero.heroInventory.bigToken;
    if(!bigToken is Shield || bigToken.GetComponent<PhotonView>().ViewID != GetComponent<PhotonView>().ViewID) return;

    HalfShield shield = HalfShield.Factory();
    GameManager.instance.MainHero.heroInventory.ReplaceBigToken((BigToken)this, shield, true);
  }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = 2;
    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
        Shield toAdd = Shield.Factory();
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