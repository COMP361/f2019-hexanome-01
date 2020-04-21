using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System;

public class ArcherFighter : Fighter {
    
    public int RollDice() {
        Debug.Log("Archer roll");
        
        if (rollCount < 4) {
            foreach (regularDices rd in rd)
            {
                gameObject.SetActive(false);
            }
            rd[rollCount].gameObject.SetActive(true);
            rd[rollCount].OnMouseDown();
            lastRoll = rd[rollCount].getFinalSide();
            hasRolled = true;
            rollCount++;
            lastHeroToRoll = this;
        }
        return lastRoll;
    }
}