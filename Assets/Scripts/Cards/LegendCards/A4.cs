using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4 : LegendCard
{
    public A4()
    {
        this.id = "A4";

        this.story = "At first sunlight, the heroes receive a message: Old King Brandur's willpower seem to have weakened with the passage of time. " +
            "But there is said to be a herb growing in the mountain passes that can revive a person's life.";

        this.task = "The players must heal the king with the medicinal herb. " +
            "To do that, they must find the witch, only she knows where the locations where this herb grows. " +
            "The witch is hiding behind one of the fog tokens.";
    }

    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}
