using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerLayoutGroup : MonoBehaviourPunCallbacks, IMatchmakingCallbacks, IInRoomCallbacks
{

    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return _playerListings; }
    }

    //Called by photon whenever the master client is swithced.
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }


    //Called by photon whenever you join a room.
    public override void OnJoinedRoom()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling();

        Player[] photonPlayers = PhotonNetwork.PlayerList;
        for (int i = 0; i < photonPlayers.Length; i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    //Called by photon when a player joins the room.
    public override void OnPlayerEnteredRoom(Player player)
    {
        print("OnPhotonPlayerConnected called.");
        PlayerJoinedRoom(player);
    }

    //Called by photon when a player leaves the room.
    private void OnPhotonPlayerDisconnected(Player player)
    {
        PlayerLeftRoom(player);
    }


    private void PlayerJoinedRoom(Player player)
    {
        if (player == null)
            return;

        PlayerLeftRoom(player); // Prevents getting duplicates.

        GameObject playerListingObj = Instantiate(PlayerListingPrefab);
        playerListingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(player);

        PlayerListings.Add(playerListing);
    }

    private void PlayerLeftRoom(Player player)
    {
        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == player);
        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }

    public void OnClickRoomState()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = !PhotonNetwork.CurrentRoom.IsOpen;
        PhotonNetwork.CurrentRoom.IsVisible = !PhotonNetwork.CurrentRoom.IsVisible;

        print("Locked the current room! Nobody should be able to enter until you reactivate it.\n" +
            "isVisible is now set to: " + PhotonNetwork.CurrentRoom.IsVisible);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
