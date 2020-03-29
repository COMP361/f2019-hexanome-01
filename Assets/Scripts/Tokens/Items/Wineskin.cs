using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wineskin : SmallToken
{
    public static string name = "Wineskin";
    public static string desc = "Each side of the wineskin can be used to advance 1 space without having to move the time marker.";

    public static Wineskin Factory()
    {
      GameObject wineSkinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Wineskin", Vector3.zero, Quaternion.identity, 0);
      return wineSkinGO.GetComponent<Wineskin>();
    }

    public static Wineskin Factory(int cellID)
    {
        Wineskin wineskin = Wineskin.Factory();
        wineskin.Cell = Cell.FromId(cellID);
        return wineskin;
    }

    public static string Type { get => typeof(Wineskin).ToString(); }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            Token wineskin = Wineskin.Factory();
            GameManager.instance.CurrentPlayer.heroInventory.AddItem(wineskin);      
        }
   }
}
