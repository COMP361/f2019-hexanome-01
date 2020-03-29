using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWitchBrew : Fog {
    public static void Factory() {
        Init("WitchBrew", 1, typeof(FogWitchBrew));
    }

    public override void ApplyEffect() {
        Witch.Factory(Cell.Index);
        GameManager.instance.witch = Witch.Instance;

        Token potion = Potion.Factory();
        GameManager.instance.CurrentPlayer.heroInventory.AddItem(potion);

        Cell = null;
        Destroy(gameObject);
    }
}
