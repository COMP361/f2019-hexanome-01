using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bow : BigToken {
    public static string name = "Bow";
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
    //  EventManager.TriggerCellGoldClick(this);
    }

    public override void UseHero(){
    //  EventManager.TriggerHeroGoldClick(this);
    }


/*
    public void onEnable(){
      object[] data = photonView.InstantiationData;
      if(data == null){
        return;
      }
      this.Cell = Cell.FromId((int)data[0]);
    }
    */

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;

        if(hero.heroInventory.numOfGold >= cost) {
          Bow toAdd = Bow.Factory();
          if(hero.heroInventory.AddBigToken(toAdd)){
            hero.heroInventory.RemoveGold(cost);
          }
          else{
            EventManager.TriggerBuyError(1);
            return;
          }
        }
        else{
          EventManager.TriggerBuyError(0);
          return;
        }
   }
}
