using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

public class TradePanelUI : MonoBehaviour
{
  GameObject FalconTradePanel;
  GameObject BlockPanel;
  Button TradeBtn, CancelBtn;
  public PhotonView photonView;
  Hero hero1;
  Hero hero2;
  Text heroOneTitle;
  Text heroTwoTitle;
  GameObject PlayerBoardTrade1;
  GameObject PlayerBoardTrade2;

  void OnEnable() {
    EventManager.FalconTrade += ShowFalconTrade;
    EventManager.EndTrade += HideFalconTrade;
  }

  void OnDisable() {
    EventManager.FalconTrade -= ShowFalconTrade;
    EventManager.EndTrade -= HideFalconTrade;
  }

  void Awake(){
    hero1 = null;
    hero2 = null;

    FalconTradePanel = transform.Find("FalconTrade").gameObject;
    BlockPanel = transform.Find("BlockPanel").gameObject;

    heroOneTitle = FalconTradePanel.transform.Find("HeroTitle1").GetComponent<Text>();
    heroTwoTitle = FalconTradePanel.transform.Find("HeroTitle2").GetComponent<Text>();

    CancelBtn= FalconTradePanel.transform.Find("Cancel Button").GetComponent<Button>();
    CancelBtn.onClick.AddListener(delegate { HideFalconTrade(); });

    TradeBtn = FalconTradePanel.transform.Find("Trade Button").GetComponent<Button>();
    TradeBtn.onClick.AddListener(delegate { Trade(); });

    PlayerBoardTrade1 = FalconTradePanel.transform.Find("PlayerBoardTrade1").gameObject;
    PlayerBoardTrade2 = FalconTradePanel.transform.Find("PlayerBoardTrade2").gameObject;

    FalconTradePanel.SetActive(false);
    BlockPanel.SetActive(false);
  }

  public void ShowFalconTrade(Hero hero1, Hero hero2, bool isFalcon){
    this.hero1 = hero1;
    this.hero2 = hero2;
    heroOneTitle.text = this.hero1.TokenName;
    heroTwoTitle.text = this.hero2.TokenName;

    PlayerBoardTrade1.GetComponent<PlayerBoardTrade>().updateCompleteBoard(hero1);
    PlayerBoardTrade2.GetComponent<PlayerBoardTrade>().updateCompleteBoard(hero2);

    FalconTradePanel.SetActive(true);
    ActivateBlockPanel(hero2.TokenName);

    EventManager.TriggerInventoriesTrade(this.hero1, this.hero2, isFalcon);
  }

  public void HideFalconTrade(){
    DeactivateBlockPanel(hero2.TokenName);
    this.hero1 = null;
    this.hero2 = null;

    heroOneTitle.text = "";
    heroTwoTitle.text = "";

    EventManager.TriggerQuitTrade();
    FalconTradePanel.SetActive(false);
  }

  void ActivateBlockPanel(string heroName){
    photonView.RPC("ActivateBlockPanelRPC", RpcTarget.AllViaServer, new object[] {heroName});
  }

  [PunRPC]
  void ActivateBlockPanelRPC(string heroName){
    if(GameManager.instance.MainHero.TokenName.Equals(heroName)){
      BlockPanel.SetActive(true);
    }
  }

  void DeactivateBlockPanel(string heroName){
    photonView.RPC("DeactivateBlockPanelRPC", RpcTarget.AllViaServer, new object[] {heroName});
  }

  [PunRPC]
  void DeactivateBlockPanelRPC(string heroName){
    if(GameManager.instance.MainHero.TokenName.Equals(heroName)){
      BlockPanel.SetActive(false);
    }
  }

  public void Trade(){
    EventManager.TriggerTradeVerify();
  }
}
