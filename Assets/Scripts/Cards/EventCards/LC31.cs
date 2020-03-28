using UnityEngine;
using System.Collections.Generic;

public class LC31 : EventCard {
    
    public LC31() {
        id = 31;
        intro = "Hot rain from the south lashes the land."; 
        effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
        shield = true;
    }
}