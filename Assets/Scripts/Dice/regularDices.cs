﻿using System.Collections;
using UnityEngine;

public class regularDices : MonoBehaviour
{

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;
    //
    public int finalSide;
    // Use this for initialization
    private void Start()
    {

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Dices/regularDices/");
        Debug.Log(diceSides.Length);
    }

    // If you left click over the dice then RollTheDice coroutine is started
    public void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    public void OnflipDie()
    {
        StartCoroutine("FlipTheDie");
    }

    public IEnumerator FlipTheDie()
    {
        int otherSide = 7 - this.finalSide;
        this.finalSide = otherSide;
        this.rend.sprite = diceSides[otherSide - 1];

        yield return new WaitForSeconds(0.05f);
    }

    // Coroutine that rolls the dice
    public IEnumerator RollTheDice()
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
        yield return new WaitForSeconds(0.05f);
    }
    public int getFinalSide()
    {
        return finalSide;
    }

}
