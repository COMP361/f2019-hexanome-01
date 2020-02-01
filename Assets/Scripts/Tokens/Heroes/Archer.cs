using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    static Archer _instance;
    
    static void Factory() {
        Color color = new Color(0.4f, 0.75f, 0, 1);
        GameObject go = Geometry.Disc(Vector3.zero, color);
        Archer archer = go.AddComponent<Archer>();
        archer.Color = color;

        archer.Type = typeof(Archer).ToString();
        archer.TokenName = archer.Type;

        archer.rank = 25;
        Cell cell = Cell.FromId(archer.rank);
        archer.Cell = cell;
        archer.State = new HeroState(cell);

        cell.State.addHero(archer);

        archer.IsDone = false;
            
        archer.Dices = new int[21] {
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5
        };
    
        archer.names = new string[2] {
            "Chada",
            "Pasco"
        };
    }

    void Awake() {
        if (_instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Archer) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public static Archer Instance {
        get {
            if(!_instance) {
                Archer.Factory();
            } 

            return _instance;
        } 
        private set {
            _instance = value;
        }
    }
}