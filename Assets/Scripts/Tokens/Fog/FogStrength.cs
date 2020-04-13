using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogStrength : Fog {
    public static void Factory() {
        Init("Strength", 1, typeof(FogStrength));
    }

    public override void ApplyEffect(Hero hero) {
        if(hero != GameManager.instance.MainHero) return;
        GameManager.instance.CurrentPlayer.Strength += 1;     
    }
}