using UnityEngine;
using System.Collections.Generic;

public class LC25 : EventCard {
    List<int> cellsID = new List<int>(){ 0, 22, 23, 24, 25, 49, 48, 47, 56, 63, 58, 62, 51, 53, 54, 50, 52, 55, 57, 59, 60, 71, 72 };  

    public LC25() {
        id = 25;
        intro = "A storm moves across the countryside and weighs upon the mood of the heroes.";
        effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
        shield = true;
    }

    public override void ApplyEffect() {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(!cellsID.Contains(hero.Cell.Index)) {
                hero.Willpower -= 2;
            } 
        }
    }
}