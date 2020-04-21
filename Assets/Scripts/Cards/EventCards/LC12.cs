using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class LC12 : EventCard {
    List<int> cellsID = new List<int>(){ 8, 9, 11, 12, 13, 15, 16, 26, 27, 28, 29, 30, 31, 32, 33,
        37, 38, 39, 40, 41, 42, 44, 46, 47, 48, 49, 50, 56, 63, 64 };

    public LC12() {
        id = 12;
        intro = "The heroes replenish their water supplies at the river.";
        effect = "Each hero who is now standing on a space bordering the river gets a wineskin.";
    }

    public override void ApplyEffect() {
      if(PhotonNetwork.IsMasterClient){
        foreach(Hero hero in GameManager.instance.heroes) {
            if(cellsID.Contains(hero.Cell.Index)) {
                SmallToken wineskin = Wineskin.Factory();
                hero.heroInventory.AddSmallToken(wineskin);
            }
        }
      }
    }
}
