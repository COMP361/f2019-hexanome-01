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
    if(GameManager.instance.heroes.Count == 3) {
      transform.Find("MerchantUI/Potion/Button/Text").GetComponent<UnityEngine.UI.Text>().text = "" + 4;
    } else {
      transform.Find("MerchantUI/Potion/Button/Text").GetComponent<UnityEngine.UI.Text>().text = "" + 5;
    }
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
    Button strengthBtn = transform.Find("MerchantUI/Items/Strenght/Button").GetComponent<Button>();

    if(hero.heroInventory.numOfGold < 2 || !typeof(MerchantCell).IsCompatibleWith(hero.Cell.GetType())) {
      Buttons.Lock(strengthBtn);
    } else {
      Buttons.Unlock(strengthBtn);
    }
  }
}