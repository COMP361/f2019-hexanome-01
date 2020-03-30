using UnityEngine;
using System.Collections.Generic;

public class LC13 : EventCard {
    
    public LC13() {
        id = 13;
        intro = "The lovely sound of a horn echoes across the land.";
        effect = "Each hero who has fewer than 10 willpower points can immediately raise his total to 10.";
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.Willpower < 10) {
                hero.Willpower = 10;
            } 
        }
    }
}