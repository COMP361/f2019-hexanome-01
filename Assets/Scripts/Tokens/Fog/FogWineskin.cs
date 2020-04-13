using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWineSkin : Fog {
    public static void Factory() {
        Init("WineSkin", 1, typeof(FogWineSkin));
    }

    public override void ApplyEffect(Hero hero) {
        if(hero != GameManager.instance.MainHero) return;
        Wineskin.Factory(Cell.Index);
    }
}
