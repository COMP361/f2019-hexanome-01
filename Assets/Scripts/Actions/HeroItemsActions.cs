using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HeroItemsActions : MonoBehaviour
{

  GameObject heroItemsPanel;
  Button cancelBtn, dropBtn, useBtn;
  public PhotonView photonView;
  Token token;

  Text heroItemsPanelTitle;
  Text heroItemsPanelDesc;


  void OnEnable() {
    EventManager.HeroItemClick += ShowHeroActions;
    }

  void OnDisable() {
    EventManager.HeroItemClick -= ShowHeroActions;
  }


  void Awake() {
      heroItemsPanel = transform.Find("Hero Items Actions").gameObject;

      heroItemsPanelTitle = heroItemsPanel.transform.Find("Panel Title").GetComponent<Text>();
      heroItemsPanelDesc = heroItemsPanel.transform.Find("Panel Description").GetComponent<Text>();


      token = null;


      cancelBtn= heroItemsPanel.transform.Find("Cancel Button").GetComponent<Button>();
      cancelBtn.onClick.AddListener(delegate { HideHeroActions(); });


      useBtn = heroItemsPanel.transform.Find("Use Item Button").GetComponent<Button>();
      useBtn.onClick.AddListener(delegate { UseItem(); });

      dropBtn = heroItemsPanel.transform.Find("Drop Item Button").GetComponent<Button>();
      dropBtn.onClick.AddListener(delegate { DropItem(); });
  }


  public void ShowHeroActions(Token item) {
      token = item;

      if(token is Wineskin){
      heroItemsPanelTitle.text = Wineskin.name;
      heroItemsPanelDesc.text = Wineskin.desc;
      }
      else if(token is Potion){
      heroItemsPanelTitle.text = Potion.name;
      heroItemsPanelDesc.text = Potion.desc;
      useBtn.interactable = false;
      }
      else if(token is Bow){
      heroItemsPanelTitle.text = Bow.name;
      heroItemsPanelDesc.text = Bow.desc;
      useBtn.interactable = false;
      }
      else if(token is Falcon){
      heroItemsPanelTitle.text = Falcon.name;
      heroItemsPanelDesc.text = Falcon.desc;
      }
      else if(token is Helm){
      heroItemsPanelTitle.text = Helm.name;
      heroItemsPanelDesc.text = Helm.desc;
      useBtn.interactable = false;
      }
      else if(token is Herb){
      heroItemsPanelTitle.text = Herb.name;
      heroItemsPanelDesc.text = Herb.desc;
      }
      else if(token is Runestone){
      heroItemsPanelTitle.text = Runestone.name;
      heroItemsPanelDesc.text = Runestone.desc;
      useBtn.interactable = false;
      }
      else if(token is Shield){
      heroItemsPanelTitle.text = Shield.name;
      heroItemsPanelDesc.text = Shield.desc;
      useBtn.interactable = false;
      }
      else if(token is Telescope){
      heroItemsPanelTitle.text = Telescope.name;
      heroItemsPanelDesc.text = Telescope.desc;
      }

      else if(token is GoldCoin){
      heroItemsPanelTitle.text = GoldCoin.name;
      heroItemsPanelDesc.text = GoldCoin.desc;
      useBtn.interactable = false;
      }


      heroItemsPanel.SetActive(true);
  }



  public void HideHeroActions() {
      this.token = null;
      useBtn.interactable = true;
      heroItemsPanel.SetActive(false);
  }

  public void DropItem() {

      if(token is SmallToken){
        GameManager.instance.MainHero.heroInventory.RemoveSmallToken((SmallToken) this.token);
        Cell cell = GameManager.instance.MainHero.Cell;
        cell.Inventory.AddToken(this.token);
      }
      else if (token is BigToken){
        GameManager.instance.MainHero.heroInventory.RemoveBigToken((BigToken) this.token);
        Cell cell = GameManager.instance.MainHero.Cell;
        cell.Inventory.AddToken(this.token);
      }
      else if (token is Helm){
        GameManager.instance.MainHero.heroInventory.RemoveHelm((Helm) this.token);
        Cell cell = GameManager.instance.MainHero.Cell;
        cell.Inventory.AddToken(this.token);
      }
      else if (token is GoldCoin){
        GameManager.instance.MainHero.heroInventory.RemoveGold(1);
        Cell cell = GameManager.instance.MainHero.Cell;
        Token goldCoin = GoldCoin.Factory();
        cell.Inventory.AddToken(goldCoin);
      }

      HideHeroActions();
  }

  public void UseItem(){
      token.UseEffect();
      HideHeroActions();
  }



}
