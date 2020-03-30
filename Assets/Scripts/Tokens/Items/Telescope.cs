using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : SmallToken
{
    public static string name = "Telescope";
    public static string desc = "The telescope can be used to d reveal all hidden tokens on adjacent spaces.";


    public PhotonView photonView;

    public static Telescope Factory()
    {
        GameObject telescopeGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Telescope", Vector3.zero, Quaternion.identity, 0);
        return telescopeGO.GetComponent<Telescope>();
    }

    public static Telescope Factory(int cellID)
    {
        object[] myCustomInitData = {cellID};
        GameObject telescopeGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Teescope", Vector3.zero, Quaternion.identity, 0, myCustomInitData);
        return telescopeGO.GetComponent<Telescope>();
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
          SmallToken toAdd = Telescope.Factory();
          if(hero.heroInventory.AddSmallToken(toAdd)){
            hero.heroInventory.RemoveGold(cost);
          }
        }
   }
}
