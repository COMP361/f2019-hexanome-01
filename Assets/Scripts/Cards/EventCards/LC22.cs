using UnityEngine;
using System.Collections.Generic;

public class LC22 : EventCard {
    
    public LC22() {
        id = 22;
        intro = "Rampaging creatures despoil the well at the foot of the mountains.";
        effect = "The well token on space 45 is removed from the game.";
        shield = true;
    }
}