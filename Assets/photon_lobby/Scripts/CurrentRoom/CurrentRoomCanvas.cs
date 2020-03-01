﻿using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CurrentRoomCanvas : MonoBehaviourPunCallbacks
{

    public void OnClickStartSync()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.LoadLevel(1);
        //PhotonNetwork.LoadLevel(2);
    }

    public void OnClickStartDelayed()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
