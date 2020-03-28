using UnityEngine;
using System.Collections.Generic;

public class LC17 : EventCard {
    
    public LC17() {
        id = 17;
        intro = "Heavy weather moves across the land.";
        effect = "Each hero with more than 12 willpower points immediately reduces his point total to 12.";
        shield = true;
    }
}