using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    GameObject panel;

    void OnEnable()
    {
        EventManager.GameWin += Show;
    }

    void OnDisable()
    {
        EventManager.GameWin -= Show;
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
