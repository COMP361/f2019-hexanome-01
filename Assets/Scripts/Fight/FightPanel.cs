using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    private Text _playerName;
    private Text PlayerName { get; set; }

    private Text _enemyName;
    private Text EnemyName { get; set; }

    private Text _playerStrength;
    private Text PlayerStrength { get; set; }

    private Text _playerWP;
    private Text PlayerWP { get; set; }

    GameManager gm = FindObjectOfType<GameManager>();

    // Start is called before the first frame update
    void Start()
    {
        SetNames();
        SetPlayerStrength();
        SetPlayerWP();
    }

    private void SetNames()
    {
        PlayerName.text = gm.CurrentPlayer.HeroName;
        EnemyName.text = gm.CurrentPlayer.Cell.State.Enemies[0].TokenName;
    }

    private void SetPlayerStrength()
    {
        PlayerStrength.text = gm.CurrentPlayer.State.getStrength().ToString();
    }

    private void SetPlayerWP()
    {
        PlayerWP.text = gm.CurrentPlayer.State.getWP().ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
