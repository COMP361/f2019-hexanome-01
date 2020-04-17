using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : SmallToken
{
  public static string itemName = "Potion";
  public static string desc = "Each side of the potion token can be used to double a hero’s dice value during a battle.";
  public PhotonView photonView;

  public static Potion Factory()
  {
    GameObject potionGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Potion", Vector3.zero, Quaternion.identity, 0);
    Potion potion = potionGO.GetComponent<Potion>();
    potion.Cell = null;
    potion.maxUse = 2;
    return potion;
  }

  public static Potion Factory(int cellID)
  {
    Potion potion = Potion.Factory();
    potion.Cell = Cell.FromId(cellID);
    return potion;
  }

  public static Potion Factory(string hero)
  {
    Potion potion = Potion.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(potion);
    return potion;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    Debug.Log("Use Potion Effect");
  }

  public static string Type { get => typeof(Potion).ToString(); }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = Witch.Instance.PotionPrice;
    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
          Potion toAdd = Potion.Factory();
          if(hero.heroInventory.AddSmallToken(toAdd)){
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
