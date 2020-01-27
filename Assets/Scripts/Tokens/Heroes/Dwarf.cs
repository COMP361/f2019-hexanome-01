using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Hero
{
    public static Dwarf instance;
    
    void Awake() {
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Dwarf) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this;
        }
        
        dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3
        };
    
        names = new string[2] {
            "Brigha",
            "Kram"
        };
        
        Setup(7, new Color(1, 0.9f, 0, 1));
    }
}