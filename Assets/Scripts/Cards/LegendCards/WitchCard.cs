using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCard : LegendCard
{
    public int herbCell;

    public WitchCard()
    {
        this.id = "WitchCard";

        this.story = "Finally! There in the fog, one of the heroes discovers the witch named Reka. " +
            "Reka knows where to find the medicinal herb to heal the king.";

        this.effect = "The hero standing on the witch's space activates the fog token and gets her magic potion for free. " +
            "From now on, the players can purchase witch's brews. " +
            "It doubles the values of one's die roll and each potion can be used twice.";

        this.task = "When the Narrator reaches the letter N on the Legend track, the medicinal herb must be in the castle.";
    }

    public override void ApplyEffect()
    {
        GameManager.instance.gors.Add(HerbGor.Factory(GameManager.instance.narrator.herbCellId));
    }
}
