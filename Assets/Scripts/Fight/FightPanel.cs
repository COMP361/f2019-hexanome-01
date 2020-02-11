using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    [SerializeField]
    private Text _playerName;
    private Text PlayerName { get; set; }

    [SerializeField]
    private Text _enemyName;
    private Text EnemyName { get; set; }

    [SerializeField]
    private Text _playerStrength;
    private Text PlayerStrength { get; set; }

    [SerializeField]
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
        this.PlayerName.text = gm.CurrentPlayer.HeroName;
        this.EnemyName.text = gm.CurrentPlayer.Cell.State.Enemies[0].TokenName;
    }

    private void SetPlayerStrength()
    {
        this.PlayerStrength.text = gm.CurrentPlayer.State.getStrength().ToString();
    }

    private void SetPlayerWP()
    {
        this.PlayerWP.text = gm.CurrentPlayer.State.getWP().ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
