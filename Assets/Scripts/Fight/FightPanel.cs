
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FightPanel : MonoBehaviour
{
    public PhotonView pv;

    public Text HeroName;
    public Text HeroStrength;
    public Text HeroWP;
    public Text EnemyName;
    public Text EnemyStrength;
    private int og_SMonster;
    public Text EnemyWP;
    private int og_WPMonster;

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


    [PunRPC]
    public void showPanelRPC()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (!PhotonNetwork.LocalPlayer.Equals(GameManager.instance.playerTurn.Peek()))
        {
            AttackButton.gameObject.SetActive(false);
            RollButton.gameObject.SetActive(false);
            AbandonButton.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        SetNames();
        SetStrength();
        SetWP();
        nb_rd = GameManager.instance.CurrentPlayer.Dices[GameManager.instance.CurrentPlayer.State.getWP()];

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
        //TO REMOVE BC TESTING PURPOSES
        GameManager.instance.CurrentPlayer.State.setStrength(2);
        og_SMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        HeroStrength.text = GameManager.instance.CurrentPlayer.State.getStrength().ToString();
        EnemyStrength.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength.ToString();
    }

    private void SetWP()
    {
        og_WPMonster = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;
        HeroWP.text = GameManager.instance.CurrentPlayer.State.getWP().ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        rollMessage.text = "*PLEASE ROLL THE DICE*";
    }

    public void Attack(int attack_str)
    {
        rollMessage.text = "";
        int hero_strength = GameManager.instance.CurrentPlayer.State.getStrength();
        int hero_wp = GameManager.instance.CurrentPlayer.State.getWP();
        int monster_strength = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        int monster_wp = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;

        int total_strength = attack_str + hero_strength;
        monster_strength = monster_strength + Random.Range(0, 5)+1;
        EnemyStrength.text = ""+monster_strength;

        if (total_strength > monster_strength)
        {
            monster_wp -= (hero_strength);
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will = monster_wp;
        }
        else if (total_strength > monster_strength)
        {
            hero_wp -= (monster_strength);
            GameManager.instance.CurrentPlayer.State.setWP(hero_wp);
        }

        if (GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will <= 0)
        {
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].gameObject.SetActive(false);
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

        if (GameManager.instance.CurrentPlayer.State.getWP() <= -1) // TODO: SET TO ZERO WHEN DONE
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
        SetWP();
        monster_strength = og_SMonster;
        EnemyStrength.text = "" + monster_strength;
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will = og_WPMonster;
        GameManager.instance.CurrentPlayer.State.setStrength(GameManager.instance.CurrentPlayer.State.getStrength() - 1);
        //SetStrength();
        //SetWP();
    }
}
