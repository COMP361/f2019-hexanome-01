using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class LC29 : EventCard {

    public LC29() {
        id = 29;
        intro = "The keepers of the Tree of Songs offer a gift.";
        effect = "Now place a shield on space 57. A hero who enters space 57 or is already standing there can collect the shield. If more than one hero is standing there, the hero with the lowest rank gets the shield.";
    }

    public override void ApplyEffect() {
      if(PhotonNetwork.IsMasterClient){
        Shield.Factory(57);
      }
    }
}
