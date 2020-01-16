using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    public static Warrior instance;
    
    void Awake()
    {    
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Warrior) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this;
        }
   
        rank = Cell.FromId(14);
        
        dices = new int[21] {
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4
        };
    
        names = new string[2] {
            "Mairen",
            "Thorn"
        };

        color = new Color(0.09f, 0.6f, 1, 1);

        Init(rank);
    }
}