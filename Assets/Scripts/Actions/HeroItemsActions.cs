using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

public class HeroItemsActions : MonoBehaviour
{

  GameObject heroItemsPanel;
  Button cancelBtn, dropBtn, useBtn;
  public PhotonView photonView;
  Token token;

  Text heroItemsPanelTitle;
  Text heroItemsPanelDesc;

  bool canUseMoveItems;
  bool isInRoundFight;


  void OnEnable() {
    EventManager.HeroItemClick += ShowHeroActions;
    EventManager.MoveSelect += UnlockMoveItems;
    EventManager.LockMoveItems += LockMoveItems;
    EventManager.StartRoundFight += updateRoundFight;
    EventManager.EndRoundFight += updateRoundFightEnd;
    }

  void OnDisable() {
    EventManager.HeroItemClick -= ShowHeroActions;
    EventManager.MoveSelect -= UnlockMoveItems;
    EventManager.LockMoveItems -= LockMoveItems;
    EventManager.StartRoundFight -= updateRoundFight;
    EventManager.EndRoundFight -= updateRoundFightEnd;
  }


  void Awake() {
      canUseMoveItems = false;
      bool isInRoundFight = false;
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

  void updateRoundFight(){
    isInRoundFight = true;
  }

  void updateRoundFightEnd(){
    isInRoundFight = false;
  }


  public void ShowHeroActions(Token item) {
      token = item;

      if(token is Wineskin){
        heroItemsPanelTitle.text = Wineskin.itemName;
        heroItemsPanelDesc.text = Wineskin.desc;
        if(!canUseMoveItems){
          useBtn.interactable = false;
        }
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      if(token is HalfWineskin){
        heroItemsPanelTitle.text = HalfWineskin.itemName;
        heroItemsPanelDesc.text = HalfWineskin.desc;
        if(!canUseMoveItems){
          useBtn.interactable = false;
        }
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Potion){
        heroItemsPanelTitle.text = Potion.itemName;
        heroItemsPanelDesc.text = Potion.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Bow){
        heroItemsPanelTitle.text = Bow.itemName;
        heroItemsPanelDesc.text = Bow.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Falcon){
        heroItemsPanelTitle.text = Falcon.itemName;
        heroItemsPanelDesc.text = Falcon.desc;
        if(!canUseFalcon()){
          useBtn.interactable = false;
        }
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Helm){
        heroItemsPanelTitle.text = Helm.itemName;
        heroItemsPanelDesc.text = Helm.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Herb){
        heroItemsPanelTitle.text = Herb.itemName;
        heroItemsPanelDesc.text = Herb.desc;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Runestone){
        heroItemsPanelTitle.text = Runestone.itemName;
        heroItemsPanelDesc.text = Runestone.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Shield){
        heroItemsPanelTitle.text = Shield.itemName;
        heroItemsPanelDesc.text = Shield.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }
      else if(token is Telescope){
        heroItemsPanelTitle.text = Telescope.itemName;
        heroItemsPanelDesc.text = Telescope.desc;
        if(!canUseTelescope()){
         useBtn.interactable = false;
        }
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }

      else if(token is GoldCoin){
        heroItemsPanelTitle.text = GoldCoin.itemName;
        heroItemsPanelDesc.text = GoldCoin.desc;
        useBtn.interactable = false;
        if(GameManager.instance.MainHero.IsFighting){
          dropBtn.interactable = false;
          useBtn.interactable = false;
        }
      }


      heroItemsPanel.SetActive(true);
  }



  public void HideHeroActions() {
    this.token = null;
    useBtn.interactable = true;
    dropBtn.interactable = true;
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

  public void UnlockMoveItems(){
    canUseMoveItems = true;
  }
  public void LockMoveItems(){
    canUseMoveItems = false;
  }

  public bool canUseTelescope(){
    Hero hero = GameManager.instance.MainHero;
    List<Transform> cellsToCheck = hero.Cell.neighbours;
    foreach(Transform toCheck in cellsToCheck){
      foreach(Token item in toCheck.GetComponent<Cell>().Inventory.AllTokens){
        if(item is Fog){
           return true;
          }
        }
        foreach(DictionaryEntry item in toCheck.GetComponent<Cell>().Inventory.items){
          if( item.Value is Runestone){
            if(((Runestone)item.Value).isCovered){
              return true;
            }
          }
        }
      }
    return false;
  }

  public bool canUseFalcon(){
    List<Hero> Heroes = GameManager.instance.heroes;
    if(((Falcon)token).isTurned){
      return false;
    }
    foreach (Hero hero in Heroes){
      if(hero.heroInventory.helm != null || hero.heroInventory.golds.Count != 0 || hero.heroInventory.smallTokens.Count != 0){
        return true;
      }
    }
    return false;
  }



}
