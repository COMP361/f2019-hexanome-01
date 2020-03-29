using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LegendCard
{
    public string id;
    public string story;
    public string task;
    public string effect;
    public string additionalInfo;
    public bool group = false;

    public abstract void ApplyEffect();
}