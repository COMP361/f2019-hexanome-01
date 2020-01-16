using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Hero
{
    public static Dwarf instance;
    
    void Awake()
    {
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Dwarf) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this;
        }
        
        rank = Cell.FromId(7);
        
        dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3
        };
    
        names = new string[2] {
            "Brigha",
            "Kram"
        };

        color = new Color(1, 0.9f, 0, 1);

        Init(rank);
    }
}