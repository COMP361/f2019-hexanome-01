using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWineSkin : Fog {
    public static void Factory() {
        Init("WineSkin", 1, typeof(FogWineSkin));
    }

    public override void ApplyEffect() {
        Token wineskin = Wineskin.Factory(Cell.Index);
      //  GameManager.instance.CurrentPlayer.heroInventory.AddItem(wineskin);
        Cell = null;
        Destroy(gameObject);
    }
}
