using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetwork : MonoBehaviourPunCallbacks, IConnectionCallbacks, ILobbyCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
    }	
    
    public override void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.AutomaticallySyncScene = true; // this will sync every player to the scene the master player is on.
        PhotonNetwork.NickName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby.");

        if(!PhotonNetwork.InRoom)
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    }
}
