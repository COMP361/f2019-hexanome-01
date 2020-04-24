using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog2Will : Fog {
    public static void Factory() {
        Init("2Will", 1, typeof(Fog2Will));
    }

    public override void ApplyEffect() {
        GameManager.instance.CurrentPlayer.Willpower += 2;
    }
}
