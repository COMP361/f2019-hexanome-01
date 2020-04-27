using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timeline
{
    private int _index = 0;
    public static int freeLimit = 7;
    public static int extendedLimit = 10;
    GameObject token;
    Hero hero;
    public int Index {
        get {
            return _index;
        }
        set {
            _index = value;
            GameObject timeSlot = GameObject.Find("Timeline/" + _index + "/" + hero.GetType());
            if(timeSlot != null) token.transform.position = timeSlot.transform.position;
        }
    }

    public Timeline(Hero hero)
    {
        this.hero = hero;

        token = new GameObject("Timeline" + hero.GetType());
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + hero.GetType().ToString());
        SpriteRenderer sr = token.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 3;
        token.transform.localScale = new Vector3(10, 10, 10);
        token.transform.parent = GameObject.Find("Tokens").transform;
        token.transform.position = GameObject.Find("Timeline/0/" + hero.name).transform.position;
        Index = 0;

        EventManager.Move += OnMove;
    }

    ~Timeline() {
        EventManager.Move -= OnMove;
    }

    public bool HasHoursLeft()
    {
        return GetFreeHours() > 0 || GetExtendedHours() > 0;
    }

    public int GetFreeHours()
    {
        return GetFreeHours(Index);
    }

    public int GetExtendedHours()
    {
        return GetExtendedHours(Index, hero.Willpower);
    }

    public static int GetFreeHours(int index)
    {
        return Math.Max(freeLimit - index, 0);
    }

    public static int GetExtendedHours(int index, int willpower)
    {
        int count = Math.Min(extendedLimit - index, extendedLimit - freeLimit);
        int i;
        for(i = 0; i <= count; i++) {
            if(willpower - i * 2 < 3 || i == count) break;
        }
        return i;
    }

    private void OnMove(Movable movable, int qty) {
        if(movable.MovePerHour == 0) return;
        if(qty == 0) return;
        if(GameManager.instance.CurrentPlayer != hero) return;

        int cost = (int)Math.Ceiling((double)qty/movable.MovePerHour);
        Update(cost);
    }

    // Update time of day
    public void Update(int cost)
    {
        int freeHours = GetFreeHours();
        int extendedHours = GetExtendedHours();

        if (Index + cost > extendedLimit) {
            Debug.Log("Invalid Change of Timeline");
            return;
        }

        // if path reaches 8, 9 10 decreae willpoints
        if (cost > freeHours) {
            int wp = (cost - freeHours) * 2;
            hero.Willpower -= wp;
        }

        Index += cost;
    }

    void Update(Hero hero, int cost) {
        if(hero != this.hero) return;
        Update(cost);
    }

    public void Reset()
    {
        Index = 0;
    }
}
