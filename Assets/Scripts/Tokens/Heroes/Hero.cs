using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public enum Sex
{
    Female,
    Male
}

public class Hero : Movable
{
    protected Sex sex = Sex.Female;
    protected string[] names;
    protected int rank;
    public string Type { get; protected set; }
    public Color Color { get; set; }
    public Timeline timeline;
    public HeroInventory heroInventory;
    protected string heroDescription;
    
    void OnEnable() {
        //EventManager.Save += Save;
    }

    void OnDisable() {
        //EventManager.Save -= Save;
    }

    public string HeroName {
        get {
            return names[(int)sex];
        }
    }

    public Sex Sex {
        get {
            return sex;
        }
    }

    public String getSex()
    {
        if (sex == 0)
        {
            return "female";
        }
        else
        {
            return "male";
        }
    }

    public string HeroDescription{
        get {
            return heroDescription;
        }
    }

    public int[] Dices { get; protected set; }
    public Action Action { get; set; } = Action.None;

    private int _strength = 1;
    public int Strength {
        get {
            return _strength;
        }
        set {
            if(value < 0) _strength = 0;
            _strength = value;
            EventManager.TriggerUpdateHeroStats(this);
        }
    }

    private int _willpower = 7;

    public int Willpower {
        get {
            return _willpower;
        }
        set {
            if(value < 0) _willpower = 0;
            _willpower = value;
            EventManager.TriggerUpdateHeroStats(this);
        }
    }

    protected override Vector3 getWaypoint(Cell cell)
    {
        return cell.HeroesPosition;
    }

    public void Init() {
        timeline = new Timeline(this);
        heroInventory = new HeroInventory(Type.ToString());
        MovePerHour = 1;
    }
    
    public void Load() {
        // Need a string
        //SaveGame.Load();
        //Cell = Cell.FromId(SaveGame.Instance.heroCellID);
    }

    public void Save(String saveId) {
        HeroState.Save(this, saveId);
    }
}

public class HeroState {
    public int cellId;
    public int willpower;
    public int strength;
    public int timelineIndex;
    
    
    public HeroState() {}

    private static HeroState _instance;
    public static HeroState Instance {
        get {
            //if (_instance == null) Load();
            if (_instance == null) _instance = new HeroState();
            return _instance;
        }
    }

    public static void Save(Hero hero, String saveId) {
        String _gameDataId = hero.Type + ".json";
        HeroState.Instance.cellId = hero.Cell.Index;
        HeroState.Instance.willpower = hero.Willpower;
        HeroState.Instance.strength = hero.Strength;
        HeroState.Instance.timelineIndex = hero.timeline.Index;

        FileManager.Save(Path.Combine(saveId, _gameDataId), HeroState.Instance);
    }

    public static void Load(Hero hero, String saveId) {
        String _gameDataId = hero.Type + ".json";
        _instance = FileManager.Load<HeroState>(Path.Combine(saveId, _gameDataId));
    }
}