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
    protected InventorySpotCell[] smallSpots;
    protected InventorySpotCell bigSpot;
    protected InventorySpotCell helmSpot;
    protected InventorySpotCell goldSpot;

    protected Transform goldText;




  void Start()
  {
    smallSpots = smallItemsParent.GetComponentsInChildren<InventorySpotCell>();
    bigSpot = bigParent.GetComponentInChildren<InventorySpotCell>();
    helmSpot = helmParent.GetComponentInChildren<InventorySpotCell>();
    goldSpot = goldParent.GetComponentInChildren<InventorySpotCell>();

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

/*
      if(Input.GetButtonDown("LockCellInventory")){
          isLocked = !isLocked;
          }
      if(Input.GetButtonDown("displayCellInv")){
          isText = !isText;
        }

        */
    }


    void UpdateUI(HeroInventory heroInv){
      Debug.Log("HEEEEEEEERE");
      //updating smallSpots
    for(int i = 0; i < smallSpots.Length; i++){
      if(i < heroInv.smallTokens.Count){
        smallSpots[i].AddItem(heroInv.smallTokens[i]);
      }
      else{
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
      goldSpot.AddItem(heroInv.golds[0]);
      goldText.GetComponent<Text>().text = "X" +heroInv.numOfGold;
      goldText.gameObject.SetActive(true);
    }
    else{
      goldText.gameObject.SetActive(false);
      goldSpot.ClearSpot();
    }
  }


}
