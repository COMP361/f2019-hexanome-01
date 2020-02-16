using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject panel;

    void Awake()
    {
        panel = this.gameObject;
        Hide();

        EventManager.GameOver += Show;
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
