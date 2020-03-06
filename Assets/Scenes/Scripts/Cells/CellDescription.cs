using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CellDescription : MonoBehaviour
{

    public GameObject panel;
    private string description;

    // Use this for initialization


    void Awake()
    {
        formatDescription();
        panel = GameObject.Find("Popup");
        panel.SetActive(false);
    }

    void OnMouseEnter()
    {
        formatDescription();
        panel.transform.FindDeepChild("Text").GetComponent<UnityEngine.UI.Text>().text = description;
        panel.SetActive(true);

    }

    void OnMouseExit()
    {
        panel.SetActive(false);
    }


    void formatDescription()
    {
      this.description = "Heroes: \n";
      this.description = description + "Item: \n";
      this.description = description + "Monster: \n";
      this.description = description + "Gold: \n";
    }
}
