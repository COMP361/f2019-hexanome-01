using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopups : Singleton<ErrorPopups>
{
    // Start is called before the first frame update

    GameObject errorPanel;
    Text errorPanelTitle;
    Text errorPanelDesc;


    Button okBtn;
    void OnEnable() {

      EventManager.Error += ShowErrorPanel;
    }


    void OnDisable() {

      EventManager.Error -= ShowErrorPanel;
    }

    void Start(){
      errorPanel = transform.Find("Error Panel").gameObject;
      errorPanelTitle = errorPanel.transform.Find("Title").GetComponent<Text>();
      errorPanelDesc = errorPanel.transform.Find("Description").GetComponent<Text>();

      okBtn = errorPanel.transform.Find("OK Button").GetComponent<Button>();
      okBtn.onClick.AddListener(delegate { HideErrorPanel(); });
    }

    void HideErrorPanel() {
      errorPanel.SetActive(false);
    }

    public void ShowErrorPanel(int type){
      if(type == 0){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "Not enough gold.";
      }
      if(type == 1){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "Not enough space in your inventory.";
      }
      errorPanel.SetActive(true);
    }
}
