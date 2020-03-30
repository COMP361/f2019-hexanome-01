using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject panel;

    void OnEnable() {
        EventManager.GameOver += Show;
    }

    void OnDisable() {
        EventManager.GameOver -= Show;
    }

    void Awake()
    {
        panel = transform.Find("Panel").gameObject;
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
