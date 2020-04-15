using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

public class TradePanelUI : MonoBehaviour
{
  GameObject FalconTradePanel;

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
  }

  void OnDisable() {

  }

  void Awake(){
    hero1 = null;
    hero2 = null;

    FalconTradePanel = transform.Find("FalconTrade").gameObject;

    heroOneTitle = FalconTradePanel.transform.Find("HeroTitle1").GetComponent<Text>();
    heroTwoTitle = FalconTradePanel.transform.Find("HeroTitle2").GetComponent<Text>();

    CancelBtn= FalconTradePanel.transform.Find("Cancel Button").GetComponent<Button>();
    CancelBtn.onClick.AddListener(delegate { HideFalconTrade(); });

    TradeBtn = FalconTradePanel.transform.Find("Trade Button").GetComponent<Button>();
    TradeBtn.onClick.AddListener(delegate { });

    PlayerBoardTrade1 = FalconTradePanel.transform.Find("PlayerBoardTrade1").gameObject;
    PlayerBoardTrade2 = FalconTradePanel.transform.Find("PlayerBoardTrade2").gameObject;

    FalconTradePanel.SetActive(false);
  }

  public void ShowFalconTrade(Hero hero1, Hero hero2){
    this.hero1 = hero1;
    this.hero2 = hero2;
    heroOneTitle.text = this.hero1.TokenName;
    heroTwoTitle.text = this.hero2.TokenName;

    PlayerBoardTrade1.GetComponent<PlayerBoardTrade>().updateCompleteBoard(hero1);
    PlayerBoardTrade2.GetComponent<PlayerBoardTrade>().updateCompleteBoard(hero2);
//    c = t.Item2.neighbours[j].GetComponent<Cell>();



    FalconTradePanel.SetActive(true);
  }

  public void HideFalconTrade(){

  }
}
