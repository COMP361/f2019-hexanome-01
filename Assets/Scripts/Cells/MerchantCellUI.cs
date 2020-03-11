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

  void Enter(int index){
    FormatTitle(index);
  }

  void FormatTitle(int index){
    titleTransform.GetComponent<Text>().text = "Merchant";
  }

  void LockItems(Token token) {
    if(!typeof(Hero).IsCompatibleWith(token.GetType())) return;

    Hero hero = (Hero)token;
    Button strengthBtn = transform.Find("MerchantUI/allItems/Strenght/Button").GetComponent<Button>();

    if(hero == GameManager.instance.MainHero) {
      if(hero.State.heroInventory.numOfGold < 2 || !hero.Cell.GetType().IsCompatibleWith(typeof(MerchantCell))) {
        Buttons.Lock(strengthBtn);
      } else {
        Buttons.Unlock(strengthBtn);
      }
    }
  }
}
