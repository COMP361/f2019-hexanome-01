using UnityEngine;
using System.Collections.Generic;

public class LC4 : EventCard {
    
    public LC4() {
        id = 4;
        intro = "Poisonous vapors from the mountains are tormenting the heroes.";
        effect = "Each hero standing on a space with a number between 37 and 70 now loses 3 willpower points.";
        shield = true; 
    }
}