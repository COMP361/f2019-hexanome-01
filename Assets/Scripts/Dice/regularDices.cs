using System.Collections;
using UnityEngine;

public class regularDices : MonoBehaviour
{

    // Array of dice sides sprites to load from Resources folder
    protected Sprite[] diceSides;
    protected Sprite emptyDice;
    // Reference to sprite renderer to change sprites
    protected SpriteRenderer rend;
    public int finalSide;

    
    public void Awake() {
        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();
        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Dices/regularDices/");
        emptyDice = Resources.Load<Sprite>("Dices/rd0");
    }

    public void FlipTheDie()
    {
        int otherSide = 7 - this.finalSide;
        this.finalSide = otherSide;
        this.rend.sprite = diceSides[otherSide - 1];
    }

    public void ResetTheDie()
    {
        this.rend.sprite = emptyDice;
    }

    // Coroutine that rolls the dice
    public virtual void RollTheDice()
    {

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

            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            //yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        //Debug.Log(finalSide);
    }

    public int getFinalSide()
    {
        return finalSide;
    }

}
