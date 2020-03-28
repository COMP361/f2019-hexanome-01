using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class C1 : LegendCard
{
    public bool isEasy;

    public C1(bool isEasy)
    {
        this.id = "C1";

        this.isEasy = isEasy;

        this.story = "The king's scouts have discovered the skarl stronghold. " + 
            "Rumors are circulating about cruel wardraks from the south. " +
            "They have not yet been sighted, but more and more farmers are losing their courage, leaving their farmsteads, and seeking safety in the castle.";

        this.task = "The skral on the tower must be defeated. As soon as he is defeated, the Narrator is advanced to the letter N on the Legend track.";
    }

    public override void ApplyEffect()
    {
        Random rand = new Random();
        int towerSkralCell = rand.Next(1, 6) + 50;
        GameManager.instance.towerskrals.Add(TowerSkral.Factory(towerSkralCell, 3)); //TODO set num of players
    }
}
