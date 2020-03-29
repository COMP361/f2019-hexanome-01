using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog3Will : Fog {
    public static void Factory() {
        Init("3Will", 1, typeof(Fog3Will));
    }

    public override void ApplyEffect() {
        GameManager.instance.CurrentPlayer.Willpower += 3;
        Cell = null;
        Destroy(gameObject);
    }
}