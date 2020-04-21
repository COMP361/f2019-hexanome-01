﻿using System.Collections;
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
    public List<Fighter> fighters;
    public Text HeroesTotalStrength;

    // Monster's fields
    private Enemy monster;
    public GameObject MonsterSprite;
    public Text MonsterName;
    public Text MonsterStrength;
    private int og_MonsterStrength;
    public Text MonsterWP;
    private int og_MonsterWP;

    public Text MonsterTotalStrength;
    public regularDices[] GST_dice = new regularDices[2]; // GorsSkralTroll_dice
    public specialDices[] wardrack_dice = new specialDices[2];

    // Thorald
    private bool Thorald = false;
    public GameObject ThoraldSprite;

    
    public void InitializeHeroes(List<Hero> selectedHeroes)
    {
        fighters = new List<Fighter>();
        foreach (Hero hero in selectedHeroes) {
            Fighter h = transform.Find("Grid/" + hero.TokenName).GetComponent<Fighter>();
            h.Init(hero);
            fighters.Add(h);
        }

        IsThoraldPresent();
    }

    public void InitializeMonster()
    {
        monster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0];
        //monster = GameManager.instance.monster;
        MonsterName.text = monster.TokenName;
        MonsterStrength.text = monster.Strength.ToString();
        og_MonsterStrength = monster.Strength;
        MonsterWP.text = monster.Will.ToString();
        og_MonsterWP = monster.Will;

        if (monster.TokenName.Equals("Wardrack"))
        {
            wardrack_dice[0].gameObject.SetActive(true);
            wardrack_dice[1].gameObject.SetActive(true);
        }
        else
        {
            GST_dice[0].gameObject.SetActive(true);
            GST_dice[1].gameObject.SetActive(true);
        }

        MonsterSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/" + MonsterName.text.ToLower());
    }

    public int MonsterRoll()
    {
        int die1;
        int die2;
        if (monster.TokenName.Equals("Wardrack"))
        {
            wardrack_dice[0].OnMouseDown();
            wardrack_dice[1].OnMouseDown();
            die1 = wardrack_dice[0].finalSide;
            die2 = wardrack_dice[1].finalSide;
        }
        else
        {
            GST_dice[0].OnMouseDown();
            GST_dice[1].OnMouseDown();
            die1 = GST_dice[0].finalSide;
            die2 = GST_dice[1].finalSide;
        }


        if (die1 == die2)
        {
            return (die1 + die2);
        }
        else if (die1 > die2)
        {
            return die1;
        }
        else
        {
            return die2;
        }
    }

    private bool IsReadyToAttack()
    {
        foreach (Fighter hf in fighters)
        {
            if (hf.hasRolled)
            {
                continue;
            }
            return false;
        }
        return true;
    }

    public void Attack()
    {
        if (!IsReadyToAttack())
        {
            AttackMessage.text = "Everyone has to roll his dice!";
            return;
        }
        AttackMessage.text = "";
        int total_hero_strength = 0;
        int total_monster_strength = int.Parse(MonsterStrength.text) + MonsterRoll();

        foreach (Fighter h in fighters)
        {
            if (h.lastRoll == -1)
            {
                return;
            }
            total_hero_strength += h.lastRoll;
            total_hero_strength += h.hero.Strength;
        }

        HeroesTotalStrength.text = total_hero_strength.ToString();
        MonsterTotalStrength.text = total_monster_strength.ToString();

        if (Thorald)
        {
            HeroesTotalStrength.text += " + 4";
            total_hero_strength += 4;
        }

        // Actors fighting.
        int difference;
        if (total_hero_strength > total_monster_strength)
        {
            difference = total_hero_strength - total_monster_strength;
            MonsterWP.text = (int.Parse(MonsterWP.text) - difference).ToString();
        }
        else if (total_hero_strength < total_monster_strength)
        {
            difference = total_monster_strength - total_hero_strength;
            foreach (Fighter h in fighters)
            {
                if (h.useShield)
                {
                    h.shield.enabled = true;
                    Image img = h.shield.GetComponent<Image>();
                    img.color = Color.white;
                    h.useShield = false;

                    continue;
                }

                h.hero.Willpower -= difference;
                h.wp.text = h.hero.Willpower.ToString();
                foreach (regularDices rd in h.rd)
                {
                    rd.gameObject.SetActive(false);
                }
                if (h.hero.Willpower <= 0) // Remove the hero from the fight
                {
                    h.DisableFighter();
                    h.hero.Strength -= 1;
                    h.hero.Willpower = 3;
                    fighters.Remove(h);
                    continue;
                }
                for (int i = 0; i < h.hero.Dices[h.hero.Willpower]; i++)
                {
                    h.rd[i].gameObject.SetActive(true);
                }

            }
        }

        // Need to re-initialize the count of rolls.
        ArcherFighter.rollCount = 0;

        if (fighters.Count == 0)
        {
            RestoreMonster();
            panel.SetActive(false);
        }

        foreach (Fighter hf in fighters)
        {
            hf.hasRolled = false;
            hf.lastRoll = -1;
        }

        if (int.Parse(MonsterWP.text) <= 0)
        {
            killMonster();

            foreach (Fighter f in fighters) {
                f.DisableFighter();
            }
            
            DisableMonsterUI();
            panel.SetActive(false);
        }

        //restore the flipped thingy
        MageFighter.flipMessage.text = "";
        Fighter.lastHeroToRoll = null;
        MageFighter.hasflippedDie = false;
    }

    private IEnumerator disablePanel()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
    }

    
    private void RestoreMonster()
    {
        monster = null;
        MonsterName.text = "";
        MonsterStrength.text = "";
        MonsterWP.text = "";
        return;
        throw new NotImplementedException(); // I am just doing it via UI thus I think it's fine...
        // Maybe just do restorepanel!
    }

    [PunRPC]
    private void killMonsterRPC()
    {
        kill();
        GameManager.instance.RemoveTokenCell(monster, monster.Cell.Inventory);
    }

    private void killMonster()
    {
        pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
    }

    private void kill()
    {
        int id = GameManager.instance.CurrentPlayer.Cell.Index;
        Cell c = Cell.FromId(id);
        Token m = c.Inventory.Enemies[0];
        Destroy(m.gameObject);
    }

    private void DisableMonsterUI()
    {
        MonsterName.text = "No Monsters";
        MonsterSprite.GetComponent<Image>().sprite = null;
        MonsterStrength.text = "";
        MonsterWP.text = "";
        MonsterTotalStrength.text = "";

        if (monster.TokenName.Equals("Wardrack"))
        {
            wardrack_dice[0].gameObject.SetActive(false);
            wardrack_dice[1].gameObject.SetActive(false);
        }
        else
        {
            GST_dice[0].gameObject.SetActive(false);
            GST_dice[1].gameObject.SetActive(false);
        }
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
        
        if (fighters.Count == 0) {
            RestoreMonster();
            panel.gameObject.SetActive(false);
        }

    }
}