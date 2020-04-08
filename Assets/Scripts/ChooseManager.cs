﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Linq;
using Photon.Realtime;
using UnityEngine.EventSystems;
using Random = System.Random;

public class ChooseManager : MonoBehaviour
{
    public static readonly int playerCount = PhotonNetwork.PlayerList.ToList().Count();
    public PlayerCard[] PlayerCards;
    private Queue<Player> playerTurn;
    public List<Player> players = PhotonNetwork.PlayerList.ToList();
    private List<Button> buttons = new List<Button>();
    public Button archer;
    public Button warrior;
    public Button mage;
    public Button dwarf;
    public Button confirmButton;
    public Button startButton;
    public Button loadButton;
    public Text turnText;
    public string currentSelection;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Awake()
    {
        startButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        
        playerTurn = new Queue<Player>();
        foreach(Player p in players)
        {
            playerTurn.Enqueue(p);
        }

        buttons.Add(archer);
        buttons.Add(warrior);
        buttons.Add(mage);
        buttons.Add(dwarf);

        archer.interactable = false;
        warrior.interactable = false;
        mage.interactable = false;
        dwarf.interactable = false;
        confirmButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        // When all players are done choosing, enable start game button
        if (playerTurn.Count == 0 && PhotonNetwork.IsMasterClient)
        {
            turnText.text = "Time to start the game!";
            startButton.gameObject.SetActive(true);
            loadButton.gameObject.SetActive(true);
        }
        else if (playerTurn.Count != 0 && playerTurn.Peek().Equals(PhotonNetwork.LocalPlayer))
        {
            turnText.text = "It is your turn to pick!";
            foreach (Button b in buttons)
            {
                b.interactable = true;
            }
            confirmButton.interactable = true;
        }
        else
        {
            turnText.text = "Waiting for other players...";
        }
    }

    public void OnClickedClassButton(Button button)
    {
        currentSelection = button.name;
    }

    public void OnClickedConfirmButton()
    {
        ExitGames.Client.Photon.Hashtable classTable = new ExitGames.Client.Photon.Hashtable();
        classTable.Add("Class", currentSelection);
        PhotonNetwork.LocalPlayer.SetCustomProperties(classTable);
            
        if(!PhotonNetwork.OfflineMode) {
            photonView.RPC("ReceiveOnClickedConfirmButton", RpcTarget.All, currentSelection);
        } else {
            ReceiveOnClickedConfirmButton(currentSelection);
        }
    }

    [PunRPC]
    public void ReceiveOnClickedConfirmButton(string selection)
    {
        Button btn = GameObject.Find(selection).GetComponent<Button>();
        btn.enabled = false;
        buttons.RemoveAll(x => x.name == selection);
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }
        playerTurn.Dequeue();
        confirmButton.interactable = false;
    }

    public void OnClickStartButton()
    {
        // Add where the tower skral will spawn;
        Random rand = new Random();
        int towerSkralCell = rand.Next(1, 6) + 50;
        ExitGames.Client.Photon.Hashtable towerSkralTable = new ExitGames.Client.Photon.Hashtable();
        towerSkralTable.Add("TowerSkralCell", towerSkralCell);
        PhotonNetwork.CurrentRoom.SetCustomProperties(towerSkralTable);
        // Add where runestone card will be
        int roll = rand.Next(1, 6);
        ExitGames.Client.Photon.Hashtable runestoneCardTable = new ExitGames.Client.Photon.Hashtable();
        runestoneCardTable.Add("RunestoneCardPosition", roll);
        PhotonNetwork.CurrentRoom.SetCustomProperties(runestoneCardTable);
        // Add where runestones will spawn
        for (int i = 1; i <= 5; i++)
        {
            int tens = rand.Next(1, 6);
            int ones = rand.Next(1, 6);
            int runestoneCell = tens * 10 + ones;
            ExitGames.Client.Photon.Hashtable runestoneTable = new ExitGames.Client.Photon.Hashtable();
            runestoneTable.Add("RunestoneCell" + i, runestoneCell);
            PhotonNetwork.CurrentRoom.SetCustomProperties(runestoneTable);
        }
        PhotonNetwork.LoadLevel(3);
    }
}
