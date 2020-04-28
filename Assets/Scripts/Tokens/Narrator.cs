using UnityEngine;
using System;
using Random = System.Random;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Narrator {
    public int index; // 0 -> A
    public int runestoneIndex;
    public char runestoneLetter;
    public int herbRoll;
    public int herbCellId;
    public int towerSkralCell;
    public int runestoneRoll;
    public int[] runestoneCells;
    public bool witchFound;
    public bool medicineDelivered;
    public bool towerSkralDefeated;
    public LegendCardDeck legendCardDeck;
    public TMP_Text TasksListText;
    GameObject narratorToken;
    GameObject runestoneToken;
    Dictionary<String, String> tasks;

    public Narrator()
    {
        EventManager.Save += Save;
        EventManager.HerbInCastle += HerbFound;

        index = 0;
        witchFound = false;
        legendCardDeck = new LegendCardDeck(false);
        herbRoll = 1;
        if(!PhotonNetwork.OfflineMode) herbRoll = (int)PhotonNetwork.CurrentRoom.CustomProperties["HerbRoll"];

        if(herbRoll == 1 || herbRoll == 2) {
            herbCellId = 37;
        } else if (herbRoll == 3 || herbRoll == 4) {
            herbCellId = 67;
        } else {
            herbCellId = 61;
        }

        towerSkralCell = 51;
        if(!PhotonNetwork.OfflineMode) towerSkralCell = (int)PhotonNetwork.CurrentRoom.CustomProperties["TowerSkralCell"];
        runestoneRoll = 4;
        if(!PhotonNetwork.OfflineMode) runestoneRoll = (int)PhotonNetwork.CurrentRoom.CustomProperties["RunestoneCardPosition"];
        runestoneCells = new int[] { 23, 45, 64, 73, 49 };
        if(!PhotonNetwork.OfflineMode) runestoneCells = PhotonNetwork.CurrentRoom.CustomProperties["RunestoneCells"] as int[];
        SetRunestonePosition();

        TasksListText = GameObject.Find("TasksListText").GetComponent<TMP_Text>();
        tasks = new Dictionary<String, String>();
        tasks.Add("Witch", "- Find the Witch hidden in the fog \n");
        UpdateTasks();

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

    ~Narrator()
    {
        EventManager.Save -= Save;
        EventManager.HerbInCastle -= HerbFound;
    }

    public void MoveNarrator()
    {
        if (index < 13)
        {
            index++;
        }
        char letter = (char)(index + 65);
        narratorToken.transform.position = GameObject.Find("Narrator/" + letter + "/Narrator").transform.position;
    }

    public void MoveNarratorToIndex(int indexToMove)
    {
        if (indexToMove > 0 && indexToMove <= 13)
        {
            index = indexToMove;
            char letter = (char)(index + 65);
            narratorToken.transform.position = GameObject.Find("Narrator/" + letter + "/Narrator").transform.position;
        }
    }

    public void CheckLegendCards()
    {
        // Check if narrator is at runestones
        if (index == runestoneIndex)
        {
            LegendCard runestoneCard = legendCardDeck.getCard("RunestoneCard");
            witchFound = true;//REMOVE AFTER
            if (!legendCardDeck.isEasy && witchFound) {
                runestoneCard.ApplyEffect();
            } else if (legendCardDeck.isEasy) {
                runestoneCard.ApplyEffect();
            }
        }

        if (index == 0) // A
        {
            LegendCard A3 = legendCardDeck.getCard("A3");
            LegendCard A4 = legendCardDeck.getCard("A4");
            A3.ApplyEffect();
            A4.ApplyEffect();
        }

        if (index == 2) // C
        {
            LegendCard c1 = legendCardDeck.getCard("C1");
            LegendCard c2 = legendCardDeck.getCard("C2");
            c1.ApplyEffect();
            c2.ApplyEffect();
            tasks.Add("Tower Skrall", "- Kill the Tower Skral \n");
            UpdateTasks();
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

    void UpdateTasks() {
        TasksListText.text = "";
        foreach (KeyValuePair<string, string> entry in tasks) {
            TasksListText.text += entry.Value;
        }
    }

    void HerbFound() {
        tasks.Remove("Herb");
        UpdateTasks();
    }

    public void TowerSkralDefeated() {
        tasks.Remove("Tower Skrall");
        UpdateTasks();
    }

    public void TriggerWitchCard()
    {
        witchFound = true;
        LegendCard witchCard = legendCardDeck.getCard("WitchCard");
        witchCard.ApplyEffect();
        tasks.Remove("Witch");
        tasks.Add("Herb", "- Bring the Herb (Cell " + herbCellId + ") to the Castle \n");
        UpdateTasks();
    }

    // Decide when the runestone card will be triggered
    public void SetRunestonePosition()
    {
        switch (runestoneRoll)
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

    public void Save(String saveId)
    {
        String _gameDataId = "Narrator.json";
        FileManager.Save(Path.Combine(saveId, _gameDataId), new NarratorState());
    }

    public void Load(NarratorState narratorState)
    {
        this.index = narratorState.index;
        this.runestoneIndex = narratorState.runestoneIndex;
        this.runestoneLetter = narratorState.runestoneLetter;
        this.herbRoll = narratorState.herbRoll;
        this.towerSkralCell = narratorState.towerSkralCell;
        this.runestoneRoll = narratorState.runestoneRoll;
        this.runestoneCells = narratorState.runestoneCells;
        this.witchFound = narratorState.witchFound;
        this.medicineDelivered = narratorState.medicineDelivered;
        this.towerSkralDefeated = narratorState.towerSkralDefeated;
        MoveNarratorToIndex(this.index);
        SetRunestonePosition();

        tasks = new Dictionary<String, String>();
        if(!witchFound) {
            tasks.Add("Witch", "- Find the Witch hidden in the fog \n");
        } else if(!medicineDelivered) {
            tasks.Add("Herb", "- Bring the Herb (Cell " + herbCellId + ") to the Castle \n");
        }

        if(!towerSkralDefeated && index >= 2) tasks.Add("Tower Skrall", "- Kill the Tower Skral \n");

        UpdateTasks();
    }
}

[Serializable]
public class NarratorState
{
    public int index;
    public int runestoneIndex;
    public char runestoneLetter;
    public int herbRoll;
    public int towerSkralCell;
    public int runestoneRoll;
    public int[] runestoneCells;
    public bool witchFound;
    public bool medicineDelivered;
    public bool towerSkralDefeated;

    public NarratorState()
    {
        Narrator narrator = GameManager.instance.narrator;
        index = narrator.index;
        runestoneIndex = narrator.runestoneIndex;
        runestoneLetter = narrator.runestoneLetter;
        herbRoll = narrator.herbRoll;
        towerSkralCell = narrator.towerSkralCell;
        runestoneRoll = narrator.runestoneRoll;
        runestoneCells = narrator.runestoneCells;
        witchFound = narrator.witchFound;
        medicineDelivered = narrator.medicineDelivered;
        towerSkralDefeated = narrator.towerSkralDefeated;
    }
}
