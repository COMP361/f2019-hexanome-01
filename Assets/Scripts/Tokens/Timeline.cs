﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timeline : ICloneable {
  public int Index { get; set; }
  private int freeLimit = 7;
  private int extendedLimit = 10;
  Token token;

  public Timeline() {
    Index = 0;
  }

  public int GetFreeHours() {
    return freeLimit - Index;
  }

  public int GetExtendedHours() {
    return Math.Min(extendedLimit - Index, extendedLimit - freeLimit);
  }

  void OnEnable() {
    //EventManager.MoveSelect += InitMove;
    //EventManager.CellClick += InitMove;
    //EventManager.MoveCancel += ResetCommand;
    //EventManager.MoveConfirm += ExecuteMove;
  }

  void OnDisable() {
    //EventManager.MoveSelect -= InitMove;
    //EventManager.CellClick -= InitMove;
    //EventManager.MoveCancel -= ResetCommand;
    //EventManager.MoveConfirm -= ExecuteMove;
  }

  public object Clone() {
    Timeline tl = (Timeline) this.MemberwiseClone();
    return tl;
  }     

  void EndDay() {
    Index = 0;
  }
}