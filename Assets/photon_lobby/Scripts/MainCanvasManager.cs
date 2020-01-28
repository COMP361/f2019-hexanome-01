using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MainCanvasManager : MonoBehaviourPunCallbacks
{
    public static MainCanvasManager Instance;

    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    private void Awake()
    {
        Instance = this;
    }

}
