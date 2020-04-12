using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FreeMoveUI : MonoBehaviour
{

  GameObject FreeMovePanel;
  Button Btn0, Btn1, Btn2, Btn3, Btn4;
  public PhotonView photonView;
  Token token;

  Text FreeMovePanelTitle;
  Text FreeMovePanelDesc;


  void OnEnable() {
    EventManager.FreeMoveUI += ShowFreeMoves;
    EventManager.ClearFreeMove += clearToken;
    }

  void OnDisable() {
    EventManager.ClearFreeMove -= clearToken;
  }


  void Awake() {
      FreeMovePanel = transform.Find("Free Move UI").gameObject;

      FreeMovePanelTitle = FreeMovePanel.transform.Find("Panel Title").GetComponent<Text>();
      FreeMovePanelDesc = FreeMovePanel.transform.Find("Panel Description").GetComponent<Text>();

      FreeMovePanelTitle.text  = "Wineskin";
      FreeMovePanelDesc.text = "How many free moves do you want to use from your wineskin";
      token = null;


      Btn0 = FreeMovePanel.transform.Find("Btn0").GetComponent<Button>();
      Btn0.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(0, this.token); });

      Btn1 = FreeMovePanel.transform.Find("Btn1").GetComponent<Button>();
      Btn1.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(1, this.token); });

      Btn2 = FreeMovePanel.transform.Find("Btn2").GetComponent<Button>();
      Btn2.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(2, this.token); });

      Btn3 = FreeMovePanel.transform.Find("Btn3").GetComponent<Button>();
      Btn3.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(3, this.token); });

      Btn4 = FreeMovePanel.transform.Find("Btn4").GetComponent<Button>();
      Btn4.onClick.AddListener(delegate { HideFreeMove(); EventManager.TriggerFreeMoveCount(4, this.token); });

  }


  public void ShowFreeMoves(Token item, int PathSize) {
      token = item;
      if(token is Wineskin){
        FreeMovePanelTitle.text = Wineskin.itemName;
        FreeMovePanelDesc.text = "How many free moves do you want to use from your" + Wineskin.itemName;
        disableButtons(PathSize);
        Btn3.interactable = false;
        Btn4.interactable = false;
      }
      
      if(token is HalfWineskin){
        FreeMovePanelTitle.text = HalfWineskin.itemName;
        FreeMovePanelDesc.text = "How many free moves do you want to use from your" + HalfWineskin.itemName;
        disableButtons(PathSize);
        Btn2.interactable = false;
        Btn3.interactable = false;
        Btn4.interactable = false;
      }
      FreeMovePanel.SetActive(true);
  }

  public void HideFreeMove(){
      Btn1.interactable = true;
      Btn2.interactable = true;
      Btn3.interactable = true;
      Btn4.interactable = true;
      FreeMovePanel.SetActive(false);
  }

  public void clearToken(){
    this.token = null;
  }

  public void disableButtons(int PathSize){
    if(PathSize == 2){
      Btn2.interactable = false;
      Btn3.interactable = false;
      Btn4.interactable = false;
    }
    else if(PathSize == 3){
      Btn3.interactable = false;
      Btn4.interactable = false;
    }
    else if(PathSize == 4){
      Btn4.interactable = false;
    }
  }
}
