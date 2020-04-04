using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : Token {
    public static string name = "Strength";
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
      Debug.Log("Use Strength Cell");
    }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = (hero.GetType() == typeof(Dwarf) && hero.Cell.Index == 71) ? 1 : 2;

        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            GameManager.instance.photonView.RPC("AddStrengthRPC", RpcTarget.AllViaServer, new object[] {1, hero.TokenName});
        }

        else{
          EventManager.TriggerBuyError(0);
          return;
        }
   }
}
