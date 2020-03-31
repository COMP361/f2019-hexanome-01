using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield: BigToken {
    public static string name = "Shield";
    public static string desc = "Each side of the shield can be used once to help avoiding losing willpower points after a battle round, or against an event card.";

    public PhotonView photonView;


    public static Shield Factory()
    {
        GameObject shieldGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Shield", Vector3.zero, Quaternion.identity, 0);
        return shieldGO.GetComponent<Shield>();
    }

    public static Shield Factory(int cellID)
    {
        object[] myCustomInitData = {cellID};
        GameObject shieldGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Shield", Vector3.zero, Quaternion.identity, 0, myCustomInitData);
        return shieldGO.GetComponent<Shield>();
    }

    public void onEnable(){
      object[] data = photonView.InstantiationData;
      if(data == null){
        return;
      }
      this.Cell = Cell.FromId((int)data[0]);
    }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;

        if(hero.heroInventory.numOfGold >= cost) {
          Shield toAdd = Shield.Factory();
          if(hero.heroInventory.AddBigToken(toAdd)){
            hero.heroInventory.RemoveGold(cost);
          }


        }
   }
}
