using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyCanvas : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomLayoutGroup RoomLayoutGroup { get; set; }

    public void OnClickJoinRoom(string roomName)
    {
        if (!PhotonNetwork.JoinRoom(roomName))
        {
            print("Join room failed.");
        }
    }

}
