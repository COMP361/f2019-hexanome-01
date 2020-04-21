using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;


public class LC30 : EventCard {

    public LC30() {
        id = 30;
        intro = "A drink in the tavern.";
        effect = "Place a wineskin on the tavern space (72). A hero who enters space 72 or is already standing there can collect the wineskin.";
    }

    public override void ApplyEffect() {
        //Token wineskin = Wineskin.Factory();
        //Cell.FromId(72).Inventory.AddSmallToken(wineskin);
        if(PhotonNetwork.IsMasterClient){
          Wineskin.Factory(72);
        }
    }
}
