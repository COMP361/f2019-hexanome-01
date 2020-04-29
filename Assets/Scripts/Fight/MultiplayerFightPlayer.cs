using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System;

public class MultiplayerFightPlayer : MonoBehaviour
{
    public GameObject panel;
    public PhotonView pv;
    public Text AttackMessage;
    public Text ResultMsg;
    public List<Fighter> fighters;
    public List<Fighter> disabledFighters;

    public Text HeroesTotalStrength;

    // Monster's fields
    private Enemy monster;
    public GameObject MonsterSprite;
    public Text MonsterName;
    public Text MonsterStrengthStr;
    private int monsterStrength;
    public Text MonsterWPStr;
    public int monsterWP;

    public Text MonsterTotalStrength;
    // GorsSkralTroll_dice
    public regularDices[] GST_dice = new regularDices[3];
    public specialDices[] wardrack_dice = new specialDices[2];

    // Thorald
    private bool Thorald;
    public GameObject ThoraldSprite;

    public Button newRoundBtn, leaveBtn, attackBtn, shareRewardBtn;
    private int numRounds;
    public int remainingRolls;
    public bool fightOver;
    public Fight fight;

    public void Awake() {
        newRoundBtn.onClick.AddListener(delegate { NewRound(); });
        leaveBtn.onClick.AddListener(delegate { EndFight(); });
        shareRewardBtn.onClick.AddListener(delegate { shareRewardActivate(); EndFight();});
    }

    public void Init(List<Hero> selectedHeroes, int cellID) {
        InitializeMonster(cellID);
        InitializeHeroes(selectedHeroes);
        ResultMsg.text = "";
        attackBtn.interactable = false;
        shareRewardBtn.interactable = false;
        newRoundBtn.interactable = true;
        leaveBtn.interactable = true;
        fightOver = false;
        numRounds = 0;
        remainingRolls = -1;
        MonsterTotalStrength.text = "" + 0;
        HeroesTotalStrength.text = "" + 0;
        
        CheckGameOver();
    }

    public void InitializeHeroes(List<Hero> selectedHeroes)
    {
        fighters = new List<Fighter>();
        disabledFighters = new List<Fighter>();

        foreach (Hero hero in selectedHeroes) {
            Fighter h = transform.Find("Panel/Grid/" + hero.TokenName).GetComponent<Fighter>();
            h.Init(hero);
            fighters.Add(h);
        }
        IsThoraldPresent();
    }

    public void InitializeMonster(int cellID)
    {
        Cell cell = Cell.FromId(cellID);
        monster = cell.Inventory.Enemies[0];
        EventManager.TriggerFightInventories(GameManager.instance.MainHero.Cell.Inventory, GameManager.instance.MainHero.Cell.Index);

        MonsterName.text = monster.TokenName;
        monsterStrength = monster.Strength;
        MonsterStrengthStr.text = monster.Strength.ToString();
        MonsterWPStr.text = monster.Will.ToString();
        monsterWP = monster.Will;

        InitMonsterDices();

        transform.Find("Panel/Grid/Monster/Image").gameObject.SetActive(true);
        transform.Find("Panel/Grid/Monster/RIP").gameObject.SetActive(false);

        MonsterSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/" + MonsterName.text.ToLower());
    }

