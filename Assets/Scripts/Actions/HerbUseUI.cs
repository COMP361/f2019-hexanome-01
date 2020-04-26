using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HerbUseUI : MonoBehaviour
{

  GameObject HerbUsePanel;
  Button cancelBtn, willpowerBtn, freeMoveBtn;
  public PhotonView photonView;
  Herb herb;

  Text HerbUsePanelTitle;
  Text HerbUsePanelDesc;

  bool canUseMoveItems;


  void OnEnable() {
    EventManager.HerbUseUI += ShowHerbUseActions;
    EventManager.MoveSelect += UnlockMoveItems;
    EventManager.LockMoveItems += LockMoveItems;
    }

  void OnDisable() {
    EventManager.HerbUseUI -= ShowHerbUseActions;
    EventManager.MoveSelect -= UnlockMoveItems;
    EventManager.LockMoveItems -= LockMoveItems;
  }


  void Awake() {
      HerbUsePanel = transform.Find("Herb Use UI").gameObject;

      HerbUsePanelTitle = HerbUsePanel.transform.Find("Panel Title").GetComponent<Text>();
      HerbUsePanelDesc = HerbUsePanel.transform.Find("Panel Description").GetComponent<Text>();

      HerbUsePanelTitle.text = "With the medicinal herb, you can either increase your willpoints or get free moves.";
      HerbUsePanelDesc.text = "Herb Action";

      herb = null;

      cancelBtn= HerbUsePanel.transform.Find("Cancel Button").GetComponent<Button>();
      cancelBtn.onClick.AddListener(delegate { changeHerbUsage();HideHerbUse(); });

      willpowerBtn = HerbUsePanel.transform.Find("WillPower Button").GetComponent<Button>();
      willpowerBtn.onClick.AddListener(delegate { clickWillpower(); });

      freeMoveBtn = HerbUsePanel.transform.Find("Free Move Button").GetComponent<Button>();
      freeMoveBtn.onClick.AddListener(delegate { EventManager.TriggerFreeMove(this.herb); HideHerbUse();});
  }


  public void ShowHerbUseActions(Herb item) {
      herb = item;
      if(!canUseMoveItems){
        freeMoveBtn.interactable = false;
      }
      else{}
      HerbUsePanel.SetActive(true);
  }

  public void HideHerbUse() {
      this.herb = null;
      freeMoveBtn.interactable = true;
      HerbUsePanel.SetActive(false);
  }

  public void changeHerbUsage() {
      this.herb.reserved = 0;

  }

  public void clickWillpower(){
    if(this.herb.myType.Equals(Herbs.Herb3)){
    Hero a = GameManager.instance.findHero(GameManager.instance.MainHero.TokenName);
    a.setWP(a.Willpower + 3);
    }
    else
    {
    Hero a = GameManager.instance.findHero(GameManager.instance.MainHero.TokenName);
    a.setWP(a.Willpower + 4);
    }
    GameManager.instance.MainHero.heroInventory.RemoveSmallToken(this.herb);
    HideHerbUse();
  }



  public void UnlockMoveItems(){
    canUseMoveItems = true;
  }
  public void LockMoveItems(){
    canUseMoveItems = false;
  }
}
