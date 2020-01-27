using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    public static Mage instance;
    
    void Awake()
    {
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(Mage) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this;
        }

        dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1
        };
    
        names = new string[2] {
            "Eara",
            "Liphardus"
        };

        Setup(34, new Color(0.6f, 0.2f, 1, 1));
    }
}