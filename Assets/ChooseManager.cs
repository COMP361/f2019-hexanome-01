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
    public PhotonView pv;
    public List<Player> players = PhotonNetwork.PlayerList.ToList();
    private List<Button> buttons = new List<Button>();
    public Button archer;
    public Button warrior;
    public Button mage;
    public Button dwarf;


    // Start is called before the first frame update
    void Awake()
    {
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
        if (playerTurn.Peek().Equals(PhotonNetwork.LocalPlayer))
        {
            foreach (Button b in buttons)
            {
                b.enabled = true;
            }
        }
        else
        {
            archer.enabled = false;
            warrior.enabled = false;
            mage.enabled = false;
            dwarf.enabled = false;
        }// unsure to put it in update
    }

    public void OnClickedButton()
    {
        var b = GameObject.Find("newChooseHero/Canvas/" + EventSystem.current.currentSelectedGameObject.name.ToString()).GetComponent<Button>();
        Button button = (Button) b;

        ExitGames.Client.Photon.Hashtable classTable = new ExitGames.Client.Photon.Hashtable();
        classTable.Add("Class", button.name);
        PhotonNetwork.LocalPlayer.SetCustomProperties(classTable);

        buttons.Remove(button);
        playerTurn.Dequeue();
    }

}
