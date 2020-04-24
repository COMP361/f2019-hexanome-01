using UnityEngine;
using System.Collections.Generic;

public class LC15 : EventCard {
    
    public LC15() {
        id = 15;
        intro = "Rampaging creatures despoil the well in the south of Andor.";
        effect = "The well token on space 35 is removed from the game.";
        shield = true;
    }

    public override void ApplyEffect() {
        for(int i = 0; i < GameManager.instance.wells.Count; i++) {
            if(GameManager.instance.wells[i].Cell.Index == 35) {
                GameManager.instance.wells[i].DestroyWell();
            }
        }
    }
}