    void InitMonsterDices() {
        int maxDices = monster.Dices[monsterWP];
        regularDices[] dices;
        if (monster.TokenName.Equals("Wardrak")) {
            dices = wardrack_dice;
            for (int i = 0; i < GST_dice.Length; i++) {
                GST_dice[i].gameObject.SetActive(false);
            }
        } else {
            dices = GST_dice;
            for (int i = 0; i < wardrack_dice.Length; i++) {
                wardrack_dice[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < dices.Length; i++) {
            if(i < maxDices) {
                dices[i].gameObject.SetActive(true);
                dices[i].ResetTheDie();
            } else {
                dices[i].gameObject.SetActive(false);
            }
        }
    }

    void NewRound() {
        foreach(Fighter f in fighters) {
            f.NewRound();
        }

        foreach(Fighter f in disabledFighters) {
            f.InitDices();
        }

        numRounds++;
        remainingRolls = fighters.Count;
        leaveBtn.interactable = false;
        newRoundBtn.interactable = false;
        MonsterTotalStrength.text = "" + 0;
        HeroesTotalStrength.text = "" + 0;

        Fighter.lastHeroToRoll = null;
        InitMonsterDices();
        getHeroesScore();
    }

    public int MonsterRoll() {
        int maxDices = monster.Dices[monsterWP];
        int[] results = new int[maxDices];

        if (monster.TokenName.Equals("Wardrak")) {
            for(int i = 0; i < maxDices; i++) {
                wardrack_dice[i].RollTheDice();
                results[i] = wardrack_dice[i].finalSide;
            }
        } else {
            for(int i = 0; i < maxDices; i++) {
                GST_dice[i].RollTheDice();
                results[i] = GST_dice[i].finalSide;
            }
        }

        if(maxDices == 2) {
            if (results[0] == results[1]) return results[0] + results[1];
            return Math.Max(results[0], results[1]);

        } else if(maxDices == 3) {
            if(results[0] == results[1] && results[0] == results[2]) return results[0] + results[1] + results[2];

            int doubleScore = 0;
            if(results[0] == results[1]) {
                doubleScore = results[0] + results[1];
            } else if(results[0] == results[2]) {
                doubleScore = results[0] + results[2];
            } else if(results[1] == results[2]) {
                doubleScore = results[1] + results[2];
            }

            int singleScore = Math.Max(results[0], Math.Max(results[1], results[2]));

            return Math.Max(singleScore, doubleScore);
        } else {
            return results[0];
        }
    }

    public int getHeroesScore() {
        int totalScore = 0;

        foreach (Fighter h in fighters) {
            if (h.lastRoll != -1) totalScore += h.lastRoll;
            totalScore += h.hero.Strength;
        }

        if (Thorald) {
            totalScore += 4;
        }

        HeroesTotalStrength.text = totalScore.ToString();

        return totalScore;
    }


    void CheckGameOver() {
        for(int i = fighters.Count-1; i >= 0; i--) {
            Fighter f = fighters[i];
            if (f.hero.Willpower <= 0) {
                f.isDead = true;
                f.KillFighter();
                f.hero.Strength = Math.Max(1, f.hero.Strength-1) ;
                f.hero.setWP(3);
                disabledFighters.Add(f);
                fighters.Remove(f);
            } else if (!f.hero.timeline.HasHoursLeft()) {
                f.DisableFighter();
                disabledFighters.Add(f);
                fighters.Remove(f);
            }
        }

        if (fighters.Count == 0) {
            fightOver = true;
            ResultMsg.text = "Heroes have lost.";
        }

        if(fightOver) newRoundBtn.interactable = false;
    }

    public void Attack()
    {
        foreach (Fighter f in fighters) {
            if (f.lastRoll == -1) return;
        }

        int total_hero_strength = getHeroesScore();
        int total_monster_strength = monsterStrength + MonsterRoll();
        MonsterTotalStrength.text = total_monster_strength.ToString();

        // Actors fighting.
        int difference;
        int roundLoss = 0;
        if (total_hero_strength > total_monster_strength) {
            difference = total_hero_strength - total_monster_strength;
            monsterWP = Math.Max(0, monsterWP - difference);
            MonsterWPStr.text = monsterWP.ToString();
        } else if (total_hero_strength < total_monster_strength) {
            difference = total_monster_strength - total_hero_strength;
            roundLoss = difference;
            for(int i = fighters.Count-1; i >= 0; i--) {
                Fighter f = fighters[i];
                int prevWill = f.hero.Willpower;

                int willpower = Math.Max(0, f.hero.Willpower - difference);
                f.hero.setWP(willpower);

                // Check Game Over

                if (willpower <= 0) {
                    f.isDead = true;
                    f.KillFighter();
                    f.hero.Strength = Math.Max(1, f.hero.Strength-1) ;
                    f.hero.setWP(3);
                    disabledFighters.Add(f);
                    f.EndofRound(prevWill);
                    fighters.Remove(f);
                } else if (!(Timeline.GetFreeHours(f.hero.timeline.Index) > 0 || Timeline.GetExtendedHours(f.hero.timeline.Index, willpower) > 0)) {
                    f.DisableFighter();
                    disabledFighters.Add(f);
                    f.EndofRound(roundLoss);
                    fighters.Remove(f);
                }
            }
        }

        foreach (Fighter f in fighters) {
            f.EndofRound(roundLoss);
        }

        leaveBtn.interactable = true;
        attackBtn.interactable = false;
        newRoundBtn.interactable = true;

        for(int i = fighters.Count-1; i >= 0; i--) {
            Fighter f = fighters[i];
            if (!f.hero.timeline.HasHoursLeft()) {
                f.DisableFighter();
                disabledFighters.Add(f);
                fighters.Remove(f);
            }
        }

        if (fighters.Count == 0) {
            fightOver = true;
            ResultMsg.text = "Heroes have lost.";
        }

        if (monsterWP <= 0) {
            fightOver = true;
            killMonster();
        }

        if(fightOver) newRoundBtn.interactable = false;
    }

    private void disablePanel()
    {
        panel.SetActive(false);
    }

    private void killMonster()
    {
        ResultMsg.text = "Monster is killed!";
        transform.Find("Panel/Grid/Monster/Image").gameObject.SetActive(false);
        transform.Find("Panel/Grid/Monster/RIP").gameObject.SetActive(true);
        leaveBtn.interactable = false;
        shareRewardBtn.interactable = true;

        pv.RPC("KillMonsterRPC", RpcTarget.AllViaServer, new object[] {monster.Cell.Index});
    }

    [PunRPC]
    void NewRoundRPC(String heroName) {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.TokenName == heroName) {
                hero.timeline.Update(Action.Fight.GetCost());
                hero.IsFighting = true;
            }
        }
    }

