using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoriesUITrade : MonoBehaviour
{
  Hero hero1;
  Hero hero2;

  protected Token[] hero1Items;
  protected Token[] hero2Items;

  protected Token[] hero1ItemsToTrade;
  protected Token[] hero2ItemsToTrade;

  public Transform smallTokenHero1;
  public Transform helmHero1;
  public Transform goldHero1;

  public Transform smallTokenHero2;
  public Transform helmHero2;
  public Transform goldHero2;

  protected InventorySpotTrade[] smallSpotsHero1;
  protected InventorySpotTrade helmSpotHero1;
  protected InventorySpotTrade goldSpotHero1;
  protected Transform goldTextHero1;

  protected InventorySpotTrade[] smallSpotsHero2;
  protected InventorySpotTrade helmSpotHero2;
  protected InventorySpotTrade goldSpotHero2;
  protected Transform goldTextHero2;

  void Awake()
  {
    hero1Items = new Token[25];
    hero2Items = new Token[25];
    hero1ItemsToTrade = new Token[25];
    hero2ItemsToTrade = new Token[25];

    smallSpotsHero1 = smallTokenHero1.GetComponentsInChildren<InventorySpotTrade>();
    helmSpotHero1 = helmHero1.GetComponentInChildren<InventorySpotTrade>();
    goldSpotHero1 = goldHero1.GetComponentInChildren<InventorySpotTrade>();

    goldTextHero1 = goldHero1.FindDeepChild("GoldText");
    goldTextHero1.GetComponent<Text>().text =  "";
    goldTextHero1.gameObject.SetActive(true);

    smallSpotsHero2 = smallTokenHero2.GetComponentsInChildren<InventorySpotTrade>();
    helmSpotHero2 = helmHero2.GetComponentInChildren<InventorySpotTrade>();
    goldSpotHero2 = goldHero2.GetComponentInChildren<InventorySpotTrade>();

    goldTextHero2 = goldHero2.FindDeepChild("GoldText");
    goldTextHero2.GetComponent<Text>().text =  "";
    goldTextHero2.gameObject.SetActive(true);
  }


  void OnEnable() {
    EventManager.InventoriesTrade += init;
  }

  void OnDisable() {
    EventManager.InventoriesTrade -= init;
  }

  void init(Hero hero1, Hero hero2){
    this.hero1 = hero1;
    this.hero2 = hero2;
    setLists();
    setPanels();
  }

  void setLists(){
    //setting hero1Items1 list
    for(int i = 0; i < hero1.heroInventory.smallTokens.Count; i++){
      hero1Items[i] = (Token)hero1.heroInventory.smallTokens[i];
    }

    hero1Items[3] = hero1.heroInventory.helm;

    for(int i = 0; i < hero1.heroInventory.golds.Count; i++){
      hero1Items[i+4] = (Token) hero1.heroInventory.golds[i];
    }
    //setting hero1Items1 list
    for(int i = 0; i < hero1.heroInventory.smallTokens.Count; i++){
      hero2Items[i] = (Token)hero2.heroInventory.smallTokens[i];
    }
    hero2Items[3] = hero2.heroInventory.helm;

    for(int i = 0; i < hero2.heroInventory.golds.Count; i++){
      hero2Items[i+4] = (Token) hero2.heroInventory.golds[i];
    }
  }

  void setPanels(){
    //smallToken
    for(int i = 0; i < 3; i++){
      if(hero1Items[i]!= null){
        smallSpotsHero1[i].AddItem(hero1Items[i]);
      }
      else{
        smallSpotsHero1[i].ClearSpot();
      }
    }
    //helm
    if(hero1Items[3] != null){
      helmSpotHero1.AddItem(hero1Items[3]);
    }
    else{
      helmSpotHero1.ClearSpot();
    }

    //golds
    if(hero1Items[4] != null){
      goldSpotHero1.AddItem(hero1Items[4]);
      int count = 0;

    for (int i = 4; i < hero1Items.Length; i++){
        if(hero1Items[i] != null){
          count++;
        }
    }


    goldTextHero1.GetComponent<Text>().text =  "X" + count;
    }
    else{
      goldSpotHero1.ClearSpot();
    }

    for(int i = 0; i < 3; i++){
      if(hero2Items[i]!= null){
        smallSpotsHero2[i].AddItem(hero2Items[i]);
      }
      else{
        smallSpotsHero2[i].ClearSpot();
      }
    }
    //helm
    if(hero2Items[3]!= null){
      helmSpotHero2.AddItem(hero2Items[3]);
    }
    else{
      helmSpotHero2.ClearSpot();
    }

    //golds
    if(hero2Items[4] != null){
      goldSpotHero2.AddItem(hero2Items[4]);
      int count = 0;
      for (int i = 4; i < hero2Items.Length; i++){
        if(hero2Items[i] != null){
          count++;
        }
      }
      goldTextHero2.GetComponent<Text>().text =  "X" + count;
    }
    else{
      goldSpotHero2.ClearSpot();
    }

  }

  void UpdateUI(){

  }
}
