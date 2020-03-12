using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour
{
    public void StartGameClick()
    {
        SceneManager.LoadScene(1);
    }
}
