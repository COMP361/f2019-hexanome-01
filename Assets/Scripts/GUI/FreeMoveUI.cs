using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class FreeMoveUI : MonoBehaviour
{
  GameObject FreeMovePanel;
  List<Button> Buttons;
  public PhotonView photonView;
  Token token;
  Text FreeMovePanelTitle;
  Text FreeMovePanelDesc;

  void OnEnable() {
    EventManager.FreeMove += ShowFreeMoves;
    EventManager.ClearFreeMove += ClearToken;
  }

  void OnDisable() {
    EventManager.FreeMove -= ShowFreeMoves;
    EventManager.ClearFreeMove -= ClearToken;
  }

  void Awake() {
    FreeMovePanel = transform.Find("Free Move UI").gameObject;

    FreeMovePanelTitle = FreeMovePanel.transform.Find("Panel Title").GetComponent<Text>();
    FreeMovePanelDesc = FreeMovePanel.transform.Find("Panel Description").GetComponent<Text>();

    FreeMovePanelTitle.text  = "Wineskin";
    FreeMovePanelDesc.text = "How many free moves do you want to use from your wineskin";
    token = null;
    
    Buttons = new List<Button>();
    Button btn;
    
    btn = FreeMovePanel.transform.Find("Btn0").GetComponent<Button>();
    btn.onClick.AddListener(delegate { 
      this.token.reserved = 0; 
      EventManager.TriggerFreeMoveCount(this.token); 
      HideFreeMove(); 
    });
    Buttons.Add(btn);

    btn = FreeMovePanel.transform.Find("Btn1").GetComponent<Button>();
    btn.onClick.AddListener(delegate { 
      his.token.reserved = 1; 
      EventManager.TriggerFreeMoveCount(this.token); 
      HideFreeMove();
    });
    Buttons.Add(btn);

    btn = FreeMovePanel.transform.Find("Btn2").GetComponent<Button>();
    btn.onClick.AddListener(delegate {
      his.token.reserved = 0; 
      EventManager.TriggerFreeMoveCount(this.token); 
      HideFreeMove();
    });
    Buttons.Add(btn);

    btn = FreeMovePanel.transform.Find("Btn3").GetComponent<Button>();
    btn.onClick.AddListener(delegate { 
      his.token.reserved = 3; 
      EventManager.TriggerFreeMoveCount(this.token); 
      HideFreeMove();
    });
    Buttons.Add(btn);

    btn = FreeMovePanel.transform.Find("Btn4").GetComponent<Button>();
    btn.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(4, this.token); });
    Buttons.Add(btn);
  }

  void OnClick(int count) {
    this.token.reserved = count; 
    EventManager.TriggerFreeMoveCount(this.token); 
    HideFreeMove();
  }
  
  public void ShowFreeMoves(Token item) {
    token = item;
    if(token is Wineskin){
      FreeMovePanelTitle.text = Wineskin.itemName;
      FreeMovePanelDesc.text = "How many free moves do you want to use from your" + Wineskin.itemName;
    }

    if(token is HalfWineskin){
      FreeMovePanelTitle.text = HalfWineskin.itemName;
      FreeMovePanelDesc.text = "How many free moves do you want to use from your" + HalfWineskin.itemName;
    }

    if(token is Herb){
      if(((Herb)item).myType.Equals(Herbs.Herb3)){
        FreeMovePanelTitle.text = Herb.itemName + "3";
        FreeMovePanelDesc.text = "How many free moves do you want to use from your" + Herb.itemName + "3";
      } else {
        FreeMovePanelTitle.text = Herb.itemName + "4";
        FreeMovePanelDesc.text = "How many free moves do you want to use from your" + Herb.itemName + "4";
      }
    }

    for(int i = token.maxUse + 1; i < Buttons.Count; i++) {
      Buttons[i].interactable = false;
    }

    Buttons[token.reserved].Select();

    FreeMovePanel.SetActive(true);
  }

  public void HideFreeMove(){
    for(int i = 0; i < Buttons.Count; i++) {
      Buttons[i].interactable = true;
    }

    FreeMovePanel.SetActive(false);
  }

  public void ClearToken(){
    this.token = null;
  }

  /*public void DisableButtons(int PathSize){
    if(PathSize == 2){
      Btn2.interactable = false;
      Btn3.interactable = false;
      Btn4.interactable = false;
    } else if(PathSize == 3){
      Btn3.interactable = false;
      Btn4.interactable = false;
    } else if(PathSize == 4){
      Btn4.interactable = false;
    }
  }*/
}
