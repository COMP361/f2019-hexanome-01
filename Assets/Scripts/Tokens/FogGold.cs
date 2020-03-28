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
        GameObject goldCoinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/GoldCoin", Vector3.zero, Quaternion.identity, 0);
        Token goldCoin = goldCoinGO.GetComponent<GoldCoin>();
        GameManager.instance.CurrentPlayer.heroInventory.AddGold(goldCoin);
        Cell = null;
        Destroy(gameObject);
    }
}