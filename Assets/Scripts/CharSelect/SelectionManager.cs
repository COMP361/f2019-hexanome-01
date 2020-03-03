using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;

public class SelectionManager : MonoBehaviour {

    public static readonly int playerCount = 4;
    public PlayerCard[] PlayerCards;
    private Queue<Player> playerTurn;
    public PhotonView pv;
    public List<Player> players = PhotonNetwork.PlayerList.ToList();
    public Text player1_username;
    public Text player2_username;
    public Text player3_username;
    public Text player4_username;
    public Button cardSelection1;
    public GameObject cardSelection2;
    public GameObject cardSelection3;
    public GameObject cardSelection4;

    private int currentPlayer = 0;

    void Awake() {
        player1_username.text = players[0].NickName;
        player2_username.text = players[1].NickName;
        //player3_username.text = players[2].NickName;
        //player4_username.text = players[3].NickName;

        playerTurn = new Queue<Player>();

        foreach (Player p in players)
        {
            playerTurn.Enqueue(p);
        }

        PlayerCards[currentPlayer].setAsCurrent();
    }
  }

/*
    public void currentPlayerLock()
    {
        pv.RPC("receiveCurrentPlayerLock", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void receiveCurrentPlayerLock() {

        HeroType chosenHero = PlayerCards[currentPlayer].CurrentHero; //chosenHero=index of the current player's hero selection at the given time
        PlayerCards[currentPlayer++].setAsLocked();

        if (currentPlayer < playerCount) {
            PlayerCards[currentPlayer].setAsCurrent();
            updatePlayerCards(chosenHero);
            ExitGames.Client.Photon.Hashtable classTable = new ExitGames.Client.Photon.Hashtable();
            classTable.Add("Class", chosenHero.ToString());
            PhotonNetwork.LocalPlayer.SetCustomProperties(classTable);
            playerTurn.Dequeue();
        }
    }

    private void updatePlayerCards(HeroType heroLocked) {
        for (int i = 0; i < playerCount; i++) {
            PlayerCards[i].disableHero(heroLocked); //disable the ability to select the hero for all other players
            if (i > currentPlayer) PlayerCards[i].Status = "Waiting on P" + currentPlayer; //all cards of players AFTER the current is updated to "waiting on <player>"
        }
    }

    public void startGame() {

    }

    public void Update()
    {
        ShowCards();
    }

    private void ShowCards()
    {
        if (playerTurn.Peek().Equals(PhotonNetwork.LocalPlayer))
        {
            cardSelection1.gameObject.GetComponent<Collider2D>().enabled = true;
            cardSelection2.gameObject.GetComponent<Collider2D>().enabled = true;
            cardSelection3.gameObject.GetComponent<Collider2D>().enabled = true;
            cardSelection4.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            cardSelection1.gameObject.GetComponent<Collider2D>().enabled= false;
            cardSelection2.gameObject.GetComponent<Collider2D>().enabled = false;
            cardSelection3.gameObject.GetComponent<Collider2D>().enabled = false;
            cardSelection4.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
*/
