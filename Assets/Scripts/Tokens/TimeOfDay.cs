using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeOfDay : ICloneable
{
    public int Index { get; set; }
    private static int freeLimit = 7;
    private static int extendedLimit = 10;
    GameObject token;
    public string heroName { get; set; }
    public Color color { get; set; }

    public TimeOfDay(Color heroColor, string hero_Name)
    {
        Index = 0;
        heroName = hero_Name;
        color = heroColor;
        
        token = Geometry.Disc(Vector3.zero, heroColor, 8);
        token.name = "TimeOfDay" + hero_Name;
        token.transform.localScale = new Vector3(10, 0.1f, 10);
        token.transform.parent = GameObject.Find("Tokens").transform;
        token.transform.position = GameObject.Find("Timeline/Sunrise/" + hero_Name).transform.position;
    }

    public int GetFreeHours()
    {
        return GetFreeHours(Index);
    }

    public int GetExtendedHours()
    {
        return GetExtendedHours(Index);
    }

    public static int GetFreeHours(int index)
    {
        return freeLimit - index;
    }

    public static int GetExtendedHours(int index)
    {
        return Math.Min(extendedLimit - index, extendedLimit - freeLimit);
    }

    // Update time of day 
    public void update(int numDays)
    {
        Index += numDays % extendedLimit;
        token.transform.position = GameObject.Find("Timeline/" + Index + "/" + heroName).transform.position;
    }

    public void reset()
    {
        Index = 0;
        token.transform.position = GameObject.Find("Timeline/Sunrise/" + heroName).transform.position;
    }

    public object Clone()
    {
        TimeOfDay tl = (TimeOfDay)this.MemberwiseClone();
        return tl;
    }

    public void EndDay()
    {
        Index = 0;
        token.transform.position = GameObject.Find("Timeline/Sunrise/" + heroName).transform.position;
    }
}