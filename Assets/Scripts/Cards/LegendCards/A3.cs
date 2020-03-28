using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3 : LegendCard
{
    public bool isEasy;

    public A3(bool isEasy)
    {
        this.id = "A3";

        this.isEasy = isEasy; 

        this.story = "A gloomy mood has fallen upon the people. Rumors are making the rounds that skarls have set up a stronghold in some undisclosed location. " +
        "The heroes have scattered themselves acrosse the entire land in search of this location. The defense in the castle is in their hands alone. " +
        "Many farmers have asked for help and are seeking shelter behind the high walls of Rietburg Castle.";

        this.additionalInfo = "Farmers can be brought into the castle. The players can move their heroes to a farmer token and carry it along with them. " +
            "The players are allowed to carry multiple farmers. " +
            "If they encounter a creature while carrying farmers, the farmers immediately die. " +
            "For every farmer brought back to the castle, one more creature is allowed in the castle.";

        this.task = "";
    }

    public override void ApplyEffect()
    {
        //throw new System.NotImplementedException();
    }
}
