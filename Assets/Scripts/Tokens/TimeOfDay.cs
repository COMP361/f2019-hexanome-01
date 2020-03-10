using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeOfDay : ICloneable
{
    public int Index { get; set; }
    private int freeLimit = 7;
    private int extendedLimit = 10;
    GameObject token;
    public string heroName { get; set; }

    public TimeOfDay(Color color, string hero_Name)
    {
        Index = 0;
        token = Geometry.Disc(Vector3.zero, color, 10);
        token.name = "TimeOfDay" + hero_Name;
        token.transform.parent = GameObject.Find("Tokens").transform;
        heroName = hero_Name;

        token.transform.position = GameObject.Find("Timeline/Sunrise/" + hero_Name).transform.position;
    }

    public int GetFreeHours()
    {
        return freeLimit - Index;
    }

    public int GetExtendedHours()
    {
        return Math.Min(extendedLimit - Index, extendedLimit - freeLimit);
    }

    // update time of day... 
    public void update(int numDays)
    {
        Index += numDays % extendedLimit;
        token.transform.position = GameObject.Find("Timeline/" + Index + "/" + heroName).transform.position;
        //Debug.Log("HeroName inside time of day: " + heroName);
        //Debug.Log("index calculation: " + Index);
    }

    void OnEnable()
    {
        //EventManager.MoveSelect += InitMove;
        //EventManager.CellClick += InitMove;
        //EventManager.MoveCancel += ResetCommand;
        //EventManager.MoveConfirm += ExecuteMove;
    }

    void OnDisable()
    {
        //EventManager.MoveSelect -= InitMove;
        //EventManager.CellClick -= InitMove;
        //EventManager.MoveCancel -= ResetCommand;
        //EventManager.MoveConfirm -= ExecuteMove;
    }

    public object Clone()
    {
        TimeOfDay tl = (TimeOfDay)this.MemberwiseClone();
        return tl;
    }

    public void EndDay()
    {
        Index = 0;
    }
}