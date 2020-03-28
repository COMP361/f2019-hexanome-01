using UnityEngine;
using System.Collections.Generic;

public class LC25 : EventCard {
    
    public LC25() {
        id = 25;
        intro = "A storm moves across the countryside and weighs upon the mood of teh heroes.";
        effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
        shield = true;
    }
}