using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System;

public class ArcherFighter : Fighter {
    
    public override void Init(Hero hero) {
        maxRollCount = 4;
        base.Init(hero);
    }

    public override int RollDice() {
        Debug.Log("Archer roll");
        return RollDiceWithBow();
    }
}