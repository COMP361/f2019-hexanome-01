using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InventoriesUITrade : MonoBehaviour
{
  Hero hero1;
  Hero hero2;
  bool isUsingFalcon;


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

  public Transform smallTokenToTrade1;
  public Transform helmToTrade1;
  public Transform goldToTrade1;

  public Transform smallTokenToTrade2;
  public Transform helmToTrade2;
  public Transform goldToTrade2;

  protected InventorySpotTrade[] smallSpotsHero1;
  protected InventorySpotTrade helmSpotHero1;
  protected InventorySpotTrade goldSpotHero1;
  protected Transform goldTextHero1;

  protected InventorySpotTrade[] smallSpotsHero2;
  protected InventorySpotTrade helmSpotHero2;
  protected InventorySpotTrade goldSpotHero2;
  protected Transform goldTextHero2;

  protected InventorySpotTrade[] smallSpotsToTrade1;
  protected InventorySpotTrade helmSpotToTrade1;
  protected InventorySpotTrade goldSpotToTrade1;
  protected Transform goldTextToTrade1;

  protected InventorySpotTrade[] smallSpotsToTrade2;
  protected InventorySpotTrade helmSpotToTrade2;
  protected InventorySpotTrade goldSpotToTrade2;
  protected Transform goldTextToTrade2;

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

    smallSpotsToTrade1 = smallTokenToTrade1.GetComponentsInChildren<InventorySpotTrade>();
    helmSpotToTrade1 = helmToTrade1.GetComponentInChildren<InventorySpotTrade>();
    goldSpotToTrade1 = goldToTrade1.GetComponentInChildren<InventorySpotTrade>();

    goldTextToTrade1 = goldToTrade1.FindDeepChild("GoldText");
    goldTextToTrade1.GetComponent<Text>().text =  "";
    goldTextToTrade1.gameObject.SetActive(true);

    smallSpotsToTrade2 = smallTokenToTrade2.GetComponentsInChildren<InventorySpotTrade>();
    helmSpotToTrade2 = helmToTrade2.GetComponentInChildren<InventorySpotTrade>();
    goldSpotToTrade2 = goldToTrade2.GetComponentInChildren<InventorySpotTrade>();

    goldTextToTrade2 = goldToTrade2.FindDeepChild("GoldText");
    goldTextToTrade2.GetComponent<Text>().text =  "";
    goldTextToTrade2.gameObject.SetActive(true);


  }


  void OnEnable() {
    EventManager.InventoriesTrade += init;
    EventManager.TradeChange += UpdateUI;
    EventManager.QuitTrade += Terminate;
    EventManager.TradeVerify += Verify;
  }

  void OnDisable() {
    EventManager.InventoriesTrade -= init;
    EventManager.TradeChange -= UpdateUI;
    EventManager.QuitTrade -= Terminate;
    EventManager.TradeVerify -= Verify;
  }

  void init(Hero hero1, Hero hero2, bool isFalcon){
    isUsingFalcon = isFalcon;
    this.hero1 = hero1;
    this.hero2 = hero2;
    setLists();
    setPanels();
  }

  void Terminate(){
    this.hero1 = null;
    this.hero2 = null;

    hero1Items = new Token[25];
    hero2Items = new Token[25];
    hero1ItemsToTrade = new Token[25];
    hero2ItemsToTrade = new Token[25];

    setPanels();

  }

  void Verify(){
    // Either both toTradeList Empty --> Trigger error
    // Either trade can not happen has it would create illegal inventories --> Trigger error
    // Either trade is valid, --> Procceed to trade, close
    if(ArrayIsAllNull(hero1ItemsToTrade) && ArrayIsAllNull(hero2ItemsToTrade)){
      EventManager.TriggerError(4);
      return;
    }
    else if(MultiplayerFightPlayer.IsHeroFighting(hero2)){
      EventManager.TriggerError(6);
    }
    else if(IsTradeGood()){
      if(isUsingFalcon){
      Falcon toChange = (Falcon)hero1.heroInventory.bigToken;
      toChange.TurnFalcon();
      }
      ProceedTrade();
      EventManager.TriggerEndTrade();
      return;
    }
    else{
      EventManager.TriggerError(5);
      return;
    }
    EventManager.TriggerEndTrade();
  }

  bool IsTradeGood(){
    List<Token> verifyHero1 = new List<Token>();
    List<Token> verifyHero2 = new List<Token>();

    for(int i = 0; i < hero1.heroInventory.AllTokens.Count; i++){
      verifyHero1.Add((Token)hero1.heroInventory.AllTokens[i]);
    }

    for(int i = 0; i < hero2.heroInventory.AllTokens.Count; i++){
      verifyHero2.Add((Token)hero2.heroInventory.AllTokens[i]);
    }

    for(int i = 0; i < hero1ItemsToTrade.Length; i++ ){
      if(hero1ItemsToTrade[i] != null){
        verifyHero1.Remove(hero1ItemsToTrade[i]);
        verifyHero2.Add(hero1ItemsToTrade[i]);
      }
    }

    for(int i = 0; i < hero2ItemsToTrade.Length; i++ ){
      if(hero2ItemsToTrade[i] != null){
        verifyHero2.Remove(hero2ItemsToTrade[i]);
        verifyHero1.Add(hero2ItemsToTrade[i]);
      }
    }

    return(IsListValid(verifyHero1) && IsListValid(verifyHero2));

  }

  void ProceedTrade(){
    List<Token> verifyHero1 = new List<Token>();
    List<Token> verifyHero2 = new List<Token>();

    // Add all Items of heroInventory
    for(int i = 0; i < hero1.heroInventory.AllTokens.Count; i++){
      verifyHero1.Add((Token)hero1.heroInventory.AllTokens[i]);
    }

    // Add all Items of heroInventory
    for(int i = 0; i < hero2.heroInventory.AllTokens.Count; i++){
      verifyHero2.Add((Token)hero2.heroInventory.AllTokens[i]);
    }

    hero1.heroInventory.Clear();
    hero2.heroInventory.Clear();


    // Remove Items that we want to give away, Add to hero2




    for(int i = 0; i < hero1ItemsToTrade.Length; i++ ){
      if(hero1ItemsToTrade[i] != null){
        verifyHero1.Remove(hero1ItemsToTrade[i]);
        verifyHero2.Add(hero1ItemsToTrade[i]);
      }
    }

    // Remove Items that we want to give away, Add to hero1
    for(int i = 0; i < hero2ItemsToTrade.Length; i++ ){
      if(hero2ItemsToTrade[i] != null){
        verifyHero2.Remove(hero2ItemsToTrade[i]);
        verifyHero1.Add(hero2ItemsToTrade[i]);
      }
    }

    // add all verify Items to hero inventory
    for(int i = 0; i < verifyHero1.Count; i++){
      hero1.heroInventory.AddItem(verifyHero1[i]);
    }
    // add all verify Items to hero inventory
    for(int i = 0; i < verifyHero2.Count; i++){
      hero2.heroInventory.AddItem(verifyHero2[i]);
    }

  }

  bool IsListValid(List<Token> a){
    int helmCount = 0;
    int smallTokenCount = 0;
    foreach(Token b in a){
      if(b is Helm){
        helmCount++;
      }
      else if(b is SmallToken){
        smallTokenCount++;
      }
    }
    if(helmCount > 1 || smallTokenCount > 3){
      return false;
    }
    return true;
  }

  void setLists(){
    //setting hero1Items list
    for(int i = 0; i < hero1.heroInventory.smallTokens.Count; i++){
      hero1Items[i] = (Token)hero1.heroInventory.smallTokens[i];
    }

    hero1Items[3] = hero1.heroInventory.helm;

    for(int i = 0; i < hero1.heroInventory.golds.Count; i++){
      hero1Items[i+4] = (Token) hero1.heroInventory.golds[i];
    }
    //setting hero2Items1list
    for(int i = 0; i < hero2.heroInventory.smallTokens.Count; i++){
      hero2Items[i] = (Token)hero2.heroInventory.smallTokens[i];
    }
    hero2Items[3] = hero2.heroInventory.helm;

    for(int i = 0; i < hero2.heroInventory.golds.Count; i++){
      hero2Items[i+4] = (Token) hero2.heroInventory.golds[i];
    }
  }

  void setPanels(){

    // Hero Items 2
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
      goldTextHero1.GetComponent<Text>().text =  "";
    }

    // Hero Items 2

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
      goldTextHero2.GetComponent<Text>().text =  "";
    }

    // To Trade 1
    for(int i = 0; i < 3; i++){
      if(hero1ItemsToTrade[i]!= null){
        smallSpotsToTrade1[i].AddItem(hero1ItemsToTrade[i]);
      }
      else{
        smallSpotsToTrade1[i].ClearSpot();
      }
    }
    //helm
    if(hero1ItemsToTrade[3]!= null){
      helmSpotToTrade1.AddItem(hero1ItemsToTrade[3]);
    }
    else{
      helmSpotToTrade1.ClearSpot();
    }

    //golds
    if(hero1ItemsToTrade[4] != null){
      goldSpotToTrade1.AddItem(hero1ItemsToTrade[4]);
      int count = 0;
      for (int i = 4; i < hero1ItemsToTrade.Length; i++){
        if(hero1ItemsToTrade[i] != null){
          count++;
        }
      }
      goldTextToTrade1.GetComponent<Text>().text =  "X" + count;
    }
    else{
      goldSpotToTrade1.ClearSpot();
      goldTextToTrade1.GetComponent<Text>().text =  "";
    }

    //To Trade2
    for(int i = 0; i < 3; i++){
      if(hero2ItemsToTrade[i]!= null){
        smallSpotsToTrade2[i].AddItem(hero2ItemsToTrade[i]);
      }
      else{
        smallSpotsToTrade2[i].ClearSpot();
      }
    }
    //helm
    if(hero2ItemsToTrade[3]!= null){
      helmSpotToTrade2.AddItem(hero2ItemsToTrade[3]);
    }
    else{
      helmSpotToTrade2.ClearSpot();
    }

    //golds
    if(hero2ItemsToTrade[4] != null){
      goldSpotToTrade2.AddItem(hero2ItemsToTrade[4]);
      int count = 0;
      for (int i = 4; i < hero2ItemsToTrade.Length; i++){
        if(hero2ItemsToTrade[i] != null){
          count++;
        }
      }
      goldTextToTrade2.GetComponent<Text>().text =  "X" + count;
    }
    else{
      goldSpotToTrade2.ClearSpot();
      goldTextToTrade2.GetComponent<Text>().text =  "";
    }

  }

  void UpdateUI(Token item){
    if(ArrayContainsToken(hero1Items, item)){
      int index = ArrayIndexOf(hero1Items, item);
      if(index < 4){
        hero1ItemsToTrade[index] = item;
        hero1Items[index] = null;
      }
      // if is gold deal with gold structure
      else{
        //Add Gold to Item to trade (at the end of the gold sequence)
        for(int i = 4; i < hero1ItemsToTrade.Length; i++){
          if(hero1ItemsToTrade[i] == null){
            hero1ItemsToTrade[i] = item;
            break;
          }
        }
        // remove gold from index 4 and move other golds up the sequence
        hero1Items[index] = null;
        for(int i = 4; i < hero1Items.Length; i++){
          if(hero1Items[i + 1] != null){
            hero1Items[i] = hero1Items[i+1];
            hero1Items[i+1] = null;
          }
          else{
            break;
          }
        }
      }
    }
    else if(ArrayContainsToken(hero1ItemsToTrade, item)){
      int index = ArrayIndexOf(hero1ItemsToTrade, item);
      if(index < 4){
        hero1Items[index] = item;
        hero1ItemsToTrade[index] = null;
      }
      // if is gold deal with gold structure
      else{
        //Add Gold to Item to trade (at the end of the gold sequence)
        for(int i = 4; i < hero1Items.Length; i++){
          if(hero1Items[i] == null){
            hero1Items[i] = item;
            break;
          }
        }
        // remove gold from index 4 and move other golds up the sequence
        hero1ItemsToTrade[index] = null;
        for(int i = 4; i < hero1ItemsToTrade.Length; i++){
          if(hero1ItemsToTrade[i + 1] != null){
            hero1ItemsToTrade[i] = hero1ItemsToTrade[i+1];
            hero1ItemsToTrade[i+1] = null;
          }
          else{
            break;
          }
        }
      }
    }
    else if(ArrayContainsToken(hero2Items, item)){
      int index = ArrayIndexOf(hero2Items, item);
      if(index < 4){
        hero2ItemsToTrade[index] = item;
        hero2Items[index] = null;
      }
      // if is gold deal with gold structure
      else{
        //Add Gold to Item to trade (at the end of the gold sequence)
        for(int i = 4; i < hero2ItemsToTrade.Length; i++){
          if(hero2ItemsToTrade[i] == null){
            hero2ItemsToTrade[i] = item;
            break;
          }
        }
        // remove gold from index 4 and move other golds up the sequence
        hero2Items[index] = null;
        for(int i = 4; i < hero2Items.Length; i++){
          if(hero2Items[i + 1] != null){
            hero2Items[i] = hero2Items[i+1];
            hero2Items[i+1] = null;
          }
          else{
            break;
          }
        }
      }
    }
    else if(ArrayContainsToken(hero2ItemsToTrade, item)){
      int index = ArrayIndexOf(hero2ItemsToTrade, item);
      if(index < 4){
        hero2Items[index] = item;
        hero2ItemsToTrade[index] = null;
      }
      // if is gold deal with gold structure
      else{
        //Add Gold to Item to trade (at the end of the gold sequence)
        for(int i = 4; i < hero2Items.Length; i++){
          if(hero2Items[i] == null){
            hero2Items[i] = item;
            break;
          }
        }
        // remove gold from index 4 and move other golds up the sequence
        hero2ItemsToTrade[index] = null;
        for(int i = 4; i < hero2ItemsToTrade.Length; i++){
          if(hero2ItemsToTrade[i + 1] != null){
            hero2ItemsToTrade[i] = hero2ItemsToTrade[i+1];
            hero2ItemsToTrade[i+1] = null;
          }
          else{
            break;
          }
        }
      }
    }
    setPanels();




  }

  bool ArrayContainsToken(Token[] a, Token b){
    for(int i = 0; i< a.Length; i++){
      if(a[i] != null){
        if(a[i].GetComponent<PhotonView>().ViewID == b.GetComponent<PhotonView>().ViewID){
          return true;
        }
      }
    }
    return false;
  }

  int ArrayIndexOf(Token[] a, Token b){
    for(int i = 0; i< a.Length; i++){
      if(a[i] != null){
        if(a[i].GetComponent<PhotonView>().ViewID == b.GetComponent<PhotonView>().ViewID){
          return i;
        }
      }
    }
    return -1;
  }

  bool ArrayIsAllNull(Token[] a){
    for(int i = 0; i< a.Length; i++){
      if(a[i] != null){
        return false;
      }
    }
    return true;
  }
}
