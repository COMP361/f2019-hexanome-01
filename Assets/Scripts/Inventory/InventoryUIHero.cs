using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHero : Singleton<InventoryUIHero>
{
    // Start is called before the first frame update

    public Transform  smallItemsParent;
    public Transform bigParent;
    public Transform  helmParent;
    public Transform goldParent;
    protected InventorySpotHero[] smallSpots;
    protected InventorySpotHero bigSpot;
    protected InventorySpotHero helmSpot;
    protected InventorySpotHero goldSpot;

    protected Transform goldText;




  void Start()
  {
    smallSpots = smallItemsParent.GetComponentsInChildren<InventorySpotHero>();
    bigSpot = bigParent.GetComponentInChildren<InventorySpotHero>();
    helmSpot = helmParent.GetComponentInChildren<InventorySpotHero>();
    goldSpot = goldParent.GetComponentInChildren<InventorySpotHero>();

    goldText = transform.FindDeepChild("GoldText");
    goldText.gameObject.SetActive(false);
  }


  protected virtual void OnEnable() {
    EventManager.InventoryUIHeroUpdate += UpdateUI;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUIHeroUpdate -= UpdateUI;
  }

  void Update(){

    }


  void UpdateUI(HeroInventory heroInv){
    //updating smallSpots
    for(int i = 0; i < smallSpots.Length; i++){
      if(i < heroInv.smallItems.Count){
        smallSpots[i].AddItem(heroInv.smallItems[i]);
      }
      else{
        smallSpots[i].ClearSpot();
      }
    }

    if(heroInv.bigItem != null){
      bigSpot.AddItem(heroInv.bigItem);
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
      goldSpot.AddItem(heroInv.golds[0]);
      goldText.GetComponent<Text>().text = "X" + heroInv.numOfGold;
      goldText.gameObject.SetActive(true);
    }
    else{
      goldText.gameObject.SetActive(false);
      goldSpot.ClearSpot();
    }
  }
}
