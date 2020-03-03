using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;

public class ChooseManager : MonoBehaviour
{
    public static readonly int playerCount = 4;
    public PlayerCard[] PlayerCards;
    private Queue<Player> playerTurn;
    public PhotonView pv;
    public List<Player> players = PhotonNetwork.PlayerList.ToList();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
