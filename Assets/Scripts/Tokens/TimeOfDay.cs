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
    public Color color { get; set; }

    public TimeOfDay(Color heroColor, string hero_Name)
    {
        Index = 0;
        heroName = hero_Name;
        color = heroColor;
        Sprite sprite = null;

        //token = Geometry.Disc(Vector3.zero, color, 8);
        switch (hero_Name)
        {
            case "Dwarf":
                sprite = Resources.Load<Sprite>("Sprites/heroes/hero-yellow");
                break;
            case "Archer":
                sprite = Resources.Load<Sprite>("Sprites/heroes/hero-green");
                break;
            case "Warrior":
                sprite = Resources.Load<Sprite>("Sprites/heroes/hero-blue");
                break;
            case "Mage":
                sprite = Resources.Load<Sprite>("Sprites/heroes/hero-purple");
                break;
        }
        token = new GameObject("TimeOfDay" + hero_Name);

        SpriteRenderer renderer = token.AddComponent<SpriteRenderer>();
        if (sprite != null) renderer.sprite = sprite;
        token.transform.localScale = new Vector3(10, 10, 10);

        token.name = "TimeOfDay" + hero_Name;
        token.transform.parent = GameObject.Find("Tokens").transform;


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