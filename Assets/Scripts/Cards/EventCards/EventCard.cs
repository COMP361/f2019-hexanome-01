using UnityEngine;
using System.Collections.Generic;

public abstract class EventCard {
    public int id;
    public string intro;
    
    public string effect;
    
    public bool shield = false;
    public bool group = false;

    public abstract void ApplyEffect();
}