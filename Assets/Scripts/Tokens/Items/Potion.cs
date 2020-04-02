using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : SmallToken
{
    public static string name = "Potion";
    public static string desc = "Each side of the potion token can be used to double a hero’s dice value during a battle.";
    public PhotonView photonView;

    public static Potion Factory()
    {
        GameObject potionGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Potion", Vector3.zero, Quaternion.identity, 0);
        return potionGO.GetComponent<Potion>();
    }

    public static Potion Factory(int cellID)
    {
      Potion potion = Potion.Factory();
      potion.Cell = Cell.FromId(cellID);
      return potion;

      /*
        object[] myCustomInitData = {cellID};
        GameObject potionGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Potion",  Vector3.zero, Quaternion.identity, 0, myCustomInitData);
        return potionGO.GetComponent<Potion>();
        */
    }

    public static Potion Factory(string hero)
    {
      Potion potion = Potion.Factory();
      GameManager.instance.findHero(hero).heroInventory.AddItem(potion);
      return potion;
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

    public static string Type { get => typeof(Potion).ToString(); }

      public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = Witch.Instance.PotionPrice;

        if(hero.heroInventory.numOfGold >= cost) {
            Potion toAdd = Potion.Factory();
            if(hero.heroInventory.AddSmallToken(toAdd)){
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
