using UnityEngine;
using System;
using Random = System.Random;

public class Narrator : MonoBehaviour
{
    public int index; // 0 -> A
    public int runestoneIndex;
    public char runestoneLetter;
    GameObject token;

    public LegendCardDeck legendCardDeck;

    public Narrator()
    {
        index = 0;
        setRunestonePosition();
        legendCardDeck = new LegendCardDeck(false);

        token = new GameObject("Narrator");
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Narrator");
        SpriteRenderer sr = token.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 3;
        token.transform.localScale = new Vector3(15, 15, 15);
        token.transform.parent = GameObject.Find("Tokens").transform;
        token.transform.position = GameObject.Find("Narrator/A/Narrator").transform.position;

        GameObject runestoneToken = new GameObject("NarratorRunestone");
        Sprite runestoneSprite = Resources.Load<Sprite>("Sprites/Tokens/Stone/Stone-Blue");
        SpriteRenderer sr_runestone = runestoneToken.AddComponent<SpriteRenderer>();
        sr_runestone.sprite = runestoneSprite;
        sr_runestone.sortingOrder = 2;
        runestoneToken.transform.localScale = new Vector3(15, 15, 15);
        runestoneToken.transform.parent = GameObject.Find("Tokens").transform;
        runestoneToken.transform.position = GameObject.Find("Narrator/" + runestoneLetter + "/Runestone").transform.position;
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

    public void MoveNarrator()
    {
        if (index < 78)
        {
            index++;
        }
        char letter = (char)(index + 65);
        token.transform.position = GameObject.Find("Narrator/" + letter + "/Narrator").transform.position;
    }
}
