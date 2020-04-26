
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Random = UnityEngine.Random;

public class FightPanel : MonoBehaviour
{
    public PhotonView pv;
    private Token monster_object;

    public Text HeroName;
    public Text HeroStrength;
    public Text HeroWP;
    public Text EnemyName;
    public Text EnemyStrength;
    private int og_SMonster;
    public Text EnemyWP;
    private int og_WPMonster;

    public Text round_hero_strength;
    public Text round_monster_strength;

    private int hero_strength;
    private int monster_strength;

    public Button AttackButton;
    public Button RollButton;
    public Button AbandonButton;

    public Text rollMessage;
    public bool hasRolled = false;
    public int nb_rd;
    public int nb_sd;

    public regularDices rd1;
    public regularDices rd2;
    public regularDices rd3;
    public regularDices rd4;
    public specialDices sd1;
    public specialDices sd2;
    private List<regularDices> regular_dice = new List<regularDices>();
    private List<specialDices> special_dice = new List<specialDices>();

    public GameObject heroSprite;
    public GameObject MonsterSprite;

    // Start is called before the first frame update
    public void Start()
    {
        SetNames();
        SetImages();
        SetStrength();
        SetWP();
        nb_rd = GameManager.instance.CurrentPlayer.Dices[GameManager.instance.CurrentPlayer.Willpower];
        monster_object = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0];
        switch (nb_rd)
        {
            case 1:
                regular_dice.Add(rd1);
                break;
            case 2:
                regular_dice.Add(rd1);
                regular_dice.Add(rd2);
                break;
            case 3:
                regular_dice.Add(rd1);
                regular_dice.Add(rd2);
                regular_dice.Add(rd3);
                break;
            case 4:
                regular_dice.Add(rd1);
                regular_dice.Add(rd2);
                regular_dice.Add(rd3);
                regular_dice.Add(rd4);
                break;
            default:
                break;
        }
        switch (nb_sd)
        {
            case 1:
                special_dice.Add(sd1);
                break;
            case 2:
                special_dice.Add(sd1);
                special_dice.Add(sd2);
                break;
            default:
                break;
        }

        foreach (regularDices d in regular_dice)
        {
            d.gameObject.SetActive(true);
        }
        foreach (specialDices d in special_dice)
        {
            d.gameObject.SetActive(true);
        }
    }

    private void SetImages() //WILL NEED TO ADJUST FOR ARCHER
    {
        heroSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/"
            + GameManager.instance.CurrentPlayer.Sex.ToString()
            + "_"
            + GameManager.instance.CurrentPlayer.Type.ToLower());
        MonsterSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/" + EnemyName.text.ToLower());
    }

    private void SetNames()
    {
        HeroName.text = GameManager.instance.CurrentPlayer.HeroName;
        EnemyName.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].TokenName;
    }

    private void SetStrength()
    {
        og_SMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        hero_strength = GameManager.instance.CurrentPlayer.Strength;
        monster_strength = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        HeroStrength.text = hero_strength.ToString();
        EnemyStrength.text = monster_strength.ToString();
    }

    private void SetWP()
    {
        og_WPMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;
        HeroWP.text = GameManager.instance.CurrentPlayer.Willpower.ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        rollMessage.text = "*PLEASE ROLL THE DICE*";
    }

    public void Attack(int attack_str)
    {
        rollMessage.text = "";
        GameManager.instance.CurrentPlayer.Strength = hero_strength;
        int hero_wp = GameManager.instance.CurrentPlayer.Willpower;
        int monster_wp = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;

        int total_strength_hero = attack_str + hero_strength;
        int monster_die1 = Random.Range(0, 5) + 1;
        int monster_die2 = Random.Range(0, 5) + 1;
        int total_strength_monster;
        if (monster_die1 == monster_die2)
        {
            total_strength_monster = monster_die1 + monster_die2;
        }
        else if (monster_die1 > monster_die2)
        {
            total_strength_monster = monster_die1;
        }
        else
        {
            total_strength_monster = monster_die2;
        }
        total_strength_monster += monster_strength;
        EnemyStrength.text = "" + total_strength_monster;

        // total_strength_monster = 1;// TO REMOVE ABSOLUTELY 

        round_hero_strength.text = total_strength_hero.ToString();
        round_monster_strength.text = total_strength_monster.ToString();

        if (total_strength_hero > total_strength_monster)
        {
            monster_wp -= (total_strength_hero - total_strength_monster);
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will = monster_wp;
            EnemyWP.text = monster_wp.ToString();
        }
        else if (total_strength_hero < total_strength_monster)
        {
            hero_wp -= (total_strength_monster - total_strength_hero);
            GameManager.instance.CurrentPlayer.Willpower = hero_wp;
            HeroWP.text = hero_wp.ToString();
        }

        if (GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will <= 0)
        {
            //GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].gameObject.SetActive(false);
            if (!PhotonNetwork.OfflineMode)
            {
                killMonster();
                //pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
                //GameManager.instance.CurrentPlayer.State.cell.Inventory.RemoveToken(monster_object);
            }
            else
            {
                //GameManager.instance.CurrentPlayer.State.cell.Inventory.RemoveToken(monster_object);
                killMonster();
            }

            this.gameObject.SetActive(!this.gameObject.activeSelf);
            // gameobject shareblabla set active
            // set remainingGold to wtv the reward is!
        }

        if (GameManager.instance.CurrentPlayer.Willpower <= 0)
        {
            GameManager.instance.CurrentPlayer.Willpower = 3;
            if (GameManager.instance.CurrentPlayer.Strength > 1) GameManager.instance.CurrentPlayer.Strength -= 1;
            else GameManager.instance.CurrentPlayer.Strength = 1;
            this.gameObject.SetActive(!this.gameObject.activeSelf);
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will = og_WPMonster;
        }
        SetWP();
        monster_strength = og_SMonster;
        EnemyStrength.text = "" + monster_strength;

        EventManager.TriggerCurrentPlayerUpdate(GameManager.instance.CurrentPlayer);
    }
    

    void killMonster()
    {
        //pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
        //EventManager.TriggerEnemyDestroyed((Enemy)monster_object);
        //pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
        GameManager.instance.RemoveTokenCell(monster_object, monster_object.Cell.Inventory);
    }

    void kill()
    {
        int id = GameManager.instance.CurrentPlayer.Cell.Index;
        Cell c = Cell.FromId(id);
        Token m = c.Inventory.Enemies[0];
        Destroy(m.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
