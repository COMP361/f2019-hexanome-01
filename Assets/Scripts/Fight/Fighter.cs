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
    public bool hasSpecialDie;
    public int lastRoll;
    
    public Button potion;

    private Potion potionToken;

    public bool hasHelm;
    public Button helm;

    private Shield shieldToken;
    public Button shield;

    public static Fighter lastHeroToRoll;
    public Button rollBtn, abandonBtn;
    public MultiplayerFightPlayer fight;
    public int rollCount;
    private int doubleRank;
    public int maxRollCount;
    public int maxDices;
    public bool isDead;
    private int lastLoss;
    
    void OnEnable() {
        EventManager.UpdateHeroStats += UpdatePlayerStats;
    }

    void OnDisable() {
        EventManager.UpdateHeroStats -= UpdatePlayerStats;
    }

    void Start() {
        rollBtn.onClick.AddListener(delegate { RollDice(); });
        abandonBtn.onClick.AddListener(delegate { AbandonFight(); });
        potion.onClick.AddListener(delegate { UsePotion(); });
        helm.onClick.AddListener(delegate { UseHelm(); });
        shield.onClick.AddListener(delegate { UseShield(); });
    }

    public void InitDices() {
        hasSpecialDie = false;

        maxDices = hero.Dices[hero.Willpower];
        if(hero.HasBow()) {
            maxRollCount = maxDices;
            maxDices = 1;
        } else {
            maxRollCount = 1;
        }

        if(hero.HasSpecialDice()) {
            hasSpecialDie = true;
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
        InitAccessories();
        InitDices();
        lastLoss = 0;
        fight.pv.RPC("NewRoundRPC", RpcTarget.AllViaServer, new object[] { hero.TokenName });
        UnlockRollBtns();
        lastRoll = -1;
        rollCount = 0;
    }

    public virtual void Init(Hero hero) {
        this.hero = hero;
        strength.text = hero.Strength.ToString();
        wp.text = hero.Willpower.ToString();
        isDead = false;
        
        transform.Find("Image").gameObject.SetActive(true);
        transform.Find("RIP").gameObject.SetActive(false);

        InitDices();

        InitAccessories();
        
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

    public virtual void EndofRound(int loss) {
        potion.interactable = false;
        helm.interactable = false;
        if(loss > 0 && shieldToken != null) {
            lastLoss = loss;
            shield.interactable = true;
        }
        fight.pv.RPC("EndofRoundRPC", RpcTarget.AllViaServer, new object[] { hero.TokenName });
    }

    public int RollDice() {
        foreach(Fighter f in fight.fighters) {
            if (f.rollCount > 0 && f != this) {
                f.rollBtn.interactable = false;
                f.potion.interactable = false;
                f.helm.interactable = false;
            }        
        }

        if (rollCount >= maxRollCount) return lastRoll;
        
        // First to roll, unlock flip btn
        if(lastHeroToRoll == null) MageFighter.UnlockFlipBtn();
        
        fight.remainingRolls -= 1;
        lastHeroToRoll = this;
        doubleRank = -1;

        regularDices[] activeDice = new regularDices[maxDices];
        for(int i = 0; i < maxDices; i++) {
            rd[i].RollTheDice();
            activeDice[i] = rd[i];
        }
        lastRoll = getMaxValue(activeDice);
        
        if(maxDices >= 2) {
            for(int i = 0; i < activeDice.Length; i++) {
                for(int j = i+1; j < activeDice.Length; j++) {
                    if(activeDice[i].getFinalSide() == activeDice[j].getFinalSide() && activeDice[i].getFinalSide() > doubleRank) {
                        doubleRank = activeDice[i].getFinalSide();
                    }
                }
            }
        }
        
        if (this.hasSpecialDie) {
            sd.RollTheDice();
            if(sd.finalSide >= lastRoll) {
                lastRoll = sd.finalSide;
            }
        }

        rollCount++;
        if (rollCount >= maxRollCount) rollBtn.interactable = false;

        if(fight.remainingRolls == 0) {
            fight.attackBtn.interactable = true;
        }

        fight.getHeroesScore();

        if(potionToken != null) potion.interactable = true;
        if(hasHelm && doubleRank > -1 && doubleRank*2 > lastRoll) helm.interactable = true;

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

    private void InitAccessories() {
        potionToken = null;
        potion.gameObject.SetActive(false); 
        foreach (DictionaryEntry entry in hero.heroInventory.smallTokens) {
            Token token = (Token)entry.Value;
            if (token is Potion) {
                potionToken = (Potion)token;
                potion.gameObject.SetActive(true); 
                potion.interactable = false;
                break;
            }
        }

        hasHelm = false;
        shieldToken = null;
        helm.gameObject.SetActive(false); 
        shield.gameObject.SetActive(false); 
        foreach(DictionaryEntry entry in hero.heroInventory.AllTokens) {
            Token token = (Token)entry.Value;

            if(token is Helm) {
                hasHelm = true;
                helm.gameObject.SetActive(true);
                helm.interactable = false; 
            } else if(token is Shield) {
                shieldToken = (Shield)token;
                shield.gameObject.SetActive(true); 
                shield.interactable = false;
            }
        }
    }

    private void UsePotion() {
        if (lastRoll != -1) {
            potion.interactable = false;
            helm.interactable = false;
            lastRoll = lastRoll * 2;
            fight.getHeroesScore();

            if(potionToken is HalfPotion) {
                hero.heroInventory.RemoveSmallToken(potionToken);
            } else {
                HalfPotion hp = HalfPotion.Factory();
                hero.heroInventory.ReplaceSmallToken(potionToken, hp, true);
            }
        }
    }

    private void UseShield() {
        if (lastLoss != 0) {
            shield.interactable = false;
            
            if(isDead) {
                hero.setWP(lastLoss);

                isDead = false;
                fight.fightOver = false;
                fight.ResultMsg.text = "";
                fight.fighters.Add(this);
                fight.newRoundBtn.interactable = true;

                transform.Find("Image").gameObject.SetActive(true);
                transform.Find("RIP").gameObject.SetActive(false);
            } else {
                hero.setWP(hero.Willpower + lastLoss);
            }

            if(shieldToken is HalfShield) {
                hero.heroInventory.RemoveBigToken(shieldToken);
            } else {
                HalfShield hs = HalfShield.Factory();
                hero.heroInventory.ReplaceBigToken(shieldToken, hs, true);
            }
        }
    }

    private void UseHelm() {
        if (lastRoll != -1) {
            potion.interactable = false;
            helm.interactable = false;
            lastRoll = doubleRank * 2;
            fight.getHeroesScore();
        }
    }

    public void DisableFighter()
    {
        rollBtn.interactable = false;
        abandonBtn.interactable = false;
        InitDices();
    }

    public void KillFighter() {
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