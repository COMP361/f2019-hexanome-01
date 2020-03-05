using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    public Text HeroName;
    public Text HeroStrength;
    public Text HeroWP;
    public Text EnemyName;
    public Text EnemyStrength;
    public Text EnemyWP;

    public Text rollMessage;

    public regularDices rd1;
    public regularDices rd2;
    public regularDices rd3;
    public regularDices rd4;
    public specialDices sd1;
    public specialDices sd2;
    private List<regularDices> regular_dice = new List<regularDices>();
    private List<specialDices> special_dice = new List<specialDices>();

    // Start is called before the first frame update
    public void Start()
    {
        // PLEASE DONT DELETE THESE COMMENTS
        //HeroName = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/Player Name").GetComponent<Text>();
        //HeroStrength = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/PS Value").GetComponent<Text>();
        //HeroWP = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/PWP Value").GetComponent<Text>();

        //EnemyName = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/Monster Name").GetComponent<Text>();
        //EnemyStrength = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/MS Value").GetComponent<Text>();
        //EnemyWP = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/MWP Value").GetComponent<Text>();

        SetNames();
        SetStrength();
        SetWP();
        int nb_rd = GameManager.instance.CurrentPlayer.Dices[GameManager.instance.CurrentPlayer.State.getWP()];
        //int nb_sd = GameManager.instance.CurrentPlayer.SpecialDice;

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
        //switch (nb_sd)
        //{
        //    case 1:
        //        special_dice.Add(sd1);
        //        break;
        //    case 2:
        //        special_dice.Add(sd1);
        //        special_dice.Add(sd2);
        //        break;
        //    default:
        //        break;
        //}

        foreach(regularDices d in regular_dice)
        {
            d.SetActive(true);
        }
        //foreach (GameObject g in special_dice)
        //{
        //    g.SetActive(true);
        //}
    }

    private void SetNames()
    {
        HeroName.text = GameManager.instance.CurrentPlayer.HeroName;
        EnemyName.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].TokenName;
    }

    private void SetStrength()
    {
        //TO REMOVE BC TESTING PURPOSES
        GameManager.instance.CurrentPlayer.State.setStrength(2);

        HeroStrength.text = GameManager.instance.CurrentPlayer.State.getStrength().ToString();
        EnemyStrength.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Strength.ToString();
    }

    private void SetWP()
    {
        HeroWP.text = GameManager.instance.CurrentPlayer.State.getWP().ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        rollMessage.text = "*PLEASE ROLL THE DICE*";
    }

    public void Attack()
    {
        foreach(GameObject g in regular_dice)
        {
            
        }


        int hero_strength = GameManager.instance.CurrentPlayer.State.getStrength();
        int hero_wp = GameManager.instance.CurrentPlayer.State.getWP();
        int monster_strength = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Strength;
        int monster_wp = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will;

        if (hero_strength >= monster_strength)
        {
            monster_wp -= (hero_strength);
            GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will = monster_wp;
        }
        else
        {
            hero_wp -= (monster_strength);
            GameManager.instance.CurrentPlayer.State.setWP(hero_wp);
        }

        if (GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will == 0)
        {
            GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].gameObject.SetActive(false);
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetStrength();
        SetWP();
    }
}
