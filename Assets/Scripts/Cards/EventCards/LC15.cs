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
        Well w = Cell.FromId(35).Inventory.Well;
        if(w != null) {
            w.DestroyWell();
        }
    }
}