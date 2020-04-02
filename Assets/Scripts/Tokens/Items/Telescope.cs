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
      Telescope telescope = Telescope.Factory();
      telescope.Cell = Cell.FromId(cellID);
      return telescope;
    }

    public static Telescope Factory(string hero)
    {
      Telescope telescope = Telescope.Factory();
      GameManager.instance.findHero(hero).heroInventory.AddItem(telescope);
      return telescope;
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
          Telescope toAdd = Telescope.Factory();
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
