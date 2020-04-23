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
    public regularDices[] rd; 
    public specialDices sd;
    public bool hasSpecialDie = false;
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
    public int maxDices;
    
    void OnEnable() {
        EventManager.UpdateHeroStats += UpdatePlayerStats;
    }

    void OnDisable() {
        EventManager.UpdateHeroStats -= UpdatePlayerStats;
    }

    void Start() {
        rollBtn.onClick.AddListener(delegate { RollDice(); });
        abandonBtn.onClick.AddListener(delegate { AbandonFight(); });
    }

    public void InitDices() {
        maxDices = hero.Dices[hero.Willpower];
        if(hero.hasBow()) {
            maxRollCount = maxDices;
            maxDices = 1;
        } else {
            maxRollCount = 1;
        }

        for (int i = 0; i < rd.Length; i++) {
            if(i < maxDices) {
                rd[i].gameObject.SetActive(true);
                rd[i].ResetTheDie();
            } else {
                rd[i].gameObject.SetActive(false);
            }
        }
    }

    public virtual void Init(Hero hero) {
        this.hero = hero;
        strength.text = hero.Strength.ToString();
        wp.text = hero.Willpower.ToString();
        
        InitDices();

        List<Runestone> gms = new List<Runestone>();

        foreach (var t in hero.heroInventory.smallTokens)
        {
            if (t is Potion)
            {
                this.nb_potion++;
            }
            if (t is Runestone)
            {
                gms.Add((Runestone) t);
                this.gems++;
            }
        }
        

        if(IsFullSetGem(gms)) {
            this.hasSpecialDie = true;
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

        LockRollBtns();
    }

    public virtual void LockRollBtns() {
        rollBtn.interactable = false;
        abandonBtn.interactable = true;
    }

    public virtual void UnlockRollBtns() {
        rollBtn.interactable = true;
        abandonBtn.interactable = false;
    }


    private void UpdatePlayerStats(Hero h) {
        if(h == this.hero) {
            strength.text = h.Strength.ToString();
            wp.text = h.Willpower.ToString();
        }   
    }

    public void EndofRound() {
        MageFighter.flipBtn.interactable = false;
    }

    public int RollDice() {
        foreach(Fighter f in fight.fighters) {
            if (f.rollCount < f.maxRollCount && f.rollCount > 0 && f != this) {
                f.rollBtn.interactable = false;
            }        
        }
        if (rollCount >= maxRollCount) return lastRoll;
        if(lastHeroToRoll == null) MageFighter.UnlockFlipBtn();
        fight.remainingRolls -= 1;
        lastHeroToRoll = this;

        regularDices[] activeDice = new regularDices[maxDices];
        for(int i = 0; i < maxDices; i++) {
            rd[i].RollTheDice();
            activeDice[i] = rd[i];
        }

        rollCount++;
        if (rollCount >= maxRollCount) rollBtn.interactable = false;
        lastRoll = getMaxValue(activeDice);

        if (this.hasSpecialDie)
        {
            sd.OnMouseDown();
            if(sd.finalSide >= maxDie)
            {
                lastRoll = sd.finalSide;
                maxDie = sd.finalSide;
            }
        }

        if(fight.remainingRolls == 0) {
            fight.attackBtn.interactable = true;
        }

        fight.getHeroesScore();

        return lastRoll;
    }

    public static int getMaxValue(regularDices[] rdList) 
{
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

    private void UseHelm()
    {
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

    public void DisableFighter()
    {
        rollBtn.interactable = false;
    }


    public void AbandonFight() {
        if(fight.fighters.Count == 1) fight.EndFight();

        gameObject.SetActive(false);
        fight.RemoveFromFight(this);
    }

    private bool IsFullSetGem(List<Runestone> gms) 
    {
        bool blue = false, green = false, yellow = false;

        foreach(Runestone g in gms)
        {
            if(g.color == RunestoneColor.Blue)
            {
                blue = true;
            }
            if(g.color == RunestoneColor.Green)
            {
                green = true;
            }
            if(g.color == RunestoneColor.Yellow)
            {
                yellow = true;
            }
        }

        if(blue && yellow && green)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}