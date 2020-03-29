using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;


public class MerchantCellUI : Singleton<MerchantCellUI>
{

  GameObject merchantPanel;
  GameObject buyStrengthPanel;
  protected Transform titleTransform;
  
  void OnEnable() {
    EventManager.CurrentPlayerUpdate += LockItems;
    EventManager.CellUpdate += LockItems; 
    EventManager.MainHeroInit += LockItems;
    EventManager.MerchCellMouseEnter += Enter;  
  }

  void OnDisable() {
    EventManager.CurrentPlayerUpdate -= LockItems; 
    EventManager.CellUpdate -= LockItems; 
    EventManager.MainHeroInit -= LockItems;
    EventManager.MerchCellMouseEnter -= Enter;
  }

  void Awake() {
    merchantPanel = transform.Find("MerchantUI").gameObject;
    buyStrengthPanel = transform.FindDeepChild("BuyStrength").gameObject;
    titleTransform = transform.FindDeepChild("MerchantTitle");
  }

  void Start() {
    transform.Find("MerchantUI/Potion/Button/Text").GetComponent<UnityEngine.UI.Text>().text = "" + Witch.Instance.PotionPrice;
  }

  void Enter(int index){
    FormatTitle(index);
  }

  void FormatTitle(int index){
    titleTransform.GetComponent<Text>().text = "Merchant";
  }

  void LockItems(Token token) {
    if(GameManager.instance.MainHero == null || !GameManager.instance.MainHero.GetType().IsCompatibleWith(token.GetType())) return;
        
    Hero hero = (Hero)token;
    Component[] btns = transform.Find("MerchantUI/Items/").GetComponentsInChildren(typeof(Button));
    
    if(hero.heroInventory.numOfGold < 2 || !typeof(MerchantCell).IsCompatibleWith(hero.Cell.GetType())) {
      for(int i = 0; i < btns.Length; i++) {
        Buttons.Lock((Button)btns[i]);
      }
    } else {
      for(int i = 0; i < btns.Length; i++) {
        Buttons.Unlock((Button)btns[i]);
      }
    }

    Button potionBtn = transform.Find("MerchantUI/Potion/Button").GetComponent<Button>();
    if(hero.heroInventory.numOfGold < Witch.Instance.PotionPrice || (Witch.Instance.Cell != null && hero.Cell.Index != Witch.Instance.Cell.Index)) {
      Buttons.Lock(potionBtn);
    } else {
      Buttons.Unlock(potionBtn);
    }
  }
}