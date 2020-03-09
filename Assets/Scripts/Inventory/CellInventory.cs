using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInventory : ICloneable {
  #region Fields

  protected string description;
  // protected Transform textTransform;


  //public InventoryUICell InventoryUICell;

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

  public CellInventory() {
    AllTokens = new List<Token>();
    Heroes = new List<Hero>();
    Enemies = new List<Enemy>();
    Farmers = new List<Farmer>();
    Tokens = new List<Token>();
    Golds = new List<Token>();

    EventManager.FarmerDestroyed += FarmerDestroyed;

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

  public void RemoveToken(Token token) {
    Type listType;
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
  }

  public object Clone() {
    CellInventory ci = (CellInventory)this.MemberwiseClone();
    return ci;
  }
}
