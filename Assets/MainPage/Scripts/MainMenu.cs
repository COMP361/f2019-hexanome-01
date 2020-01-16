using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("YOU CLOSED THE GAME!");
        Application.Quit();
    }

    public void CreateGameClick()
    {
        Debug.Log("test");
        SceneManager.LoadScene("PreGame_OnlineLobby");
    }
}
