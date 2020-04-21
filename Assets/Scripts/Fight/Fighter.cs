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
    public Text name;
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
    

    protected void Awake() {
        rollBtn.onClick.AddListener(delegate { RollDice(); });
        abandonBtn.onClick.AddListener(delegate { AbandonFight(); });
    }

    public void Init(Hero hero) {
        this.hero = hero;
        strength.text = hero.Strength.ToString();
        wp.text = hero.Willpower.ToString();
        
        Debug.Log(hero.TokenName);
        Debug.Log(hero.Dices[hero.Willpower]);


        for (int i = 0; i < hero.Dices[hero.Willpower]; i++) {
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

    public int RollDice() {
        if (hasRolled) {
            return lastRoll;
        }

        Fighter.lastHeroToRoll = this;

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
        foreach (regularDices rd in rd) {
            gameObject.SetActive(false);
        }

        strength.text = "";
        wp.text = "";
        
        gameObject.SetActive(false);
    }


    public void AbandonFight()
    {
        DisableFighter();
        fight.  RemoveFromFight(this);
    }
}