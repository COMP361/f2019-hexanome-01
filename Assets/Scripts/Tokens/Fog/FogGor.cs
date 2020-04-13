using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGor : Fog {
    public static void Factory() {
        Init("Gor", 2, typeof(FogGor));
    }

    public override void ApplyEffect(Hero hero) {
        GameManager.instance.gors.Add(Gor.Factory(Cell.Index));
    }
}
