using UnityEngine;
using System.Collections.Generic;

public class LC28 : EventCard {
    
    public LC28() {
        id = 28;
        intro = "A beautifully clear, starry night gives the heroes confidence.";
        effect = "Every hero whose time marker is presently in the sunrise box gets 2 willpower points.";
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.timeline.Index == 0) {
                hero.Willpower += 2;
            } 
        }
    }
}