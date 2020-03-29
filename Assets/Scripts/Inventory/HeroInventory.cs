using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using  System.Collections.Specialized;
public class HeroInventory
{
  //  public List<SmallToken> smallTokens { get; private set; }
  //  public List<GoldCoin> golds { get; private set; }
    public BigToken bigToken { get; private set; }
    public Helm helm { get; private set; }
 //   public List<Token> AllTokens { get; private set; }

    public string parentHero;
    private int spaceSmall;
    public int numOfGold { get; private set; }
    public PhotonView photonView;

    public OrderedDictionary golds2 { get; private set; }
    public OrderedDictionary AllTokens2 { get; private set; }
    public OrderedDictionary smallTokens2 { get; private set; }

    void OnEnable() {
      EventManager.InventoryUICellEnter += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUICellEnter -= updateUI;
    }

    public HeroInventory(string parentHero){
    //  AllTokens = new List<Token>();
    //  smallTokens = new List<SmallToken>();
    //  golds = new List<GoldCoin>();
      bigToken =null;
      helm = null;
      spaceSmall = 3;
      numOfGold = 0;
      this.parentHero = parentHero;
      golds2 = new OrderedDictionary();
      AllTokens2 = new OrderedDictionary();
      smallTokens2 = new OrderedDictionary();
    }


#region AddTokens



    public void AddGold(GoldCoin gold){
      int viewID = gold.GetComponent<PhotonView>().ViewID;
      GameManager.instance.AddGoldHeroRPC(viewID, parentHero);
    }

    public void addGold2(GoldCoin gold){
        string id = convertToKey(gold.GetComponent<PhotonView>().ViewID);
        golds2.Add(id, (GoldCoin)gold);
        AllTokens2.Add(id,(GoldCoin)gold);
        numOfGold++;
        if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
          EventManager.TriggerInventoryUIHeroUpdate(this);
        }
    }

    public void RemoveGold(int amtToRemove){
      if(numOfGold >= amtToRemove){
        for(int i = 0; i< amtToRemove; i++){
          int viewID = ((GoldCoin)golds2[i]).GetComponent<PhotonView>().ViewID;
          GameManager.instance.RemoveGoldHeroRPC(viewID, parentHero);
        }
      }
      else{
        // error not enough gold
      }
    }

    public void RemoveGold2(int viewID){
        numOfGold--;
        AllTokens2.Remove(convertToKey(viewID));
        golds2.Remove(convertToKey(viewID));
        if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
          EventManager.TriggerInventoryUIHeroUpdate(this);
        }
    }


    //what to do with errors
    public bool AddSmallToken(SmallToken smallToken){
      if(smallTokens2.Count >= spaceSmall){
        Debug.Log("Not enough room ");
        return false;
      }
      else{
        int viewID = smallToken.GetComponent<PhotonView>().ViewID;
        GameManager.instance.AddSmallTokenRPC(viewID, parentHero);
        return true;
      }
    }

    public void AddSmallToken2(SmallToken smallToken){
      string id = convertToKey(smallToken.GetComponent<PhotonView>().ViewID);
      smallTokens2.Add(id, (SmallToken)smallToken);
      AllTokens2.Add(id,(SmallToken)smallToken);
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }

    public void RemoveSmallToken(SmallToken smallToken){
          int viewID = smallToken.GetComponent<PhotonView>().ViewID;
          GameManager.instance.RemoveSmallTokenRPC(viewID, parentHero);
    }

    public void RemoveSmallToken2(int viewID){
      AllTokens2.Remove(convertToKey(viewID));
      smallTokens2.Remove(convertToKey(viewID));
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }

    public bool AddBigToken(BigToken item){
      if(bigToken == null){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.AddBigTokenRPC(viewID, parentHero);
        return true;
      }
      else{
        //Error
        return false;
      }
    }

    public void AddBigToken2(BigToken item){
      string id = convertToKey(item.GetComponent<PhotonView>().ViewID);
      bigToken = item;
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }

    public void RemoveBigToken(BigToken item){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.RemoveBigTokenRPC(viewID, parentHero);
    }

    public void RemoveBigToken2(int viewID){
      if(bigToken.GetComponent<PhotonView>().ViewID == viewID){
        bigToken = null;
      }
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }

    public bool AddHelm(Helm item){
      if(helm == null){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.AddHelmRPC(viewID, parentHero);
        return true;
      }
      else{
        //Error
        return false;
      }
    }

    public void AddHelm2(Helm item){
      string id = convertToKey(item.GetComponent<PhotonView>().ViewID);
      helm = item;
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }

    public void RemoveHelm(Helm item){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.RemoveHelmRPC(viewID, parentHero);
    }

    public void RemoveHelm2(int viewID){
      if(helm.GetComponent<PhotonView>().ViewID == viewID){
        helm = null;
      }
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
    }


    public void updateUI(CellInventory inventory, int index){}

    public string convertToKey(int a){
      string toReturn = "" + a;
      return toReturn;
    }

    public void AddItem(Token item){

      if(item == null){
        return;
      }
      if (item is GoldCoin){
        AddGold((GoldCoin) item);
      }
      if (item is Helm){
        AddHelm((Helm) item);
      }
      else if (item.GetType().IsSubclassOf(typeof(SmallToken))){
        AddSmallToken((SmallToken) item);
      }
      else if (item.GetType().IsSubclassOf(typeof(BigToken))){
        AddBigToken((BigToken) item);
      }
    }

  #endregion

}

/*
    public bool AddSmallToken(Token token){

      if(smallTokens.Count >= spaceSmall){
        Debug.Log("Not enough room ");
        return false;
      }
       smallTokens.Add((SmallToken)token);
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public bool AddBigToken(Token token){
      if(bigToken != null){
        Debug.Log("Not enough room ");
        return false;
      }
       bigToken = (BigToken)token;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public bool AddHelm(Token token){
      if(helm != null){
        Debug.Log("Not enough room ");
        return false;
      }
       helm = (Helm)token;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    // maybe have a void return type
    /*
    public bool AddGold(Token token){
      golds.Add((GoldCoin)token);
      AllTokens.Add(token);
      numOfGold++;
      if(GameManager.instance.MainHero.heroInventory == this){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
      return true;
    } */



    /*
        public void RemoveToken(Token token){
            Type listType;
            listType = smallTokens.GetListType();
            if (listType.IsCompatibleWith(token.GetType())) {
                smallTokens.Remove((SmallToken)token);
                return;
              }
            listType = BigToken.GetType();
            if (listType.IsCompatibleWith(token.GetType())) {
              if (bigToken == token){
                bigToken = null;
              }
              return;
            }

            listType = helm.GetType();
            if (listType.IsCompatibleWith(token.GetType())) {
              if (helm == token){
                helm = null;
              }
              return;
            }

            listType = golds.GetListType();
            if (listType.IsCompatibleWith(token.GetType())) {
              golds.Remove((GoldCoin)token);
              return;
            }
            AllTokens.Remove(token);
            EventManager.TriggerInventoryUIHeroUpdate(this);
        }

    /*
        public void RemoveGold(int amtToRemove)
        {
            if (golds.Count >= amtToRemove)
            {
                numOfGold-= amtToRemove;
                while(amtToRemove != 0)
                {
                    amtToRemove--;
                    if(amtToRemove < golds.Count) {
                      AllTokens.Remove(golds[amtToRemove]);
                      golds.RemoveAt(amtToRemove);
                    }
                }

            }
            EventManager.TriggerInventoryUIHeroUpdate(this);
        }
    */
