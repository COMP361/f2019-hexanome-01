using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N : LegendCard
{
    public N()
    {
        this.id = "N";

        this.story = "Congrats! With the heroes' combined powers, you were able to take the skral's stronghold. The medicinal herb did its work as well, and King Brandur soon felt better. " +
            "And yet, the heroes still felt troubled. " +
            "The King's son, Prince Thorald, had not yet returned. What is keeping him so long?";
    }

    public override void ApplyEffect()
    {
       if(GameManager.instance.narrator.medicineDelivered && GameManager.instance.narrator.towerSkralDefeated)
        {
            Debug.Log("Game Won");
        }
        else
        {
            Debug.Log("Game Lost");
        }
    }
}
