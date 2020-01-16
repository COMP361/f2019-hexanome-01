using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    public static Archer instance;
    
    public void Awake()
    {
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Archer) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this;
        }
        
        rank = Cell.FromId(25);
        
        dices = new int[21] {
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5
        };
    
        names = new string[2] {
            "Chada",
            "Pasco"
        };

        color = new Color(0.4f, 0.75f, 0, 1);

        Init(rank);
    }
}