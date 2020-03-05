using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    public Text HeroName;

    //private Text _enemyName;
    public Text EnemyName;

    //private Text _playerStrength;
    public Text HeroStrength;

    //private Text _playerWP;
    public Text HeroWP;

    public Text EnemyStrength;

    public Text EnemyWP;

    // Start is called before the first frame update
    public void Start()
    {
        //HeroName = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/Player Name").GetComponent<Text>();
        //HeroStrength = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/PS Value").GetComponent<Text>();
        //HeroWP = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/PWP Value").GetComponent<Text>();

        //EnemyName = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/Monster Name").GetComponent<Text>();
        //EnemyStrength = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/MS Value").GetComponent<Text>();
        //EnemyWP = GameObject.Find("AndorBoard/Canvas/Action Options/Fight/MWP Value").GetComponent<Text>();

        SetNames();
        SetStrength();
        SetWP();
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

        HeroStrength.text = GameManager.instance.CurrentPlayer.State.getStrength().ToString();
        EnemyStrength.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength.ToString();
    }

    private void SetWP()
    {
        HeroWP.text = GameManager.instance.CurrentPlayer.State.getWP().ToString();
        EnemyWP.text = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will.ToString();
    }

    public void OnClickAttack()
    {
        // ADD DICE ROLL
        int hero_strength = GameManager.instance.CurrentPlayer.State.getStrength();
        int hero_wp = GameManager.instance.CurrentPlayer.State.getWP();
        int monster_strength = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Strength;
        int monster_wp = GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will;

        if (hero_strength >= monster_strength)
        {
            monster_wp -= (hero_strength);
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will = monster_wp;
        }
        else
        {
            hero_wp -= (monster_strength);
            GameManager.instance.CurrentPlayer.State.setWP(hero_wp);
        }

        if (GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Will == 0)
        {
            GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].gameObject.SetActive(false);
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
