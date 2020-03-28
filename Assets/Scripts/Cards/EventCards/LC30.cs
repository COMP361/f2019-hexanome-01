using UnityEngine;
using System.Collections.Generic;

public class LC30 : EventCard {
    
    public LC30() {
        id = 30;
        intro = "A drink in the tavern."; 
        effect = "Place a wineskin on the tavern space (72). A hero who enters space 72 or is already standing there can collect the wineskin. If more than one hero is standing there, the hero with the lowest rank gets the wineskin.";
    }
}