using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4 : LegendCard
{
    public A4()
    {
        this.id = "A4";

        this.story = "A gloomy mood has fallen upon the people. Rumors are making the rounds that skarls have set up a stronghold in some undisclosed location. " +
        "The heroes have scattered themselves acrosse the entire land in search of this location. The defense in the castle is in their hands alone. " +
        "Many farmers have asked for help and are seeking shelter behind the high walls of Rietburg Castle." +
        "At first sunlight, the heroes receive a message: Old King Brandur's willpower seem to have weakened with the passage of time. " +
            "But there is said to be a herb growing in the mountain passes that can revive a person's life.";

        this.task = "The players must heal the king with the medicinal herb. " +
            "To do that, they must find the witch, only she knows where the locations where this herb grows. " +
            "The witch is hiding behind one of the fog tokens.";
    }

    public override void ApplyEffect()
    {
        //throw new System.NotImplementedException();
    }
}
