using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using  System.Collections.Specialized;

public class CellInventory {
  #region Fields

  protected string description;
  public int cellID;
  public PhotonView photonView;
  public List<Token> AllTokens { get; private set; }
  public List<Hero> Heroes { get; private set; }
  public List<Enemy> Enemies { get; private set; }
  public List<Farmer> Farmers { get; private set; }
  public List<Token> Golds { get; private set; }
  public Well Well { get; private set; }
  public OrderedDictionary items { get; private set; }

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
    Golds = new List<Token>();

    items = new OrderedDictionary();

    this.cellID = cellID;
    EventManager.FarmerDestroyed += FarmerDestroyed;
  }
  #endregion

  void FarmerDestroyed(Farmer farmer) {
    if(Farmers.Contains(farmer)) {
      Farmers.Remove(farmer);
      AllTokens.Remove(farmer);
    }
  }

  public void AddToken(Token token) {
    Type listType;

    listType = Heroes.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        AllTokens.Add(token);
        Heroes.Add((Hero)token);
        return;
    }

    listType = Enemies.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        AllTokens.Add(token);
        Enemies.Add((Enemy)token);
        return;
    }

    listType = Farmers.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        AllTokens.Add(token);
        Farmers.Add((Farmer)token);
        return;
    }

    if(token is Fog){
      AllTokens.Add(token);
      return;
    }

    if(token is Well){
      AllTokens.Add(token);
      Well = (Well)token;
      return;
    }

    if(token is Witch){
      AllTokens.Add(token);
      return;
    }

    if (token is Thorald){
      AllTokens.Add(token);
      return;
    }

    // if none of these options means its an item
    AddItem(token);
  }

  public void AddItem(Token item){
    if(item != null){
      int viewID = item.GetComponent<PhotonView>().ViewID;
      GameManager.instance.photonView.RPC("AddItemCellRPC", RpcTarget.AllViaServer, new object[] {viewID, cellID});
    }
  }

  public void AddItem2(Token item){
    string id = convertToKey(item.GetComponent<PhotonView>().ViewID);
    items.Add(id, item);
    InventoryUICell.instance.ForceUpdate(this, cellID);
    EventManager.TriggerCellItemUpdate(cellID);
  }


  public void RemoveItem(int viewID){
    if(items.Contains(convertToKey(viewID))){
      items.Remove(convertToKey(viewID));
      InventoryUICell.instance.ForceUpdate(this, cellID);
      EventManager.TriggerCellItemUpdate(cellID);
    }
    else{
      //error
      Debug.Log("ERROR REMOVE ITEM CELL INVENTORY");
    }
  }

  public void RemoveToken(int objectIndex){
    if(objectIndex > AllTokens.Count - 1 || objectIndex < 0) return;

    Token token = AllTokens[objectIndex];
    AllTokens.Remove(token);

    Type listType = Heroes.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
      Heroes.Remove((Hero)token);
      InventoryUICell.instance.ForceUpdate(this, cellID);
      return;
    }

    listType = Enemies.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
        Enemies.Remove((Enemy)token);
        InventoryUICell.instance.ForceUpdate(this, cellID);
        return;
      }

    listType = Farmers.GetListType();
    if (listType.IsCompatibleWith(token.GetType())) {
      Farmers.Remove((Farmer)token);
      InventoryUICell.instance.ForceUpdate(this, cellID);
      return;
    }

    if(typeof(Well).IsCompatibleWith(token.GetType())) {
      Well = null;
      InventoryUICell.instance.ForceUpdate(this, cellID);
      return;
    }

    InventoryUICell.instance.ForceUpdate(this, cellID);
  }

  public void RemoveToken(Token token) {
    if(token == null) return;

    if(token is Hero || token is Fog || token is Well || token is Enemy || token is Farmer || token is Witch || token is Thorald){
      GameManager.instance.RemoveTokenCell(token, this);
    } else { 
      // is an item
      int viewID = token.GetComponent<PhotonView>().ViewID;
      if(items.Contains(convertToKey(token.GetComponent<PhotonView>().ViewID))){
        GameManager.instance.photonView.RPC("RemoveItemCellRPC", RpcTarget.AllViaServer, new object[] {viewID, cellID});
      } else {
        Debug.Log("Error Cell RemoveToken1");
      }
    }
  }

  public string convertToKey(int a){
    string toReturn = "" + a;
    return toReturn;
  }
}
