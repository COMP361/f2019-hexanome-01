using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class Castle : Cell
{
    void OnEnable() {
        EventManager.MoveComplete += UpdateShields;
        EventManager.CellItemUpdate += HasHerb;
        EventManager.Save += Save;
        base.OnEnable();
    }

    void OnDisable() {
        EventManager.MoveComplete -= UpdateShields;
        EventManager.CellItemUpdate -= HasHerb;
        EventManager.Save -= Save;
        base.OnDisable();
    }
    
    void Awake()
    {
        if (Instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Castle) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        base.Awake();
    }

    public void Init(int numOfPlayers) {
        ShieldsCount = 2;
        if (numOfPlayers == 4) ShieldsCount = 1;
        
        EventManager.TriggerShieldsUpdate(ShieldsCount);
    }
    
    public static bool IsCastle(Cell cell) {
        return cell.Index == 0;
    }

    public int ShieldsCount { get; private set; }

    public void UpdateShields(Movable movable) {
        if(movable.Cell.Index != Index) return;

        if (typeof(Enemy).IsCompatibleWith(movable.GetType())) {
            ShieldsCount--;
            // Destroy Monster
            Inventory.RemoveToken(movable);
            Destroy(movable.gameObject);
            EventManager.TriggerEnemyDestroyed((Enemy)movable);
        } else if (typeof(Farmer).IsCompatibleWith(movable.GetType())) {
            ShieldsCount++;
            // Destroy Farmer
            Inventory.RemoveToken(movable);
            Destroy(movable.gameObject);
            EventManager.TriggerFarmerDestroyed((Farmer)movable);  
        } 
         
        if ( ShieldsCount < 1) EventManager.TriggerGameOver();
        EventManager.TriggerShieldsUpdate(ShieldsCount);
    }

    void HasHerb(int cellID) {
        if(Index == cellID) {
            foreach(DictionaryEntry entry in Inventory.items) {
                Token token = (Token)entry.Value;
                if(token is Herb) {
                    EventManager.TriggerHerbInCastle();
                }
            }
        } 
    }

    // Call this when the tower skrall is defeated
    public void CheckWin()
    {
        Boolean hasHerb = false;
        foreach(object item in Inventory.items.Values)
        {
            if (item.GetType() == typeof(Herb))
            {
                hasHerb = true;
            }
        }

        if (hasHerb)
        {
            EventManager.TriggerGameWin();
        }
        else
        {
            EventManager.TriggerGameOver();
        }
    }

    public void Save(String saveId) {
        String _gameDataId = "Castle.json";
        FileManager.Save(Path.Combine(saveId, _gameDataId), new CastleState(ShieldsCount));
    }

    public static Castle Instance { get; private set; }
}


public class CastleState {
    public int shields;
    
    public CastleState(int shields) {
        this.shields = shields;
    }
};