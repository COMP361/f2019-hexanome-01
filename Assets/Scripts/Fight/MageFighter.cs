using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class MageFighter : Fighter {
    public static bool hasflippedDie = false;
    public static Button flipBtn;

    void Awake() {
        flipBtn = transform.Find("Dice Actions/Flip Die").GetComponent<Button>();
        flipBtn.onClick.AddListener(delegate { MageSuperpower(); });
    }

    public override void LockRollBtns() {
        rollBtn.interactable = false;
        flipBtn.interactable = false;
        abandonBtn.interactable = true;
    }

    public static void UnlockFlipBtn() {
        flipBtn.interactable = true;
    }

    public static void LockFlipBtn() {
        flipBtn.interactable = false;
    }

    public void MageSuperpower()
    {
        hasflippedDie = true;

        int smallestdie = 6;
        regularDices dieToFlip = Fighter.lastHeroToRoll.rd[0];
        foreach (regularDices rd in Fighter.lastHeroToRoll.rd)
        {
            //Debug.Log("the smallest die is : " + smallestdie + " and the actual one rn is : "+ rd.finalSide);
            if ((rd.finalSide <= smallestdie) && rd.gameObject.activeSelf)
            {
                //Debug.Log("switched!");
                smallestdie = rd.finalSide;
                dieToFlip = rd;
            }
        }
        
        dieToFlip.FlipTheDie();
        LockFlipBtn();


        if (Fighter.lastHeroToRoll.gameObject.name.Equals("Archer"))
        {
            Fighter.lastHeroToRoll.rollBtn.interactable = false;
            Fighter.lastHeroToRoll.lastRoll = dieToFlip.finalSide;
            fight.getHeroesScore();
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
        Fighter.lastHeroToRoll.lastRoll = maxDie;
        fight.getHeroesScore();
    }

    public override void EndofRound(int loss) {
        hasflippedDie = false;
        flipBtn.interactable = false;
        base.EndofRound(loss);
    }
}