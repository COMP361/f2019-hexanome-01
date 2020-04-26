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
        errorPanelDesc.text = "Not enough gold to buy this article.";
      }
      if(type == 1){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "Not enough space in your inventory.";
      }
      if(type == 2){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You cannot execute a free action when your hero is located on the Sunrise cell of the timeline.";
      }
      if(type == 3){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You are already using this item. Please select another item or continue.";
      }
      if(type == 4){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You want to make a trade involving no item. Please make another trade or cancel.";
      }
      if(type == 5){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You can not make that trade as one of the hero would have too much items. Please make another trade or cancel.";
      }
      if(type == 6){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You can not make that trade as the other hero is currently participating in a fight round. Please make another trade or cancel.";
      }
      if(type == 7){
        errorPanelTitle.text = "Error!";
        errorPanelDesc.text = "You are using more free moves than the number of cells you want to move accross! Change the number of free moves and try again.";
      }
      errorPanel.SetActive(true);
    }
}
