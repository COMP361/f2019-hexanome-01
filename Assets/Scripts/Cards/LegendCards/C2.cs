using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2 : LegendCard
{
    public C2()
    {
        this.id = "C2";

        this.story = "The king's scouts have discovered the skarl stronghold. " +
            "Rumors are circulating about cruel wardraks from the south. " +
            "They have not yet been sighted, but more and more farmers are losing their courage, leaving their farmsteads, and seeking safety in the castle." +
        "But there's good news from the south too: Prince Thorald, just back from a battle on the edge of the southern forest, is preparing himself to help the heroes.";

        this.additionalInfo = "A player can move Prince Thorald up to 4 spaces multiple times costing 1 hour each time. " +
            "Prince Thorald accompanies the heroes up to letter G on the Legend Track.";

        this.task = "Legend goals: \n-Bring the medicinal herb to the castle before the Narrator reaches the castle.\n" +
            "-Defend the castle.\n" +
            "-Defeat the skral on the tower.";
    }

    public override void ApplyEffect()
    {
        GameManager.instance.gors.Add(Gor.Factory(27));
        GameManager.instance.gors.Add(Gor.Factory(31));
        GameManager.instance.skrals.Add(Skral.Factory(29));
        GameManager.instance.thorald = Thorald.Instance;
    }
}
