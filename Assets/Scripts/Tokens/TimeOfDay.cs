using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeOfDay : ICloneable {
  public int Index { get; set; }
  private int freeLimit = 7;
  private int extendedLimit = 10;
  GameObject token;
  Hero hero;

  public TimeOfDay(Color color, string heroName) {
    Index = 0;
    token = Geometry.Disc(Vector3.zero, color, 10);
    token.name = "TimeOfDay" + heroName;
    token.transform.parent = GameObject.Find("Tokens").transform;
    this.hero = hero;

    token.transform.position = GameObject.Find("Timeline/Sunrise/" + heroName).transform.position;
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
    TimeOfDay tl = (TimeOfDay) this.MemberwiseClone();
    return tl;
  }     

  void EndDay() {
    Index = 0;
  }
}