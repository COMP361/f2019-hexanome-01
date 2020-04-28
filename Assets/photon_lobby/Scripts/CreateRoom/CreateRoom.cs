using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class CreateRoom : MonoBehaviourPunCallbacks, IMatchmakingCallbacks
{

    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("create room successfully sent.");
        }
        else
        {
            print("create room failed to send");
        }
    }

    /*TO REVIEW : REFACTORING OF THIS METHOD'S INPUTS*/
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed: " + codeAndMessage[1]);
    }

    public override void OnJoinedRoom() {
        GameObject btn = GameObject.Find("Canvas/CurrentRoom/StartMatchSync").gameObject;

        if (PhotonNetwork.IsMasterClient) {
            btn.SetActive(true);
        } else {
            btn.SetActive(false);
        }
    }
}