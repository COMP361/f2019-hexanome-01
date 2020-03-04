using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;
using UnityEngine.EventSystems;

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
    public Text turnText;
    public Text selectionText;
    public string currentSelection;

    public PhotonView photonView;

    // Start is called before the first frame update
    void Awake()
    {
        startButton.gameObject.SetActive(false);
        playerTurn = new Queue<Player>();
        foreach(Player p in players)
        {
            playerTurn.Enqueue(p);
        }

        buttons.Add(archer);
        buttons.Add(warrior);
        buttons.Add(mage);
        buttons.Add(dwarf);
    }

    // Update is called once per frame
    void Update()
    {
        // When all players are done choosing, enable start game button
        if (playerTurn.Count == 0 && PhotonNetwork.IsMasterClient)
        {
            turnText.text = "Time to start the game!";
            startButton.gameObject.SetActive(true);
        }
        else if (playerTurn.Count != 0 && playerTurn.Peek().Equals(PhotonNetwork.LocalPlayer))
        {
            turnText.text = "It is your turn to pick!";
            foreach (Button b in buttons)
            {
                b.enabled = true;
            }
            confirmButton.enabled = true;
        }
        else
        {
            turnText.text = "Waiting for other players...";
            archer.enabled = false;
            warrior.enabled = false;
            mage.enabled = false;
            dwarf.enabled = false;
            confirmButton.enabled = false;
        }
    }

    public void OnClickedClassButton(Button button)
    {
        currentSelection = button.name;
        selectionText.text = "You have chosen the " + button.name;
    }

    public void OnClickedConfirmButton()
    {
        ExitGames.Client.Photon.Hashtable classTable = new ExitGames.Client.Photon.Hashtable();
        classTable.Add("Class", currentSelection);
        PhotonNetwork.LocalPlayer.SetCustomProperties(classTable);
        photonView.RPC("ReceiveOnClickedConfirmButton", RpcTarget.All, currentSelection);
    }

    [PunRPC]
    public void ReceiveOnClickedConfirmButton(string selection)
    {
        Button b = GameObject.Find(selection).GetComponent<Button>();
        b.GetComponent<Image>().color = Color.grey;
        buttons.RemoveAll(x => x.name == selection);
        playerTurn.Dequeue();
    }

    public void OnClickStartButton()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
