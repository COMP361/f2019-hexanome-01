using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BigToken {
    public static string itemName = "Bow";
    public static string desc = "A hero with a bow may attack a creature in an adjacent space.";

    public PhotonView photonView;

    public static Bow Factory()
    {
        GameObject bowGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Bow", Vector3.zero, Quaternion.identity, 0);
        return bowGO.GetComponent<Bow>();
    }

    public static Bow Factory(int cellID)
    {
      Bow bow = Bow.Factory();
      bow.Cell = Cell.FromId(cellID);
      return bow;
    }

    public static Bow Factory(string hero)
    {
      Bow bow = Bow.Factory();
      GameManager.instance.findHero(hero).heroInventory.AddItem(bow);
      return bow;
    }

    public override void UseCell(){
      EventManager.TriggerCellItemClick(this);
    }

    public override void UseHero(){
      EventManager.TriggerHeroItemClick(this);
    }

    public override void UseEffect(){
      Debug.Log("Use Bow Effect");
    }

    public static void Buy() {
      Hero hero = GameManager.instance.MainHero;
      int cost = 2;

      if(hero.timeline.Index != 0){
        if(hero.heroInventory.numOfGold >= cost) {
          Bow toAdd = Bow.Factory();
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
