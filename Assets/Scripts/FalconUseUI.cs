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


    ArcherBtn.interactable = false;
    DwarfBtn.interactable = false;
    MageBtn.interactable = false;
    WarriorBtn.interactable = false;
    
  }

  public void ShowFalconUse(Falcon item) {
    token = item;
    FalconUsePanelTitle.text = "Falcon Action";
    FalconUsePanelDesc.text = "With the Falcon, you can make trades with heroes who are not on the same cell as you. Choose a hero with who you want to make a trade.";

    if(canMakeTrade("Archer")){
      ArcherBtn.interactable = true;
    }
    if(canMakeTrade("Dwarf")){
      DwarfBtn.interactable = true;
    }
    if(canMakeTrade("Mage")){
      MageBtn.interactable = true;
    }
    if(canMakeTrade("Warrior")){
      WarriorBtn.interactable = true;
    }

    FalconUsePanel.SetActive(true);
  }

  public void HideFalconUse() {
    this.token = null;
    ArcherBtn.interactable = false;
    DwarfBtn.interactable = false;
    MageBtn.interactable = false;
    WarriorBtn.interactable = false;
    FalconUsePanel.SetActive(false);
  }


  public void clearToken(){
    this.token = null;
  }


  public bool canMakeTrade(string heroName){
    //can not trade to yourself
    if(heroName.Equals(GameManager.instance.MainHero.TokenName)){
      return false;
    }
    Hero mainHero = GameManager.instance.MainHero;
    Hero toCheck = GameManager.instance.findHero(heroName);

    //if hero not in game
    if(toCheck == null){
      return false;
    }

    // Return true only if both mainHero AND hero to trade to do not have empty inventories
    if((toCheck.heroInventory.helm != null || toCheck.heroInventory.golds.Count != 0 || mainHero.heroInventory.smallTokens.Count != 0) && (toCheck.heroInventory.helm != null || mainHero.heroInventory.golds.Count != 0 || mainHero.heroInventory.smallTokens.Count != 0) ){
      return true;
    }
    return false;
  }
}
