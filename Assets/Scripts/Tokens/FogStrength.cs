using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogStrength : Fog {
    public static void Factory() {
        Init("Strength", 1, typeof(FogStrength));
    }

    public override void ApplyEffect() {
        GameManager.instance.CurrentPlayer.Strength += 1;     
        Cell = null;   
        Destroy(gameObject);
    }
}