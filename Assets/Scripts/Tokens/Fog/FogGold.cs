using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGold : Fog {
    public static void Factory() {
        FogGold.Init("Gold", 3, typeof(FogGold));
    }

    public override void ApplyEffect() {
        Token goldCoin = GoldCoin.Factory(Cell.Index);
      //  GameManager.instance.CurrentPlayer.heroInventory.AddItem(goldCoin);
        Cell = null;
        Destroy(gameObject);
    }
}
