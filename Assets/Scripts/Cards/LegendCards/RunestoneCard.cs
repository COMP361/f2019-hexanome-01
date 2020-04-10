using Photon.Pun;
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
        if (PhotonNetwork.IsMasterClient)
        {
            int[] runestoneCells = GameManager.instance.narrator.runestoneCells;

            int firstRunestoneCell = runestoneCells[0];
            int secondRunestoneCell = runestoneCells[1];
            int thirdRunestoneCell = runestoneCells[2];
            int fourthRunestoneCell = runestoneCells[3];
            int fifthRunestoneCell = runestoneCells[4];
            //Debug.Log(firstRunestoneCell + "  " + secondRunestoneCell + "  " + thirdRunestoneCell + "  " + fourthRunestoneCell + "  " + fifthRunestoneCell);
            Runestone runestone1 = Runestone.Factory(firstRunestoneCell);
            Runestone runestone2 = Runestone.Factory(secondRunestoneCell);
            Runestone runestone3 = Runestone.Factory(thirdRunestoneCell);
            Runestone runestone4 = Runestone.Factory(fourthRunestoneCell);
            Runestone runestone5 = Runestone.Factory(fifthRunestoneCell);
        }
    }
}

