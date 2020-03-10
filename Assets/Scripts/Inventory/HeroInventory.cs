using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroInventory : MonoBehaviour
{
    public List<Token> smallTokens { get; private set; }
    public List<Token> golds { get; private set; }
    public Token bigToken { get; private set; }
    public Token helm { get; private set; }



    public HeroState heroState;
    private int spaceSmall;
    public int numOfGold { get; private set; }


    void OnEnable() {
      EventManager.InventoryUICellEnter += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUICellEnter -= updateUI;
    }

    public HeroInventory(){
      smallTokens = new List<Token>();
      golds = new List<Token>();
      bigToken =null;
      helm = null;
      spaceSmall = 3;
      numOfGold = 0;

    }


#region AddTokens

    public bool AddSmallToken(Token token){

      if(smallTokens.Count >= spaceSmall){
        Debug.Log("Not enough room ");
        return false;
      }
       smallTokens.Add(token);
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
     }

    public void RemoveSmallToken(Token token){
      smallTokens.Remove(token);
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }


    public bool AddBigToken(Token token){
      if(bigToken != null){
        Debug.Log("Not enough room ");
        return false;
      }
       bigToken = token;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public void RemoveBigToken(Token token){
      bigToken = null;
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }

    public bool AddHelm(Token token){
      if(helm != null){
        Debug.Log("Not enough room ");
        return false;
      }
       helm = token;
       EventManager.TriggerInventoryUIHeroUpdate(this);
       return true;
    }

    public void RemoveHelm(Token token){
      helm = null;
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }

    // maybe have a void return type
    public bool AddGold(Token token){
      golds.Add(token);
      numOfGold++;
      if(GameManager.instance.MainHero.State.heroInventory == this){
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }
      return true;
    }

    public void RemoveGold(Token token){
      numOfGold--;
      golds.Remove(token);
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }

#endregion

    public void updateUI(CellInventory inventory, int index){}


}
