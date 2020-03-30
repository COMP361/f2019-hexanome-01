using UnityEngine;
using System.Collections.Generic;

public class LC5 : EventCard {
    
    public LC5() {
        id = 5;
        intro = "A farm girl sings a beautiful song that wafts across the northern woods. But it fails to stir the hearts of all the heroes.";
        effect = "The mage and the archer immediately get 3 willpower points each.";
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.GetType().IsCompatibleWith(typeof(Mage)) || hero.GetType().IsCompatibleWith(typeof(Archer))) hero.Willpower += 3;
        }
    }
}