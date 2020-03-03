using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    //private Text _playerName;
/*
    //private Text _enemyName;
    private Text EnemyName { get; set; }

    //private Text _playerStrength;
    private Text HeroStrength { get; set; }

    //private Text _playerWP;
    private Text HeroWP { get; set; }

    private Text EnemyStrength { get; set; }

    private Text EnemyWP { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        HeroName = GameObject.Find("UI/Action Options/Fight Panel/Player Name").GetComponent<Text>();
        HeroStrength = GameObject.Find("UI/Action Options/Fight Panel/PS Value").GetComponent<Text>();
        HeroWP = GameObject.Find("UI/Action Options/Fight Panel/PWP Value").GetComponent<Text>();

        EnemyName = GameObject.Find("UI/Action Options/Fight Panel/Monster Name").GetComponent<Text>();
        EnemyStrength = GameObject.Find("UI/Action Options/Fight Panel/MS Value").GetComponent<Text>();
        EnemyWP = GameObject.Find("UI/Action Options/Fight Panel/MWP Value").GetComponent<Text>();

        SetNames();
        SetStrength();
        SetWP();
    }

    private void SetNames()
    {
        HeroName.text = GameManager.instance.CurrentPlayer.HeroName;
        EnemyName.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].TokenName;
    }

    private void SetStrength()
    {
        //TO REMOVE BC TESTING PURPOSES
        //GameManager.instance.CurrentPlayer.State.Strength(2);

        //HeroStrength.text = GameManager.instance.CurrentPlayer.State.Strength.ToString();
        EnemyStrength.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Strength.ToString();
    }

    private void SetWP()
    {
        //HeroWP.text = GameManager.instance.CurrentPlayer.State.Will.ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.State.cellInventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        // ADD DICE ROLL
        /*int hero_strength = GameManager.instance.CurrentPlayer.State.getStrength();
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
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        SetStrength();
        SetWP();
    }*/
}
