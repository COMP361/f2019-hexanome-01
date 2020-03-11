using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CellInventory : MonoBehaviour {
  #region Fields

  protected string description;
  public int cellID;
  public PhotonView photonView;

  //public enum
  public List<Token> AllTokens { get; private set; }
  public List<Hero> Heroes { get; private set; }
  public List<Enemy> Enemies { get; private set; }
  public List<Farmer> Farmers { get; private set; }
  public List<Token> Tokens { get; private set; }
  public List<Token> Golds { get; private set; }
  #endregion

  #region Functions [Constructor]

  ~CellInventory() {
    EventManager.FarmerDestroyed -= FarmerDestroyed;
  }

  public CellInventory(int cellID) {
    AllTokens = new List<Token>();
    Heroes = new List<Hero>();
    Enemies = new List<Enemy>();
    Farmers = new List<Farmer>();
    Tokens = new List<Token>();
    Golds = new List<Token>();
    this.cellID = cellID;
    EventManager.FarmerDestroyed += FarmerDestroyed;
    photonView = new PhotonView();
  }
  #endregion

  void FarmerDestroyed(Farmer farmer) {
    if(Farmers.Contains(farmer)) {
        Farmers.Remove(farmer);
    }
  }

  public void addToken(Token token) {
    Type listType;
    AllTokens.Add(token);

    listType = Heroes.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        Heroes.Add((Hero)token);
        return;
    }

    listType = Enemies.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        Enemies.Add((Enemy)token);
        return;
    }

    listType = Farmers.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        Farmers.Add((Farmer)token);
        return;
    }
  }

    [PunRPC]
    public void RemoveTokenRPC(int objectIndex, int cellIndex){
      //if(cellID == cellIndex){
        Type listType;
        Token token = AllTokens[objectIndex];
        AllTokens.Remove(token);

        listType = Heroes.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
          Heroes.Remove((Hero)token);
          return;
        }

        listType = Enemies.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Enemies.Remove((Enemy)token);
            return;
          }

        listType = Farmers.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
          Farmers.Remove((Farmer)token);
          return;
        }
    //  }
    }

    public void RemoveToken(int objectIndex){
      //if(cellID == cellIndex){
      Debug.Log("ObjectIndex "+ objectIndex);
        Debug.Log("AllTokens Length "+ cellID + " " + AllTokens.Count);
        Token token = AllTokens[objectIndex];
        Type listType;
      //  Token token = AllTokens[objectIndex];
        AllTokens.Remove(token);

        listType = Heroes.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
          Heroes.Remove((Hero)token);
          return;
        }

        listType = Enemies.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Enemies.Remove((Enemy)token);
            return;
          }

        listType = Farmers.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
          Farmers.Remove((Farmer)token);
          return;
        }
    //  }
    }

  public void RemoveToken(Token token) {
  //  int objectIndex = AllTokens.IndexOf(token);
  //  int cellIndex = cellID;
  //  photonView.RPC("RemoveTokenRPC", RpcTarget.AllViaServer, new object[] {objectIndex, cellIndex});
  GameManager.instance.RemoveTokenCell(token, this);
  }

  public object Clone() {
    CellInventory ci = (CellInventory)this.MemberwiseClone();
    return ci;
  }
}
