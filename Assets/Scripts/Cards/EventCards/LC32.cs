using UnityEngine;
using System.Collections.Generic;

public class LC32 : EventCard {
    
    public LC32() {
        id = 32;
        intro = "A sleepless night awaits the heroes."; 
        effect = "Every hero whose time marker is presently in the sunrise box loses 2 willpower points.";
        shield = true;
    }
}