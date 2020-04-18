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
        if(GameManager.instance.CurrentPlayer != GameManager.instance.MainHero) return;
        GoldCoin.Factory(Cell.Index);
    }
}
