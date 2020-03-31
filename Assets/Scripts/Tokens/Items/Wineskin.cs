using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wineskin : SmallToken
{
    public static string name = "Wineskin";
    public static string desc = "Each side of the wineskin can be used to advance 1 space without having to move the time marker.";
    public PhotonView photonView;

    public static Wineskin Factory()
    {
      GameObject wineSkinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Wineskin", Vector3.zero, Quaternion.identity, 0);
      return wineSkinGO.GetComponent<Wineskin>();
    }

    public static Wineskin Factory(int cellID)
    {
        object[] myCustomInitData = {cellID};
        GameObject WineskinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Wineskin", Vector3.zero, Quaternion.identity, 0, myCustomInitData);
        return WineskinGO.GetComponent<Wineskin>();
    }

    public void onEnable(){
      object[] data = photonView.InstantiationData;
      if(data == null){
        return;
      }
      this.Cell = Cell.FromId((int)data[0]);
    }
    public static string Type { get => typeof(Wineskin).ToString(); }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;

        if(hero.heroInventory.numOfGold >= cost) {
          Wineskin toAdd = Wineskin.Factory();
          if(hero.heroInventory.AddSmallToken(toAdd)){
            hero.heroInventory.RemoveGold(cost);
          }
        }
   }
}
