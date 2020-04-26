using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FogStrength : Fog {
    public static void Factory() {
        Init("Strength", 1, typeof(FogStrength));
    }

    public override void ApplyEffect() {
        Cell.Inventory.Heroes[0].Strength += 1;
    }
}
