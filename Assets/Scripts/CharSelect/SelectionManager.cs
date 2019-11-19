using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionManager : MonoBehaviour {

    public static readonly int playerCount = 4;
    public PlayerCard[] PlayerCards;

    private int currentPlayer = 0;

    void Start() {
        PlayerCards[currentPlayer].setAsCurrent();
    }

    public void currentPlayerLock() {
        
        HeroType chosenHero = PlayerCards[currentPlayer].CurrentHero; //chosenHero=index of the current player's hero selection at the given time
        PlayerCards[currentPlayer++].setAsLocked();

        if (currentPlayer < playerCount) {
            PlayerCards[currentPlayer].setAsCurrent();
            updatePlayerCards(chosenHero);
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
}
