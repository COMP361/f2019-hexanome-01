using UnityEngine;
using System.Collections.Generic;

public class LC2 : EventCard {

    public LC2() {
        id = 2;
        intro = "A biting wind blows across the coast from the sea. ";
        effect = "Each hero standing on a space with a number between 0 and 20 now loses 3 willpower points."; 
        shield = true;
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.Cell.Index <= 20) hero.Willpower -= 3;
        }   
    }
}
