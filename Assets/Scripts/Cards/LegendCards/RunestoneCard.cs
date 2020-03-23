using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunestoneCard : LegendCard
{
    public bool isEasy;
    public bool isWitchFound;

    public RunestoneCard(bool isEasy)
    {
        this.id = "RunestoneCard";

        this.isEasy = isEasy;

        if (isEasy)
        {
            this.story = "The heroes learn about an ancient magic that still holds power: rune stones!";
        }
        else
        {
            this.story = "The witch Reka tells the heroes about an ancient magic that still holds power: rune stones!";
        }

        this.effect = "If a hero has 3 different coloured rune stones on his hero board he gets one black die which has higher values than the hero dice. " +
            "As long as he possesses the runestones, he is allowed to use this black die in battle instead of his own die.";
    }

    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}

