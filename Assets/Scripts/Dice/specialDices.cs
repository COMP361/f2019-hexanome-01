using System.Collections;
using UnityEngine;

public class specialDices : regularDices {

    public void Awake () {
        base.Awake();
        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Dices/specialDices/");
        emptyDice = Resources.Load<Sprite>("Dices/sd0");
	}
	
    
    // Coroutine that rolls the dice
    public override void RollTheDice() {

        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            //Debug.Log(randomDiceSide);
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
        }
        
        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        if(finalSide <= 2)
        {
            finalSide = 6;
        }
        else if(finalSide == 3)
        {
            finalSide = 8;
        }
        else if(finalSide <= 5)
        {
            finalSide = 10;
        }
        else
        {
            finalSide = 12;
        }

        // Show final dice value in Console
        //Debug.Log(finalSide);
    }
}
