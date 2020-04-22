using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System;

public class Fighter : MonoBehaviour {
    public Hero hero;
    public Text strength;
    public Text wp;
    public regularDices[] rd; //= new regularDices[4];
    public specialDices sd;
    public bool hasRolled = false;
    public int lastRoll = -1;
    public Button potion;
    public double nb_potion = 0; // SET TO ZERO IF NOT TESTING
    public Text potionText;
    public Button helm;
    public int nb_helm = 0;
    public Button shield;
    public double nb_shield = 0;
    public Text shieldText;
    public bool useShield = false;
    public int gems = 0;
    public static Fighter lastHeroToRoll;
    public Button rollBtn, abandonBtn;
    public MultiplayerFightPlayer fight;
    public int rollCount = 0;
    public int maxRollCount = 1;
    

    protected void Awake() {
        rollBtn.onClick.AddListener(delegate { RollDice(); });
        abandonBtn.onClick.AddListener(delegate { AbandonFight(); });
    }

    public virtual void Init(Hero hero) {
        this.hero = hero;
        strength.text = hero.Strength.ToString();
        wp.text = hero.Willpower.ToString();
        
        for (int i = 0; i < hero.Dices[hero.Willpower] && i < rd.Length; i++) {
            rd[i].gameObject.SetActive(true);
        }

        foreach (var t in hero.heroInventory.smallTokens) {
            if (t is Potion) {
                this.nb_potion++;
            }
            if(t is Runestone) {
                this.gems++;
            }
        }

        if(this.gems > 2) {
            this.sd.gameObject.SetActive(true);
        }

        foreach(var t in hero.heroInventory.AllTokens) {
            if(t is Helm) {
                this.nb_helm += 1;
            }
            if(t is Shield) {
                this.nb_shield += 1;
            }
        }
    }

    public void EndofRound() {
        lastHeroToRoll = null;
        // Need to re-initialize the count of rolls.
        rollCount = 0;
    }

    public virtual int RollDice() {
        if (hasRolled) {
            return lastRoll;
        }

        lastHeroToRoll = this;

        regularDices[] activeDice = new regularDices[hero.Dices[hero.Willpower]];
        int maxDie;
        int i = 0;
        foreach (regularDices rd in rd)
        {
            if (rd.gameObject.activeSelf)
            {
                rd.OnMouseDown();
                activeDice[i] = rd;
                i++;
            }
        }

        maxDie = getMaxValue(activeDice);

        hasRolled = true;
        lastRoll = maxDie;

        return maxDie;
    }

    public int RollDiceWithBow() {
        if (rollCount < maxRollCount) {
            foreach (regularDices dice in rd) {
                dice.gameObject.SetActive(false);
            }
            
            rd[0].gameObject.SetActive(true);
            rd[0].OnMouseDown();

            lastRoll = rd[0].getFinalSide();
            hasRolled = true;
            rollCount++;
            lastHeroToRoll = this;
        }

        return lastRoll;
    }

    public static int getMaxValue(regularDices[] rdList) {
        int max = 0;
        foreach (regularDices dice in rdList)
        {
            int cur = dice.getFinalSide();
            if (cur > max)
            {
                max = cur;
            }
        }
        return max;
    }

    private void HasPotion()
    {
        if (nb_potion > 0) {
            potion.GetComponent<Button>().enabled = true; 
        }
    }

    private void UsePotion()
    {
        if (nb_potion > 0 && lastRoll != -1)
        {
            potion.GetComponent<Image>().color = potion.GetComponent<Button>().colors.pressedColor;
            lastRoll = lastRoll * 2;
            nb_potion -= 0.5;
            potionText.text = nb_potion.ToString();
            fight.AttackMessage.text = "";
        }
        else if (nb_potion > 0 && lastRoll == -1)
        {
        fight.AttackMessage.text = "Roll before using the potion!";
        }
        else
        {
            potion.GetComponent<Image>().color = potion.GetComponent<Button>().colors.normalColor;
            potion.enabled = false;
            fight.AttackMessage.text = "";
        }
    }

    private void UseHelm() {
        if (nb_helm > 0 && lastRoll != -1)
        {
            potion.GetComponent<Image>().color = potion.GetComponent<Button>().colors.pressedColor;
            lastRoll = lastRoll * 2;
            nb_helm -= 1;
            potionText.text = nb_helm.ToString();
            fight.AttackMessage.text = "";
        } else if (nb_helm > 0 && lastRoll == -1) {
            fight.AttackMessage.text = "Roll before using the helm!";
        } else {
            helm.GetComponent<Image>().color = helm.GetComponent<Button>().colors.normalColor;
            helm.enabled = false;
            fight.AttackMessage.text = "";
        }
    }

    public void DisableFighter() {
        rollBtn.interactable = false;
    }


    public void AbandonFight()
    {
        DisableFighter();
        fight.RemoveFromFight(this);
    }
}