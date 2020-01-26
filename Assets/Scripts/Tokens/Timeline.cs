using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Timeline : ICloneable {
  private int index = 0;

  public Timeline() {

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
    index = 0;
  }
}