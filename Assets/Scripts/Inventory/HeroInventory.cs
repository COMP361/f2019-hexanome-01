using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HeroInventory : MonoBehaviour
{
    public List<Item> smallItems { get; private set; }
    public List<GoldCoin> golds { get; private set; }
    public Item bigItem { get; private set; }
    public Helm helm { get; private set; }
    public List<Item> AllItems { get; private set; }

    public string parentHero;
    public HeroState heroState;
    private int spaceSmall;
    public int numOfGold { get; private set; }
    public PhotonView photonView;


    void OnEnable() {
      EventManager.InventoryUICellEnter += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUICellEnter -= updateUI;
    }

    public HeroInventory(string parentHero){
      AllItems = new List<Item>();
      smallItems = new List<Item>();
      golds = new List<GoldCoin>();
      bigItem =null;
      helm = null;
      spaceSmall = 3;
      numOfGold = 0;
      this.parentHero = parentHero;
    }


#region AddTokens

    public bool AddSmallItem(Item item){

      if(smallItems.Count >= spaceSmall){
        Debug.Log("Not enough room ");
        return false;
      }
       smallItems.Add(item);
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public bool AddBigItem(Item item){
      if(bigItem != null){
        Debug.Log("Not enough room ");
        return false;
      }
       bigItem = item;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public bool AddHelm(Item item){
      if(helm != null){
        Debug.Log("Not enough room ");
        return false;
      }
       helm = (Helm)item;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    // maybe have a void return type
    public bool AddGold(Item item){
      golds.Add((GoldCoin)item);
      AllItems.Add(item);
      numOfGold++;
      if(GameManager.instance.MainHero.State.heroInventory == this){
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }
      return true;
    }

    public void RemoveToken(Item item){
        Type listType;
        listType = smallItems.GetListType();
        if (listType.IsCompatibleWith(item.GetType())) {
            smallItems.Remove(item);
            return;
          }/*
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
        */
        listType = golds.GetListType();
        if (listType.IsCompatibleWith(item.GetType())) {
          golds.Remove((GoldCoin)item);
          return;
        }
        AllItems.Remove(item);
        EventManager.TriggerInventoryUIHeroUpdate(this);
    }

    public void RemoveGold(int amtToRemove)
    {
        if (golds.Count >= amtToRemove)
        {
            numOfGold-= amtToRemove;
            while(amtToRemove != 0)
            {
                amtToRemove--;
                if(amtToRemove < golds.Count) {
                  AllItems.Remove(golds[amtToRemove]);
                  golds.RemoveAt(amtToRemove);
                }
            }

        }
        EventManager.TriggerInventoryUIHeroUpdate(this);
    }

    #endregion

    public void updateUI(CellInventory inventory, int index){}


}
