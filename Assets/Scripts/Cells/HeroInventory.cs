using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroInventory : MonoBehaviour
{
    public List<Token> smallTokens { get; private set; }
    public List<Token> gold { get; private set; }
    public Token bigToken { get; private set; }
    public Token helm { get; private set; }


    private int spaceSmall;
    private int numOfGold;


    void OnEnable() {
      EventManager.InventoryUICellEnter += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUICellEnter -= updateUI;
    }

    void start(){
      smallTokens = new List<Token>();
      gold = new List<Token>();
      bigToken = null;
      helm = null;
      gold =null;
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
       return true;
     }

    public void RemoveSmallToken(Token token){
      smallTokens.Remove(token);
    }


    public bool AddBigToken(Token token){
      if(bigToken != null){
        Debug.Log("Not enough room ");
        return false;
      }
       bigToken = token;
       return true;
    }

    public void RemoveBigToken(Token token){
      bigToken = null;
    }

    public bool AddHelm(Token token){
      if(helm != null){
        Debug.Log("Not enough room ");
        return false;
      }
       helm = token;
       return true;
    }

    public void RemoveHelm(Token token){
      helm = null;
    }

    // maybe have a void return type
    public bool AddGold(Token token){
      gold.Add(token);
      numOfGold++;
      return true;
    }

    public void RemoveGold(Token token){
      numOfGold--;
      gold.Remove(token);
    }

#endregion

    public void updateUI(CellInventory inventory, int index){}


}
