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
    public static Fighter lastHeroToRoll;
    public Button rollBtn, abandonBtn;
    public MultiplayerFightPlayer fight;
    public int rollCount = 0;
    public int maxRollCount = 1;
    public int maxDices;
    public bool isDead;
    
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
        if(hero.HasBow()) {
            maxRollCount = maxDices;
            maxDices = 1;
        } else {
            maxRollCount = 1;
        }

        if(hero.HasSpecialDice()) {
            this.hasSpecialDie = true;
            this.sd.gameObject.SetActive(true);
            this.sd.ResetTheDie();
            maxDices -= 1;
        } else {
            this.sd.gameObject.SetActive(false);
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

    public void NewRound() {
        UnlockRollBtns();
        hero.timeline.Update(Action.Fight.GetCost());
        InitDices();
        lastRoll = -1;
        rollCount = 0;
        hero.IsFighting = true;
    }

    public virtual void Init(Hero hero) {
        this.hero = hero;
        strength.text = hero.Strength.ToString();
        wp.text = hero.Willpower.ToString();
        isDead = false;
        
        transform.Find("Image").gameObject.SetActive(true);
        transform.Find("RIP").gameObject.SetActive(false);

        InitDices();

        foreach (DictionaryEntry entry in hero.heroInventory.smallTokens) {
            Token token = (Token)entry.Value;
            if (token is Potion) this.nb_potion++;
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
        if(h == this.hero && !isDead) {
            strength.text = h.Strength.ToString();
            wp.text = h.Willpower.ToString();
        }   
    }

    public void EndofRound() {
        hero.IsFighting = false;
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
            sd.RollTheDice();
            if(sd.finalSide >= lastRoll)
            {
                lastRoll = sd.finalSide;
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
        abandonBtn.interactable = false;
    }

      public void KillFighter()
    {
        rollBtn.interactable = false;
        abandonBtn.interactable = false;

        transform.Find("Image").gameObject.SetActive(false);
        transform.Find("RIP").gameObject.SetActive(true);
    }


    public void AbandonFight() {
        if(fight.fighters.Count == 1) fight.EndFight();

        gameObject.SetActive(false);
        fight.RemoveFromFight(this);
    }
}