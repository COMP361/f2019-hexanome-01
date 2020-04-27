using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHero : Singleton<InventoryUIHero>
{
  public Transform smallToken;
  public Transform bigToken;
  public Transform  helm;
  public Transform gold;
  protected InventorySpotHero[] smallSpots;

  protected InventorySpotHero bigSpot;
  protected InventorySpotHero helmSpot;
  protected InventorySpotHero goldSpot;
  protected Transform goldText;

  void Awake()
  {
    smallSpots = smallToken.GetComponentsInChildren<InventorySpotHero>();
    bigSpot = bigToken.GetComponentInChildren<InventorySpotHero>();
    helmSpot = helm.GetComponentInChildren<InventorySpotHero>();
    goldSpot = gold.GetComponentInChildren<InventorySpotHero>();

    goldText = transform.FindDeepChild("GoldText");
    goldText.gameObject.SetActive(false);
  }


  protected virtual void OnEnable() {
    EventManager.InventoryUIHeroUpdate += UpdateUI;
    EventManager.InventoryUIHeroPeak += UpdateUI;
    EventManager.InitHeroInv += UpdateUI;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUIHeroUpdate -= UpdateUI;
    EventManager.InventoryUIHeroPeak -= UpdateUI;
    EventManager.InitHeroInv -= UpdateUI;
  }

  void UpdateUI(HeroInventory heroInv){
    //updating smallSpots

    Hero hero = GameManager.instance.findHero(heroInv.parentHero);
    for(int i = 0; i < smallSpots.Length; i++){
      if(i < heroInv.smallTokens.Count){
        smallSpots[i].AddItem((SmallToken)heroInv.smallTokens[i]);
      } else{
        smallSpots[i].ClearSpot();
      }
    }

    if(heroInv.bigToken != null){
      bigSpot.AddItem(heroInv.bigToken);
    }
    else{
      bigSpot.ClearSpot();
    }

    if(heroInv.helm != null){
      helmSpot.AddItem(heroInv.helm);
    }
    else{
      helmSpot.ClearSpot();
    }

    if(heroInv.numOfGold > 0){
      goldSpot.AddItem((Token) heroInv.golds[0]);
      goldText.GetComponent<Text>().text = "X" + heroInv.numOfGold;
      goldText.gameObject.SetActive(true);
    } else{
      goldText.gameObject.SetActive(false);
      goldSpot.ClearSpot();
    }
  }
}