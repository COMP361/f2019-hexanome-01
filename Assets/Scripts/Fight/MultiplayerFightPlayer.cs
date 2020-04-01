using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Random = UnityEngine.Random;
using System;

public class MultiplayerFightPlayer : MonoBehaviour
{
    public GameObject panel;
    public PhotonView pv;
    public Text AttackMessage;
    // Heroes 
    private HeroFighter mage;
    public Button mage_button;
    private HeroFighter archer;
    public Button archer_button;
    private HeroFighter warrior;
    public Button warrior_button;
    private HeroFighter dwarf;
    public Button dwarf_button;

    List<HeroFighter> fighters;
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


    public void activateMage()
    {
        mage = new HeroFighter("Mage");
        mage.isPresent = !mage.isPresent;

        Image img = mage_button.GetComponent<Image>();
        if (mage.isPresent)
        {
            img.color = new Color(121f/255f, 7f / 255f, 242f / 255f, 1);
        }
        else
        {
            DisableFighter(mage);
            img.color = Color.white;
        }
    }

    public void activateArcher()
    {
        archer = new HeroFighter("Archer");
        archer.isPresent = !archer.isPresent;

        Image img = archer_button.GetComponent<Image>();
        if (archer.isPresent)
        {
            img.color = Color.green;
        }
        else
        {
            DisableFighter(archer);
            img.color = Color.white;
        }
    }

    public void activateWarrior()
    {
        warrior = new HeroFighter("Warrior");
        warrior.isPresent = !warrior.isPresent;

        Image img = warrior_button.GetComponent<Image>();
        if (warrior.isPresent)
        {
            img.color = Color.blue;
        }
        else
        {
            DisableFighter(warrior);
            img.color = Color.white;
        }
    }

    public void activateDwarf()
    {
        dwarf = new HeroFighter("Dwarf");
        dwarf.isPresent = !dwarf.isPresent;

        Image img = dwarf_button.GetComponent<Image>();
        if (dwarf.isPresent)
        {
            img.color = Color.yellow;
        }
        else
        {
            DisableFighter(dwarf);
            img.color = Color.white;
        }
    }

    public void InTheCellHeroes()
    {
        Cell currentCell = GameManager.instance.CurrentPlayer.Cell;
        List<string> hero_types = new List<string>();
        List<Button> buttons = new List<Button> { warrior_button, mage_button, archer_button, dwarf_button };

        foreach (Hero h in currentCell.Inventory.Heroes)
        {
            hero_types.Add(h.Type.ToLower());
        }

        if (hero_types.Contains("archer"))
        {
            archer_button.enabled = true;
        }
        else
        {
            archer_button.enabled = false;
        }
        if (hero_types.Contains("warrior"))
        {
            warrior_button.enabled = true;
        }
        else
        {
            warrior_button.enabled = false;
        }
        if (hero_types.Contains("mage"))
        {
            mage_button.enabled = true;
        }
        else
        {
            mage_button.enabled = false;
        }
        if (hero_types.Contains("dwarf"))
        {
            dwarf_button.enabled = true;
        }
        else
        {
            dwarf_button.enabled = false;
        }
    }

    public void ClickedColor(string hero)
    {

    }

    public void InitializeHeroes()
    {
        List<Hero> heroes = GameManager.instance.heroes;
        fighters = new List<HeroFighter>();
        List<HeroFighter> notFighters = new List<HeroFighter> { warrior, archer, mage, dwarf };

        foreach(HeroFighter h in notFighters)
        {
            if (h != null && h.isPresent)
            {
                fighters.Add(h);
            }
        }

        foreach(HeroFighter h in fighters)
        {  
            h.name.text = h.hero.name;
            h.strength.text = h.hero.State.Strength.ToString();
            h.wp.text = h.hero.State.Willpower.ToString();
            for(int i=0; i<h.hero.Dices[h.hero.State.Willpower]; i++)
            {
                h.rd[i].gameObject.SetActive(true);
            }
        }
    }

    public void InitializeMonster()
    {
        monster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0];
        MonsterName.text = monster.TokenName;     
        MonsterStrength.text = monster.Strength.ToString();
        og_MonsterStrength = monster.Strength;
        MonsterWP.text = monster.Will.ToString();
        og_MonsterWP = monster.Will;

        MonsterSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Enemies/" + MonsterName.text.ToLower());
    }

    private int rollDice(HeroFighter hero)
    {
        if (hero.hasRolled)
        {
            return hero.lastRoll;
        }

        regularDices[] activeDice = new regularDices[hero.hero.Dices[hero.hero.State.Willpower]];
        int maxDie;
        int i = 0;
        foreach (regularDices rd in hero.rd)
        {
            if (rd.gameObject.activeSelf)
            {
                rd.OnMouseDown();
                activeDice[i] = rd;
                i++;
            }
        }
        maxDie = getMaxValue(activeDice);

        hero.hasRolled = true;
        hero.lastRoll = maxDie;

        return maxDie;
    }
    public void rollMage()
    {
        rollDice(mage);
    }
    public void rollArcher()
    {
        rollDice(archer);
    }
    public void rollWarrior()
    {
        rollDice(warrior);
    }
    public void rollDwarf()
    {
        rollDice(dwarf);
    }

    public int monsterRoll()
    {
        int die1 = Random.Range(0, 5) + 1;
        int die2 = Random.Range(0, 5) + 1;
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

    private bool isReadyToAttack()
    {
        foreach(HeroFighter hf in fighters)
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
        if (!isReadyToAttack())
        {
            AttackMessage.text = "Everyone has to roll his dice!";
            return;
        }
        AttackMessage.text = "";
        int total_hero_strength = 0;
        int total_monster_strength = int.Parse(MonsterStrength.text) + monsterRoll();

        foreach(HeroFighter h in fighters)
        {
            if(h.lastRoll == -1)
            {
                return;
            }
            total_hero_strength += h.lastRoll;
            total_hero_strength += h.hero.State.Strength;
        }

        HeroesTotalStrength.text = total_hero_strength.ToString();
        MonsterTotalStrength.text = total_monster_strength.ToString();

        HeroesTotalStrength.text = 13.ToString();
        total_hero_strength = 13;

        // Actors fighting.
        int difference;
        if(total_hero_strength > total_monster_strength)
        {
            difference = total_hero_strength - total_monster_strength;
            MonsterWP.text = (int.Parse(MonsterWP.text) - difference).ToString();
        }
        else if(total_hero_strength < total_monster_strength)
        {
            difference = total_monster_strength - total_hero_strength;
            foreach(HeroFighter h in fighters)
            {
                h.hero.State.Willpower -= difference;
                h.wp.text = h.hero.State.Willpower.ToString();
                foreach(regularDices rd in h.rd)
                {
                    rd.gameObject.SetActive(false);
                }
                if (h.hero.State.Willpower <= 0) // Remove the hero from the fight
                {
                    DisableFighter(h);
                    h.hero.State.Strength -= 1;
                    h.hero.State.Willpower = 3;
                    fighters.Remove(h);
                    continue;
                }
                for (int i = 0; i < h.hero.Dices[h.hero.State.Willpower]; i++)
                {
                    h.rd[i].gameObject.SetActive(true);
                }

            }
        }

        if(fighters.Count == 0)
        {
            RestoreMonster();
            panel.SetActive(false);
        }

        foreach(HeroFighter hf in fighters)
        {
            hf.hasRolled = false;
            hf.lastRoll = -1;
        }

        if (int.Parse(MonsterWP.text) <= 0)
        {
            killMonster();
            foreach(HeroFighter h in fighters)
            {
                DisableFighter(h);
            }
            DisableMonsterUI();
            panel.SetActive(false);
        }
    }

    public void AbandonFightMage()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("mage"));
        if (fighters.Count > 1)
        {
            fighters.Remove(hf);
        }
        else
        {
            RestoreMonster();
        }
    }
    public void AbandonFightWarrior()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("warrior"));
        if (fighters.Count > 1)
        {
            fighters.Remove(hf);
        }
        else
        {
            RestoreMonster();
        }
    }
    public void AbandonFightDwarf()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("dwarf"));
        if (fighters.Count > 1)
        {
            fighters.Remove(hf);
        }
        else
        {
            RestoreMonster();
        }
    }
    public void AbandonFightArcher()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("archer"));
        if (fighters.Count > 1)
        {
            fighters.Remove(hf);
        }
        else
        {
            RestoreMonster();
        }
    }

    private void RestoreMonster()
    {
        throw new NotImplementedException(); // I am just doing it via UI thus I think it's fine...
        // Maybe just do restorepanel!
    }

    private int getMaxValue(regularDices[] rdList)
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

    [PunRPC]
    private void killMonsterRPC()
    {
        kill();     
    }

    private void killMonster()
    {
        pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
        GameManager.instance.RemoveTokenCell(monster, monster.Cell.Inventory);
    }

    private void kill()
    {
        int id = GameManager.instance.CurrentPlayer.Cell.Index;
        Cell c = Cell.FromId(id);
        Token m = c.Inventory.Enemies[0];
        Destroy(m.gameObject);
    }

    private void DisableFighter(HeroFighter h)
    {
        foreach (regularDices rd in h.rd)
        {
            rd.gameObject.SetActive(false);
        }

        h.name.text = "Empty";
        h.spriteRenderer.sprite = null;
        h.strength.text = "0";
        h.wp.text = "0";
        fighters.Remove(h);
    }

    private void DisableMonsterUI()
    {
        MonsterName.text = "No Monsters";
        MonsterSprite.GetComponent<SpriteRenderer>().sprite = null;
        MonsterStrength.text = "";
        MonsterWP.text = "";
        MonsterTotalStrength.text = "";
    }
}

public class HeroFighter 
{
    public Hero hero;
    public Text name;
    public Text strength;
    public Text wp;
    public SpriteRenderer spriteRenderer;
    public regularDices[] rd; //= new regularDices[4];
    public bool isPresent = false;
    public bool hasRolled = false;
    public int lastRoll = -1;

    public HeroFighter(string type)
    {
        this.name = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/Name").GetComponent<Text>();
        this.strength = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/Strength").GetComponent<Text>();
        this.wp = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/WP").GetComponent<Text>();
        this.rd = new regularDices[] {
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd1").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd2").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd3").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd4").GetComponent<regularDices>()
            };
        this.hero = GameManager.instance.heroes.Find(x => x.Type.Equals(type));
        Debug.Log("Sprites/heroes/" + hero.getSex().ToLower() + "_" + hero.Type.ToLower());
        this.spriteRenderer = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/Sprite").GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/heroes/" + hero.getSex().ToLower() + "_" + hero.Type.ToLower());
    }
}