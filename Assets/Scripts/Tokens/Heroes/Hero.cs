using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Photon.Pun;

public enum Sex
{
    Female,
    Male
}

public class Hero : Movable
{
    protected string[] names;
    protected int rank;
    public string Type { get; protected set; }
    public Color Color { get; set; }
    public Timeline timeline;
    public HeroInventory heroInventory;
    protected string heroDescription;
    public bool IsFighting { get; set; } = false;
    public bool IsSleeping { get; set; } = false;
    

    void OnEnable() {
        EventManager.Save += Save;
        base.OnEnable();
    }

    void OnDisable() {
        EventManager.Save -= Save;
    }

    public string HeroName {
        get {
            return names[(int)Sex];
        }
    }

    public Sex Sex { get; set; } = Sex.Female;

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
            if(value < 0) {
            _strength = 0;
            } else {
            _strength = value;
            }

            EventManager.TriggerUpdateHeroStats(this);

            if(GameManager.instance.MainHero.TokenName.Equals(this.TokenName)) {
              EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.MainHero.heroInventory);
            } else if(this.TokenName.Equals(CharChoice.choice.TokenName)){
              EventManager.TriggerInventoryUIHeroPeak(heroInventory);
            }
        }
    }

    public void setStrength(int amt){
      GameManager.instance.photonView.RPC("addStrengthRPC", RpcTarget.AllViaServer, new object[] {amt, this.TokenName});
    }
    public void setWP(int amt){
      GameManager.instance.photonView.RPC("setWPRPC", RpcTarget.AllViaServer, new object[] {amt, this.TokenName});
    }

    private int _willpower = 7;
    public int Willpower {
        get {
            return _willpower;
        }
        set {
            if(value < 0) {
                _willpower = 0;
            } else if(value > 20) {
                _willpower = 20;
            } else {
                _willpower = value;
            }
            
            EventManager.TriggerUpdateHeroStats(this);

            if(GameManager.instance.MainHero.TokenName.Equals(this.TokenName)) {
              EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.MainHero.heroInventory);
            } else if(this.TokenName.Equals(CharChoice.choice.TokenName)){
              EventManager.TriggerInventoryUIHeroPeak(heroInventory);
            }
        }
    }

    protected override Vector3 getWaypoint(Cell cell)
    {
        return cell.HeroesPosition;
    }

    public bool HasBow() {
        if(this is Archer) return true;
        if(heroInventory != null && heroInventory.bigToken != null && heroInventory.bigToken.GetType() == typeof(Bow)) return true;
        return false;
    }

    public List<Cell> GetAttackableCells() {
        List<Cell> cells = new List<Cell>();
        if(HasBow()) {
            cells = GameManager.instance.CurrentPlayer.Cell.WithinRange(0, 1);
            for (int i = cells.Count-1; i >= 0; i--) {
                if(cells[i].Inventory.Enemies.Count == 0) cells.Remove(cells[i]);
            }
        } else {
            if(GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies.Count > 0) {
                cells.Add(GameManager.instance.CurrentPlayer.Cell.Inventory.Enemies[0].Cell);
            }
        }

        return cells;
    }


    public bool HasSpecialDice() {
        List<Runestone> stones = new List<Runestone>();

        foreach (DictionaryEntry entry in heroInventory.smallTokens) {
            Token token = (Token)entry.Value;
            if (token is Runestone) stones.Add((Runestone)token);
        }

        if(stones.Count == 3
        && stones[0].color != stones[1].color
        && stones[0].color != stones[2].color
        && stones[1].color != stones[2].color) return true;

        return false;
    }

    public void Init() {
        timeline = new Timeline(this);
        heroInventory = new HeroInventory(Type.ToString());
        MovePerHour = 1;
    }

    public void Load(string saveId) {
        HeroState.Load(this, saveId);
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
    public Sex sex;
    public List<string> inventory;

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
        HeroState.Instance.sex = hero.Sex;

        HeroState.Instance.inventory = new List<string>();
        foreach(DictionaryEntry entry in hero.heroInventory.AllTokens){
            HeroState.Instance.inventory.Add(entry.Value.GetType().ToString());
        }

        FileManager.Save(Path.Combine(saveId, _gameDataId), HeroState.Instance);
    }

    public static void Load(Hero hero, String saveId) {
        String _gameDataId = hero.Type + ".json";
        _instance = FileManager.Load<HeroState>(Path.Combine(saveId, _gameDataId));

        hero.Cell = Cell.FromId(HeroState.Instance.cellId);
        hero.Willpower = HeroState.Instance.willpower;
        hero.Strength = HeroState.Instance.strength;
        hero.timeline.Index = HeroState.Instance.timelineIndex;
        hero.Sex = HeroState.Instance.sex;

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (string tokenStr in HeroState.Instance.inventory)
            {
                Type type = Type.GetType(tokenStr);
                MethodInfo mInfo = type.GetMethods().FirstOrDefault(method => method.Name == "Factory" && method.GetParameters().Count() == 0);
                if (type == typeof(Runestone))
                {
                    mInfo = type.GetMethods().FirstOrDefault(method => method.Name == "FactoryInventory" && method.GetParameters().Count() == 0);
                }
                Token token = (Token)mInfo.Invoke(type, null);

                hero.heroInventory.AddItem(token);
            }
        }
    }
}
