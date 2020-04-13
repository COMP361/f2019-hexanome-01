using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWitchBrew : Fog {
    public static void Factory() {
        Init("WitchBrew", 1, typeof(FogWitchBrew));
    }

    public override void ApplyEffect(Hero hero) {
        if(hero != GameManager.instance.MainHero) return;
        Witch.Instance.Cell = Cell.FromId(Cell.Index);
        Token potion = Potion.Factory();
        GameManager.instance.CurrentPlayer.heroInventory.AddItem(potion);
        GameManager.instance.narrator.TriggerWitchCard();
    }
}
