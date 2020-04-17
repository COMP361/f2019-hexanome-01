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
    // Heroes 
    private HeroFighter mage;
    public Button mage_button;
    private HeroFighter archer;
    public Button archer_button;
    private int archer_roll = 0;
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
    public regularDices[] GST_dice = new regularDices[2]; // GorsSkralTroll_dice
    public specialDices[] wardrack_dice = new specialDices[2];

    // Mage's superpower
    private bool hasflippedDie = false;
    private HeroFighter lastHeroToRoll;
    public Text flipMessage;

    // Thorald
    private bool Thorald = false;
    public GameObject ThoraldSprite;


    // Methods

    public void MageSuperpower()
    {
        if (!fighters.Contains(mage))
        {
            flipMessage.text = "No wizard, Thus no flip!";
            return;
        }

        if (hasflippedDie)
        {
            flipMessage.text = "Already used the superpower this round.";
            return;
        }

        if (lastHeroToRoll == null || !lastHeroToRoll.hasRolled)
        {
            flipMessage.text = "Nobody rolled their dice yet!";
        }

        int smallestdie = 6;
        regularDices dieToFlip = lastHeroToRoll.rd[0];
        foreach (regularDices rd in lastHeroToRoll.rd)
        {
            if ((rd.finalSide <= smallestdie) && rd.gameObject.activeSelf)
            {
                dieToFlip = rd;
            }
        }
        dieToFlip.OnflipDie();

        if (archer != null && lastHeroToRoll.name.text.Equals(archer.name.text))
        {
            lastHeroToRoll.hasRolled = true;
            lastHeroToRoll.lastRoll = dieToFlip.finalSide;
            return;
        }

        regularDices[] activeDice = new regularDices[lastHeroToRoll.hero.Dices[lastHeroToRoll.hero.Willpower]];
        int maxDie;
        int i = 0;

        foreach (regularDices rd in lastHeroToRoll.rd)
        {
            if (rd.gameObject.activeSelf)
            {
                activeDice[i] = rd;
                i++;
            }
        }
        maxDie = getMaxValue(activeDice);

        lastHeroToRoll.hasRolled = true;
        lastHeroToRoll.lastRoll = maxDie;
    }

    public void ActivateMage()
    {
        if (mage == null)
        {
            mage = new HeroFighter("Mage");
        }

        mage.isPresent = !mage.isPresent;

        Image img = mage_button.GetComponent<Image>();
        if (mage.isPresent)
        {
            img.color = new Color(121f / 255f, 7f / 255f, 242f / 255f, 1);
        }
        else
        {
            DisableFighter(mage);
            mage = null;
            img.color = Color.white;
        }
    }

    public void ActivateArcher()
    {
        if (archer == null)
        {
            archer = new HeroFighter("Archer");
        }
        archer.isPresent = !archer.isPresent;

        Image img = archer_button.GetComponent<Image>();
        if (archer.isPresent)
        {
            img.color = Color.green;
        }
        else
        {
            DisableFighter(archer);
            archer = null;
            img.color = Color.white;
        }
    }

    public void ActivateWarrior()
    {
        if (warrior == null)
        {
            warrior = new HeroFighter("Warrior");
        }
        warrior.isPresent = !warrior.isPresent;

        Image img = warrior_button.GetComponent<Image>();
        if (warrior.isPresent)
        {
            img.color = Color.blue;
        }
        else
        {
            DisableFighter(warrior);
            warrior = null;
            img.color = Color.white;
        }
    }

    public void ActivateDwarf()
    {
        if (dwarf == null)
        {
            dwarf = new HeroFighter("Dwarf");
        }
        dwarf.isPresent = !dwarf.isPresent;

        Image img = dwarf_button.GetComponent<Image>();
        if (dwarf.isPresent)
        {
            img.color = Color.yellow;
        }
        else
        {
            DisableFighter(dwarf);
            dwarf = null;
            img.color = Color.white;
        }
    }

    private void ReinitializeButtons()
    {
        Image img = dwarf_button.GetComponent<Image>();
        DisableFighter(dwarf);
        img.color = Color.white;
        if (dwarf != null)
        {
            dwarf.isPresent = false;
        }

        img = archer_button.GetComponent<Image>();
        DisableFighter(archer);
        img.color = Color.white;
        if (archer != null)
        {
            archer.isPresent = false;
        }

        img = warrior_button.GetComponent<Image>();
        DisableFighter(warrior);
        img.color = Color.white;
        if (warrior != null)
        {
            warrior.isPresent = false;
        }

        img = mage_button.GetComponent<Image>();
        DisableFighter(mage);
        img.color = Color.white;
        if (mage != null)
        {
            mage.isPresent = false;
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

    private void ActivateHero(String hero)
    {
        if (hero.Equals("Mage"))
        {
            if (mage == null)
            {
                mage = new HeroFighter("Mage");
            }
            mage.isPresent = false;
            ActivateMage();
        }
        else if (hero.Equals("Warrior"))
        {
            if (warrior == null)
            {
                warrior = new HeroFighter("Warrior");
            }
            warrior.isPresent = false;
            ActivateWarrior();
        }
        else if (hero.Equals("Dwarf"))
        {
            if (dwarf == null)
            {
                dwarf = new HeroFighter("Dwarf");
            }
            dwarf.isPresent = false;
            ActivateDwarf();
        }
        else if (hero.Equals("Archer"))
        {
            if (archer == null)
            {
                archer = new HeroFighter("Archer");
            }
            archer.isPresent = false;
            ActivateArcher();
        }
    }

    public void InitializeHeroes()
    {
        fighters = new List<HeroFighter>();
        List<HeroFighter> notFighters = new List<HeroFighter> { warrior, archer, mage, dwarf };

        foreach (HeroFighter h in notFighters)
        {
            if (h != null && h.isPresent)
            {
                fighters.Add(h);
            }
        }

        foreach (HeroFighter h in fighters)
        {
            h.name.text = h.hero.name;
            h.strength.text = h.hero.Strength.ToString();
            h.strengthText.text = "Strength : ";
            h.wp.text = h.hero.Willpower.ToString();
            h.wpText.text = "Will Power : ";
            for (int i = 0; i < h.hero.Dices[h.hero.Willpower]; i++)
            {
                h.rd[i].gameObject.SetActive(true);
            }
        }

        IsThoraldPresent();
    }

    public void InitializeMonster()
    {
        monster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0];
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

        MonsterSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/" + MonsterName.text.ToLower());
    }

    private int RollDice(HeroFighter hero)
    {
        if (hero.hasRolled)
        {
            return hero.lastRoll;
        }

        lastHeroToRoll = hero;

        regularDices[] activeDice = new regularDices[hero.hero.Dices[hero.hero.Willpower]];
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

    private int ArcherRollDice()
    {
        if (archer_roll < 4)
        {
            foreach (regularDices rd in archer.rd)
            {
                rd.gameObject.SetActive(false);
            }
            archer.rd[archer_roll].gameObject.SetActive(true);
            archer.rd[archer_roll].OnMouseDown();
            archer.lastRoll = archer.rd[archer_roll].getFinalSide();
            archer.hasRolled = true;
            archer_roll++;
            lastHeroToRoll = archer;
        }
        return archer.lastRoll;
    }

    public void RollMage()
    {
        RollDice(mage);
    }

    public void RollArcher()
    {
        ArcherRollDice();
    }

    public void RollWarrior()
    {
        RollDice(warrior);
    }

    public void RollDwarf()
    {
        RollDice(dwarf);
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
        foreach (HeroFighter hf in fighters)
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

        foreach (HeroFighter h in fighters)
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
            HeroesTotalStrength.text += " + 5";
            total_hero_strength += 5;
        }

        //HeroesTotalStrength.text = 13.ToString(); FOR TESTING PURPOSES
        //total_hero_strength = 13;

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
            foreach (HeroFighter h in fighters)
            {
                h.hero.Willpower -= difference;
                h.wp.text = h.hero.Willpower.ToString();
                foreach (regularDices rd in h.rd)
                {
                    rd.gameObject.SetActive(false);
                }
                if (h.hero.Willpower <= 0) // Remove the hero from the fight
                {
                    DisableFighter(h);
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
        archer_roll = 0;

        if (fighters.Count == 0)
        {
            RestoreMonster();
            panel.SetActive(false);
        }

        foreach (HeroFighter hf in fighters)
        {
            hf.hasRolled = false;
            hf.lastRoll = -1;
        }

        if (int.Parse(MonsterWP.text) <= 0)
        {
            killMonster();
            DisableFighter(archer);
            DisableFighter(mage);
            DisableFighter(warrior);
            DisableFighter(dwarf);
            DisableMonsterUI();
            ReinitializeButtons();
            //disablePanel();
            panel.SetActive(false);
        }

        //restore the flipped thingy
        flipMessage.text = "";
        lastHeroToRoll = null;
        hasflippedDie = false;
    }

    private IEnumerator disablePanel()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
    }

    public void AbandonFightMage()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("mage"));
        if (fighters.Count > 1)
        {
            DisableFighter(hf);
        }
        else
        {
            DisableFighter(hf);
            RestoreMonster();
            panel.gameObject.SetActive(false);
        }
    }

    public void AbandonFightWarrior()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("warrior"));
        if (fighters.Count > 1)
        {
            DisableFighter(hf);
        }
        else
        {
            DisableFighter(hf);
            RestoreMonster();
            panel.gameObject.SetActive(false);
        }
    }

    public void AbandonFightDwarf()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("dwarf"));
        if (fighters.Count > 1)
        {
            DisableFighter(hf);
        }
        else
        {
            DisableFighter(hf);
            RestoreMonster();
            panel.gameObject.SetActive(false);
        }
    }

    public void AbandonFightArcher()
    {
        HeroFighter hf = fighters.Find(x => x.name.text.ToLower().Equals("archer"));
        if (fighters.Count > 1)
        {
            DisableFighter(hf);
        }
        else
        {
            DisableFighter(hf);
            RestoreMonster();
            panel.gameObject.SetActive(false);
        }
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
        GameManager.instance.RemoveTokenCell(monster, monster.Cell.Inventory);
        GameManager.instance.narrator.MoveNarrator();
        GameManager.instance.narrator.CheckLegendCards();
        // Check if game is won on tower skral defeated
        if (monster.GetType().ToString() == "TowerSkral")
        {
            GameManager.instance.narrator.MoveNarratorToIndex(13);
            GameManager.instance.castle.CheckWin();
        }
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

    private void DisableFighter(HeroFighter h)
    {
        if (h == null)
        {
            return;
        }
        foreach (regularDices rd in h.rd)
        {
            rd.gameObject.SetActive(false);
        }

        h.name.text = "Empty";
        h.spriteRenderer.sprite = null;
        h.strength.text = "";
        h.wp.text = "";
        if (fighters != null && fighters.Contains(h))
        {
            fighters.Remove(h);
        }

    }

    private void DisableMonsterUI()
    {
        MonsterName.text = "No Monsters";
        MonsterSprite.GetComponent<SpriteRenderer>().sprite = null;
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

    public void ActivateMultiplayerFightPanel()
    {
        List<Button> buttons = new List<Button> { archer_button, warrior_button, dwarf_button, mage_button };
        bool isAllWhite = true;
        foreach (Button b in buttons)
        {
            Image img = b.GetComponent<Image>();
            if (img.color != Color.white)
            {
                isAllWhite = false;
            }
        }

        if (isAllWhite)
        {
            EventManager.TriggerActionUpdate(Action.None.Value);
            return;
        }
        else
        {
            panel.gameObject.SetActive(true);
        }
    }

    private void IsThoraldPresent()
    {
        if (GameManager.instance.thorald != null && GameManager.instance.thorald.Cell.Equals(GameManager.instance.CurrentPlayer.Cell))
        {
            Thorald = true;
            ThoraldSprite.gameObject.SetActive(true);
        }
        else
        {
            Thorald = false;
            ThoraldSprite.gameObject.SetActive(false);
        }
    }

    private void HasPotion(HeroFighter h)
    {
        if (h.nb_potion > 0)
        {
            h.potion.GetComponent<Button>().enabled = true; 
            //h.potion.GetComponent<SpriteRenderer>().GetComponent<Sprite>().
        }
        else
        {
            //h.potion.get
        }
    }

    private void UsePotion(HeroFighter h)
    {
        if (h.nb_potion > 0 && h.lastRoll != null)
        {
            h.potion.GetComponent<Image>().color = h.potion.GetComponent<Button>().colors.pressedColor;
            h.lastRoll = mage.lastRoll * 2;
            h.nb_potion -= 0.5;
            h.potionText.text = h.nb_potion.ToString();
            AttackMessage.text = "";
        }
        else if (h.nb_potion > 0 && h.lastRoll == -1)
        {
            AttackMessage.text = "Roll before using the potion!";
        }
        else
        {
            h.potion.GetComponent<Image>().color = h.potion.GetComponent<Button>().colors.normalColor;
            h.potion.enabled = false;
            AttackMessage.text = "";
        }
    }

    public void OnClickPotionMage()
    {
        UsePotion(mage);
    }

    public void OnClickPotionArcher()
    {
        UsePotion(archer);
    }

    public void OnClickPotionDwarf()
    {
        UsePotion(dwarf);
    }

    public void OnClickPotionWarrior()
    {
        UsePotion(warrior);
    }

    private void HasHelm(HeroFighter h)
    {
        if (h.nb_helm > 0 && h.lastRoll != null)
        {
            h.potion.GetComponent<Image>().color = h.potion.GetComponent<Button>().colors.pressedColor;
            h.lastRoll = mage.lastRoll * 2;
            h.nb_potion -= 1;
            h.potionText.text = h.helm.ToString();
            AttackMessage.text = "";
        }
        else if (h.nb_helm > 0 && h.lastRoll == -1)
        {
            AttackMessage.text = "Roll before using the helm!";
        }
        else
        {
            h.helm.GetComponent<Image>().color = h.helm.GetComponent<Button>().colors.normalColor;
            h.helm.enabled = false;
            AttackMessage.text = "";
        }
    }

    public void OnClickHelmMage() { }
    public void OnClickHelmArcher() { }
    public void OnClickHelmWarrior() { }
    public void OnClickHelmDwarf() { }
}

public class HeroFighter
{
    public Hero hero;
    public Text name;
    public Text strength;
    public Text strengthText;
    public Text wp;
    public Text wpText;

    public SpriteRenderer spriteRenderer;

    public regularDices[] rd; //= new regularDices[4];
    public specialDices sd;
    public bool isPresent = false;
    public bool hasRolled = false;
    public int lastRoll = -1;

    public Button potion;
    public double nb_potion = 1; // SET TO ZERO IF NOT TESTING
    public Text potionText;

    public Button helm;
    public int nb_helm = 1; // SET TO ZERO IF NOT TESTING
    public Text helmText;

    public int gems = 0;

    public HeroFighter(string type)
    {
        this.name = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/Name").GetComponent<Text>();
        this.strength = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/Strength").GetComponent<Text>();
        this.strengthText = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/Strength Text").GetComponent<Text>();
        this.wp = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/WP").GetComponent<Text>();
        this.wpText = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/Will Power Text").GetComponent<Text>();
        this.rd = new regularDices[] {
                GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/regular dice/rd1").GetComponent<regularDices>(),
                GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/regular dice/rd2").GetComponent<regularDices>(),
                GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/regular dice/rd3").GetComponent<regularDices>(),
                GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/regular dice/rd4").GetComponent<regularDices>()
            };
        this.sd = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/special dice/sd1").GetComponent<specialDices>();
        this.hero = GameManager.instance.heroes.Find(x => x.Type.Equals(type));
        this.spriteRenderer = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/Sprite").GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + hero.Sex.ToString().ToLower() + "_" + hero.Type.ToLower());

        foreach(var t in hero.heroInventory.smallTokens)
        {
            if (t is Potion)
            {
                this.nb_potion++;
            }
            if(t is Runestone)
            {
                this.gems++;
            }
        }

        if(this.gems > 2)
        {
            this.sd.gameObject.SetActive(true);
        }
       
        this.potion = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/potion").GetComponent<Button>();
        this.potionText = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/potion amount").GetComponent<Text>();

        this.helm = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/helm").GetComponent<Button>();
        this.helmText = GameObject.Find("Canvas/Fight/Multiplayer-Fight/Heroes/" + type + "/helm amount").GetComponent<Text>();
    }
}