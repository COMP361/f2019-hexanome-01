using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameState
{
    public List<HeroState> heroes = new List<HeroState>();
    public List<CellState> cells = new List<CellState>();

    public GameState() { }

    private static GameState _instance;
    public static GameState Instance
    {
        get
        {
            //if (_instance == null) Load();
            if (_instance == null) _instance = new GameState();
            return _instance;
        }
    }

    public void Load()
    {
    }

    public void Save(String saveId)
    {
        String _gameDataId = "gamesave.json";
        for(int i = 0; i <= 72; i++)
        {
            cells.Add(new CellState(Cell.FromId(i)));
        }
        FileManager.Save(Path.Combine(saveId, _gameDataId), GameState.Instance);
    }
}
