using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G : LegendCard
{
    public G()
    {
        this.id = "G";

        this.story = "Prince Thorald joins up with a scouting patrol with the intention of leaving for just a few days. " +
            "He is not to be seen again for quite a long time. " +
            "Black shadows are moving in the moonlight. The rumors were right the wardraks are coming!";

        this.effect = "Prince Thorald is removed from the game.";
    }

    public override void ApplyEffect()
    {
        Thorald.Instance.Cell = null;
        Thorald.Instance.Destroy();
        GameManager.instance.wardraks.Add(Wardrak.Factory(26));
        GameManager.instance.wardraks.Add(Wardrak.Factory(27));
    }
}
