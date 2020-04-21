using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System;

public class MageFighter : Fighter {
    public static bool hasflippedDie = false;
    public static Text flipMessage;
    public Button flipBtn;

    void Awake() {
        flipBtn.onClick.AddListener(delegate { MageSuperpower(); });
        base.Awake();
    }

    public void MageSuperpower()
    {
        /*if (!fighters.Contains(mage))
        {
            flipMessage.text = "No wizard, Thus no flip!";
            return;
        }*/

        if (hasflippedDie)
        {
            flipMessage.text = "Already used the superpower this round.";
            return;
        }

        if (Fighter.lastHeroToRoll == null || !Fighter.lastHeroToRoll.hasRolled)
        {
            flipMessage.text = "Nobody rolled their dice yet!";
        }

        int smallestdie = 6;
        regularDices dieToFlip = Fighter.lastHeroToRoll.rd[0];
        foreach (regularDices rd in Fighter.lastHeroToRoll.rd)
        {
            if ((rd.finalSide <= smallestdie) && rd.gameObject.activeSelf)
            {
                dieToFlip = rd;
            }
        }
        dieToFlip.OnflipDie();

        if (Fighter.lastHeroToRoll.name.text.Equals("Archer"))
        {
            Fighter.lastHeroToRoll.hasRolled = true;
            Fighter.lastHeroToRoll.lastRoll = dieToFlip.finalSide;
            return;
        }

        regularDices[] activeDice = new regularDices[Fighter.lastHeroToRoll.hero.Dices[Fighter.lastHeroToRoll.hero.Willpower]];
        int maxDie;
        int i = 0;

        foreach (regularDices rd in Fighter.lastHeroToRoll.rd)
        {
            if (rd.gameObject.activeSelf)
            {
                activeDice[i] = rd;
                i++;
            }
        }
        maxDie = Fighter.getMaxValue(activeDice);

        Fighter.lastHeroToRoll.hasRolled = true;
        Fighter.lastHeroToRoll.lastRoll = maxDie;
    }

    public void EndofRound() {
        //restore the flipped thingy
        flipMessage.text = "";
        hasflippedDie = false;

        base.EndofRound();
    }
}