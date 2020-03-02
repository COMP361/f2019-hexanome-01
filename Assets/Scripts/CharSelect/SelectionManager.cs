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
    public PhotonView pv;
    public List<Player> players = PhotonNetwork.PlayerList.ToList();
    public Text player1_username;
    public Text player2_username;
    public Text player3_username;

    private int currentPlayer = 0;

    void Start() {
        PlayerCards[currentPlayer].setAsCurrent();
        player1_username.text = players[0].NickName;
        player2_username.text = players[1].NickName;
        player3_username.text = players[2].NickName;
    }

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
            PhotonNetwork.LocalPlayer.NickName = chosenHero.ToString();
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
        
    }
}
