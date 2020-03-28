using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2 : LegendCard
{
    public C2()
    {
        this.id = "C2";

        this.story = "But there's good news from the south too: Prince Thorald, just back from a battle on the edge of the southern forest, is preparing himself to help the heroes.";

        this.additionalInfo = "A player can move Prince Thorald up to 4 spaces multiple times costing 1 hour each time. " +
            "Prince Thorald accompanies the heroes up to letter G on the Legend Track.";

        this.task = "Legend goals: \n-Bring the medicinal herb to the castle before the Narrator reaches the castle.\n" +
            "-Defend the castle.\n" +
            "-Defeat the skral on the tower.";
    }

    public override void ApplyEffect()
    {
        //throw new System.NotImplementedException();
    }
}
