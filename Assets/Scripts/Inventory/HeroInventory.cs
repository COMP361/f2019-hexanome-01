using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HeroInventory : MonoBehaviour
{
    public List<Token> smallTokens { get; private set; }
    public List<Token> golds { get; private set; }
    public BigToken bigToken { get; private set; }
    public Helm helm { get; private set; }
    public List<Token> AllTokens { get; private set; }

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
      smallTokens = new List<Token>();
      golds = new List<Token>();
      bigToken =null;
      helm = null;
      spaceSmall = 3;
      numOfGold = 0;
      this.parentHero = parentHero;
      photonView = new PhotonView();
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
    public bool AddGold(Token token){
      golds.Add(token);
      numOfGold++;
      if(GameManager.instance.MainHero.State.heroInventory == this){
      EventManager.TriggerInventoryUIHeroUpdate(this);
    }
      return true;
    }

    [PunRPC]
    public void RemoveTokenRPC(int objectIndex, int cellIndex){

        Type listType;

        Token token = AllTokens[objectIndex];

        listType = smallTokens.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            smallTokens.Remove((SmallToken)token);
            return;
          }

        listType = bigToken.GetType();
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

    public void RemoveToken(Token token) {
      int objectIndex = AllTokens.IndexOf(token);
      photonView.RPC("RemoveTokenRPC", RpcTarget.AllViaServer, new object[] {objectIndex});
    }

    public void RemoveGold(int amtToRemove)
    {
        if (golds.Count >= amtToRemove)
        {
            numOfGold-= amtToRemove;
            while(amtToRemove != 0)
            {
                amtToRemove--;
                golds.RemoveAt(amtToRemove);
            }
            
        }
        EventManager.TriggerInventoryUIHeroUpdate(this);
    }

    #endregion

    public void updateUI(CellInventory inventory, int index){}


}
