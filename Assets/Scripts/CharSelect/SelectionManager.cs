using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionManager : MonoBehaviour {

    public static readonly int playerCount = 4;
    public PlayerCard[] PlayerCards;
    int currentPlayer;

    private void Awake() {
        currentPlayer = 0;
    }
    void Start() {
        PlayerCards[currentPlayer].setAsCurrent();
    }
    public void currentPlayerLock() {
        
        int chosenHero = PlayerCards[currentPlayer].CurrentHero;
        PlayerCards[currentPlayer++].setAsLocked();

        if (currentPlayer < playerCount) {

            PlayerCards[currentPlayer].setAsCurrent();
            for (int i = 0; i < playerCount; i++) {
                PlayerCards[i].disableHero(chosenHero);
                if (i > currentPlayer) PlayerCards[i].Status = "Waiting on P" + currentPlayer;
            }
        } 

    }
    public void startGame() {

    }
}
