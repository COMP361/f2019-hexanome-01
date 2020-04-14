using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FalconUseUI : MonoBehaviour
{

  GameObject FalconUsePanel;
  Button ArcherBtn, DwarfBtn, MageBtn, WarriorBtn, CancelBtn;
  public PhotonView photonView;
  Falcon token;

  Text FalconUsePanelTitle;
  Text FalconUsePanelDesc;


  void OnEnable() {
    EventManager.FalconUseUI += ShowFalconUse;
  }

  void OnDisable() {
    EventManager.FalconUseUI -= ShowFalconUse;
  }


  void Awake() {
    FalconUsePanel = transform.Find("Falcon Use UI").gameObject;

    FalconUsePanelTitle = FalconUsePanel.transform.Find("Panel Title").GetComponent<Text>();
    FalconUsePanelDesc = FalconUsePanel.transform.Find("Panel Description").GetComponent<Text>();

    FalconUsePanelTitle.text  = "";
    FalconUsePanelDesc.text = "";
    token = null;


    ArcherBtn = FalconUsePanel.transform.Find("Archer Button").GetComponent<Button>();
    ArcherBtn.onClick.AddListener(delegate { });

    DwarfBtn = FalconUsePanel.transform.Find("Dwarf Button").GetComponent<Button>();
    DwarfBtn.onClick.AddListener(delegate { });

    MageBtn = FalconUsePanel.transform.Find("Mage Button").GetComponent<Button>();
    MageBtn.onClick.AddListener(delegate { });

    WarriorBtn = FalconUsePanel.transform.Find("Warrior Button").GetComponent<Button>();
    WarriorBtn.onClick.AddListener(delegate { });

    CancelBtn = FalconUsePanel.transform.Find("Cancel Button").GetComponent<Button>();
    CancelBtn.onClick.AddListener(delegate {HideFalconUse(); });

  }

  public void ShowFalconUse(Falcon item) {
    token = item;
    FalconUsePanelTitle.text = "Falcon Action";
    FalconUsePanelDesc.text = "With the Falcon, you can make trades with heroes who are not on the same cell as you. Choose a hero with who you want to make a trade.";
    FalconUsePanel.SetActive(true);
  }

  public void HideFalconUse() {
    this.token = null;
    FalconUsePanel.SetActive(false);
  }


  public void clearToken(){
    this.token = null;
  }
}
