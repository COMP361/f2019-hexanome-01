using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEvent : Fog {
    public static void Factory() {
        FogEvent.Init("Event", 5, typeof(FogEvent));
    }

    public override void ApplyEffect() {
        EventCardDeck.Instance.GetCard();
    }
}
