using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWitchBrew : Fog {
    public static void Factory() {
        Init("WitchBrew", 1, typeof(FogWitchBrew));
    }

    public override void ApplyEffect() {
        Witch.Instance.Cell = Cell.FromId(Cell.Index);
        Token potion = Potion.Factory(Cell.Index);
        GameManager.instance.narrator.TriggerWitchCard();
        //  GameManager.instance.CurrentPlayer.heroInventory.AddItem(potion);
        Cell = null;
        Destroy(gameObject);
    }
}
