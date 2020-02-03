using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviourPunCallbacks
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }


    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        PlayerName = "Player#" + Random.Range(0, 100);

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
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
