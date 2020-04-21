using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWitchBrew : Fog {
    public static void Factory() {
        Init("WitchBrew", 1, typeof(FogWitchBrew));
    }

    public override void ApplyEffect() {
        Witch.Instance.Cell = Cell.FromId(Cell.Index);
        GameManager.instance.narrator.TriggerWitchCard();

        if(GameManager.instance.CurrentPlayer != GameManager.instance.MainHero) return;
        Token potion = Potion.Factory();
        GameManager.instance.CurrentPlayer.Cell.Inventory.AddItem(potion);
        //GameManager.instance.CurrentPlayer.heroInventory.AddItem(potion);
    }
}
