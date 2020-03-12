
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FightPanel : MonoBehaviour
{
    public PhotonView pv;
    private GameObject monster_object;

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


    //[PunRPC]
    //public void showPanelRPC()
    //{
    //    this.gameObject.SetActive(!this.gameObject.activeSelf);
    //    if (!PhotonNetwork.LocalPlayer.Equals(GameManager.instance.playerTurn.Peek()))
    //    {
    //        AttackButton.gameObject.SetActive(false);
    //        RollButton.gameObject.SetActive(false);
    //        AbandonButton.gameObject.SetActive(false);
    //    }
    //}

    // Start is called before the first frame update
    public void Start()
    {
        SetNames();
        SetStrength();
        SetWP();
        nb_rd = GameManager.instance.CurrentPlayer.Dices[GameManager.instance.CurrentPlayer.State.Willpower];
        monster_object = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].gameObject;
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

    private void SetNames()
    {
        HeroName.text = GameManager.instance.CurrentPlayer.HeroName;
        EnemyName.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].TokenName;
    }

    private void SetStrength()
    {
        og_SMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        hero_strength = GameManager.instance.CurrentPlayer.State.Strength;
        monster_strength = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        HeroStrength.text = hero_strength.ToString();
        EnemyStrength.text = monster_strength.ToString();
    }

    private void SetWP()
    {
        og_WPMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;
        HeroWP.text = GameManager.instance.CurrentPlayer.State.Willpower.ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        rollMessage.text = "*PLEASE ROLL THE DICE*";
    }

    public void Attack(int attack_str)
    {
        rollMessage.text = "";
        int hero_wp = GameManager.instance.CurrentPlayer.State.Willpower;
        int monster_wp = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;

        int total_strength_hero = attack_str + hero_strength;
        int monster_die1 = Random.Range(0, 5) + 1;
        int monster_die2 = Random.Range(0, 5) + 1;
        int total_strength_monster;
        if (monster_die1 == monster_die2)
        {
            total_strength_monster = monster_die1 + monster_die2;
        } else if (monster_die1 > monster_die2)
        {
            total_strength_monster = monster_die1;
        } else
        {
            total_strength_monster = monster_die2;
        }
        total_strength_monster += monster_strength;
        EnemyStrength.text = ""+ total_strength_monster;

        total_strength_monster = 1;// TO REMOVE ABSOLUTELY 

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
            hero_wp -= (monster_strength - total_strength_hero);
            GameManager.instance.CurrentPlayer.State.Willpower = hero_wp;
            HeroWP.text = hero_wp.ToString();
        }

        if (GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will <= 0)
        {
            //GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].gameObject.SetActive(false);
            if (!PhotonNetwork.OfflineMode)
            {
                pv.RPC("killMonsterRPC", RpcTarget.AllViaServer);
            }
            else
            {
                killMonsterRPC();
            }

            this.gameObject.SetActive(!this.gameObject.activeSelf);
            // gameobject shareblabla set active
            // set remainingGold to wtv the reward is!
        }

        if (GameManager.instance.CurrentPlayer.State.Willpower <= 0) 
        {
            GameManager.instance.CurrentPlayer.State.Willpower = 3;
            if (GameManager.instance.CurrentPlayer.State.Strength > 1) GameManager.instance.CurrentPlayer.State.Strength -= 1;
            else GameManager.instance.CurrentPlayer.State.Strength = 1;
            this.gameObject.SetActive(!this.gameObject.activeSelf);
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will = og_WPMonster;
        }
        SetWP();
        monster_strength = og_SMonster;
        EnemyStrength.text = "" + monster_strength;
    }
    [PunRPC]
    void killMonsterRPC()
    {
        monster_object.SetActive(!monster_object.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.CurrentPlayer.State.Strength = GameManager.instance.CurrentPlayer.State.Strength - 1;
    }
}
