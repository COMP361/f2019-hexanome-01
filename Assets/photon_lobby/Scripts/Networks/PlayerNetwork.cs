using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviourPunCallbacks
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    public Text _playerName;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        if (_playerName.text.Equals("")) PlayerName = "Player#" + Random.Range(0, 100);
        else PlayerName = _playerName.text;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void saveUsername()
    {
        PlayerName = _playerName.text;
        PhotonNetwork.LocalPlayer.NickName = _playerName.text;
    }

    public void Update()
    {
        //saveUsername();
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Pregame_CharacterSelection")
        {
            if (PhotonNetwork.IsMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {

    }

    private void NonMasterLoadedGame()
    {

    }
}
