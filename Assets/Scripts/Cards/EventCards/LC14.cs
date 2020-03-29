using UnityEngine;
using System.Collections.Generic;

public class LC14 : EventCard {
    
    public LC14() {
        id = 14;
        intro = "A fragment of a very old sculpture has been found. Not all of the heroes are able to appreciate that kind of handiwork.";
        effect = "The dwarf and the warrior immediately get 3 willpower points each.";
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.GetType().IsCompatibleWith(typeof(Dwarf)) || hero.GetType().IsCompatibleWith(typeof(Warrior))) hero.Willpower += 3;
        }
    }
}