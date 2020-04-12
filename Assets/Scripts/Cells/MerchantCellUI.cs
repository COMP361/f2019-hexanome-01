using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class MerchantCellUI : Singleton<MerchantCellUI>
{
  List<int> cellsID = new List<int>(){ 71, 57, 18 };

  GameObject merchantPanel;
  GameObject buyPanel;
  Text panelTitle;
  Text panelDesc;
  Button buyBtn, cancelBtn;
  Component[] btns;
  string item;

  void OnEnable() {
    EventManager.CurrentPlayerUpdate += LockItems;
    EventManager.CellUpdate += LockItems;
    EventManager.MainHeroInit += LockItems;
  }

  void OnDisable() {
    EventManager.CurrentPlayerUpdate -= LockItems;
    EventManager.CellUpdate -= LockItems;
    EventManager.MainHeroInit -= LockItems;
  }

  void Start() {
    merchantPanel = transform.Find("MerchantUI").gameObject;
    buyPanel = transform.Find("BuyPanel").gameObject;
    panelTitle = buyPanel.transform.Find("Title").GetComponent<Text>();
    panelDesc = buyPanel.transform.Find("Description").GetComponent<Text>();

    btns = transform.Find("MerchantUI/Items/").GetComponentsInChildren(typeof(Button));
    buyBtn = buyPanel.transform.Find("Buy Button").GetComponent<Button>();

    cancelBtn = buyPanel.transform.Find("Cancel Button").GetComponent<Button>();
    cancelBtn.onClick.AddListener(delegate { HidePanel(); });

    transform.Find("MerchantUI/Potion/Button/Text").GetComponent<UnityEngine.UI.Text>().text = "" + Witch.Instance.PotionPrice;

    for(int i = 0; i < btns.Length; i++) {
      Button btn = (Button)btns[i];

      if(btn.transform.parent.name == "Strength") {
        btn.onClick.AddListener(() => {
          ShowPanel(Strength.itemName, Strength.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Strength.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Helm") {
        btn.onClick.AddListener(() => {
          ShowPanel(Helm.itemName, Helm.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Helm.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Telescope") {
        btn.onClick.AddListener(() => {
          ShowPanel(Telescope.itemName, Telescope.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Telescope.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Wineskin") {
        btn.onClick.AddListener(() => {
          ShowPanel(Wineskin.itemName, Wineskin.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Wineskin.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Shield") {
        btn.onClick.AddListener(() => {
          ShowPanel(Shield.itemName, Shield.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Shield.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Bow") {
        btn.onClick.AddListener(() => {
          ShowPanel(Bow.itemName, Bow.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Bow.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Falcon") {
        btn.onClick.AddListener(() => {
          ShowPanel(Falcon.itemName, Falcon.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Falcon.Buy(); HidePanel(); });
        });
      } else if(btn.transform.parent.name == "Potion") {
        btn.onClick.AddListener(() => {
          ShowPanel(Potion.itemName, Potion.desc);
          buyBtn.onClick.RemoveAllListeners() ;
          buyBtn.onClick.AddListener(() => { Potion.Buy(); HidePanel(); });
        });
      }
    }
  }

  void LockItems(Token token) {
    if(btns == null || GameManager.instance.MainHero == null || !GameManager.instance.MainHero.GetType().IsCompatibleWith(token.GetType())) return;

    Hero hero = (Hero)token;

    if(hero.heroInventory.numOfGold < 2 || !cellsID.Contains(hero.Cell.Index)) {
      for(int i = 0; i < btns.Length; i++) {
        Buttons.Lock((Button)btns[i]);
      }
    } else {
      for(int i = 0; i < btns.Length; i++) {
        Buttons.Unlock((Button)btns[i]);
      }
    }

    Button potionBtn = transform.Find("MerchantUI/Potion/Button").GetComponent<Button>();
    if(hero.heroInventory.numOfGold < Witch.Instance.PotionPrice || Witch.Instance.Cell == null || hero.Cell.Index != Witch.Instance.Cell.Index) {
      Buttons.Lock(potionBtn);
    } else {
      Buttons.Unlock(potionBtn);
    }
  }

  void HidePanel() {
    buyPanel.SetActive(false);
  }

  public void ShowPanel(string name, string desc) {
    panelTitle.text = name;
    panelDesc.text = desc;
    buyPanel.transform.Find("Title").GetComponent<Text>();
    buyPanel.SetActive(true);
  }
}