    [PunRPC]
    void EndofRoundRPC(String heroName) {
        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.TokenName == heroName) {
                hero.IsFighting = false;
            }
        }
    }

    [PunRPC]
    void KillMonsterRPC(int cellID) {
        Enemy enemy = Cell.FromId(cellID).Inventory.Enemies[0];
        Destroy(enemy.gameObject);
        if (enemy.GetType() == typeof(TowerSkral))
        {
            disablePanel();
            GameManager.instance.narrator.MoveNarratorToIndex(13);
            GameManager.instance.castle.CheckWin();
        }
    }

    private void shareRewardActivate(){
      List<Hero> heroes = new List<Hero>();
      foreach(Fighter f in fighters) {
          heroes.Add(f.hero);
      }

      EventManager.TriggerShareReward(monster.Reward, heroes);
    }

    private void IsThoraldPresent()
    {
        Thorald = false;
        ThoraldSprite.gameObject.SetActive(false);

        if (GameManager.instance.thorald != null && GameManager.instance.thorald.Cell.Equals(GameManager.instance.CurrentPlayer.Cell)) {
            Thorald = true;
            ThoraldSprite.gameObject.SetActive(true);
        }
    }

    public void RemoveFromFight(Fighter hf) {
        if (fighters != null && fighters.Contains(hf)) {
            fighters.Remove(hf);
        }
    }

    public void EndFight() {
        EventManager.TriggerEndFightInventories();
        panel.SetActive(false);
        if(numRounds == 0)  {
            EventManager.TriggerActionUpdate(Action.None.Value);
        } else {
            EventManager.TriggerEndTurn();
        }
    }
}
