﻿using UnityEngine;
using System;
using Random = System.Random;

public class Narrator {
    public int index; // 0 -> A
    public int runestoneIndex;
    public char runestoneLetter;
    public bool witchFound;
    public bool medicineDelivered;
    public bool towerSkralDefeated;
    public LegendCardDeck legendCardDeck;

    GameObject narratorToken;
    GameObject runestoneToken;

    public Narrator()
    {
        index = 0;
        setRunestonePosition();
        witchFound = false;
        legendCardDeck = new LegendCardDeck(false);

        narratorToken = new GameObject("Narrator");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Narrator");
        SpriteRenderer sr = narratorToken.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 3;
        narratorToken.transform.localScale = new Vector3(15, 15, 15);
        narratorToken.transform.parent = GameObject.Find("Tokens").transform;
        narratorToken.transform.position = GameObject.Find("Narrator/A/Narrator").transform.position;

        runestoneToken = new GameObject("NarratorRunestone");
        Sprite runestoneSprite = Resources.Load<Sprite>("Sprites/Tokens/Stone/Stone-Blue");
        SpriteRenderer sr_runestone = runestoneToken.AddComponent<SpriteRenderer>();
        sr_runestone.sprite = runestoneSprite;
        sr_runestone.sortingOrder = 2;
        runestoneToken.transform.localScale = new Vector3(15, 15, 15);
        runestoneToken.transform.parent = GameObject.Find("Tokens").transform;
        runestoneToken.transform.position = GameObject.Find("Narrator/" + runestoneLetter + "/Runestone").transform.position;
    }

    public void MoveNarrator()
    {
        if (index < 13)
        {
            index++;
        }
        char letter = (char)(index + 65);
        narratorToken.transform.position = GameObject.Find("Narrator/" + letter + "/Narrator").transform.position;
        CheckLegendCards();
    }

    public void MoveNarratorToIndex(int indexToMove)
    {
        if (indexToMove > 0 && indexToMove <= 13)
        {
            index = indexToMove;
            char letter = (char)(index + 65);
            narratorToken.transform.position = GameObject.Find("Narrator/" + letter + "/Narrator").transform.position;
            CheckLegendCards();
        }
    }

    public void CheckLegendCards()
    {
        // Check if narrator is at runestones
        if (index == runestoneIndex)
        {
            LegendCard runestoneCard = legendCardDeck.getCard("RunestoneCard");
            if (!legendCardDeck.isEasy && witchFound)
            {
                runestoneCard.ApplyEffect();
            }
            else if (legendCardDeck.isEasy)
            {
                runestoneCard.ApplyEffect();
            }
        }

        if (index == 2) // C
        {
            LegendCard c1 = legendCardDeck.getCard("C1");
            LegendCard c2 = legendCardDeck.getCard("C2");
            c1.ApplyEffect();
            c2.ApplyEffect();
        }

        if (index == 6) // G
        {
            LegendCard g = legendCardDeck.getCard("G");
            g.ApplyEffect();
        }

        if (index == 13) // N
        {
            LegendCard n = legendCardDeck.getCard("N");
            n.ApplyEffect();
        }
    }

    public void TriggerWitchCard()
    {
        witchFound = true;
        LegendCard witchCard = legendCardDeck.getCard("WitchCard");
        witchCard.ApplyEffect();
    }

    public void setRunestonePosition()
    {
        Random rand = new Random();
        int roll = rand.Next(1, 6);
        switch (roll)
        {
            case 1:
                runestoneIndex = 1;
                runestoneLetter = 'B';
                break;
            case 2:
                runestoneIndex = 3;
                runestoneLetter = 'D';
                break;
            case 3:
                runestoneIndex = 4;
                runestoneLetter = 'E';
                break;
            case 4:
                runestoneIndex = 5;
                runestoneLetter = 'F';
                break;
            case 5:
                runestoneIndex = 5;
                runestoneLetter = 'F';
                break;
            case 6:
                runestoneIndex = 7;
                runestoneLetter = 'H';
                break;
        }
    }
}
