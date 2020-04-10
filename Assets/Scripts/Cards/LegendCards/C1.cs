using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class C1 : LegendCard
{
    public bool isEasy;

    public C1(bool isEasy)
    {
        this.id = "C1";

        this.isEasy = isEasy;

        this.story = "The king's scouts have discovered the skarl stronghold. " + 
            "Rumors are circulating about cruel wardraks from the south. " +
            "They have not yet been sighted, but more and more farmers are losing their courage, leaving their farmsteads, and seeking safety in the castle.";

        this.task = "The skral on the tower must be defeated. As soon as he is defeated, the Narrator is advanced to the letter N on the Legend track.";
    }

    public override void ApplyEffect()
    {
        int towerSkrallCell = GameManager.instance.narrator.towerSkralCell;
        int numPlayers = PhotonNetwork.PlayerList.Count();
        GameManager.instance.towerskrals.Add(TowerSkral.Factory(towerSkrallCell, numPlayers));
        GameManager.instance.farmers.Add(Farmer.Factory(28));
    }
}
