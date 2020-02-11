using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roolButton : MonoBehaviour
{

    private int numberOfDices;
    public static GameObject regularDice1, regularDice2, regularDice3, regularDice4;
    public static GameObject specialDice1, specialDice2;
    public bool hasSpecial;
    public int numRegularDices;
    public int numSpecialDices;
    private int whosTurn = 1;
    //store dice objects in arraylsit
    ArrayList rdList = new ArrayList();
    ArrayList sdList = new ArrayList();
    //store values get by dices in a array
    private int[] DiceValue;
    private int maxValue;

    void start()
    {
        regularDice1 = GameObject.Find("rd1");
        rdList.Add(regularDice1);
        regularDice2 = GameObject.Find("rd2");
        rdList.Add(regularDice2);
        regularDice3 = GameObject.Find("rd3");
        rdList.Add(regularDice3);
        regularDice4 = GameObject.Find("rd4");
        rdList.Add(regularDice4);
        specialDice1 = GameObject.Find("sd1");
        sdList.Add(specialDice1);
        specialDice2 = GameObject.Find("sd2");
        sdList.Add(specialDice2);


    }


    private void OnMouseDown()
    {
        StartCoroutine("activatedAllDices");
    }

    public static void activatedAllDices()
    {

        //switch (whosTurn)
        //{
        //    case 1:
        //}
            
            
            //switch (numRegularDices)
            //{
            //    case 1:
            //        regularDice1.GetComponent<regularDices>().RollTheDice();
            //        if (hasSpecial)
            //        {
            //            actSpecialDice();
            //        }
            //        else
            //        {
            //            break;
            //        }

            //    case 2:
            //        regularDice1.GetComponent<regularDices>().RollTheDice();
            //        regularDice2.GetComponent<regularDices>().RollTheDice();
            //        if (hasSpecial)
            //        {
            //            actSpecialDice();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    case 3:
            //        regularDice1.GetComponent<regularDices>().RollTheDice();
            //        regularDice2.GetComponent<regularDices>().RollTheDice();
            //        regularDice3.GetComponent<regularDices>().RollTheDice();
            //        if (hasSpecial)
            //        {
            //            actSpecialDice();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    case 4:
            //        regularDice1.GetComponent<regularDices>().RollTheDice();
            //        regularDice2.GetComponent<regularDices>().RollTheDice();
            //        regularDice3.GetComponent<regularDices>().RollTheDice();
            //        regularDice4.GetComponent<regularDices>().RollTheDice();
            //        if (hasSpecial)
            //        {
            //            actSpecialDice();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //}
    }

    public void actSpecialDice()
    {
        switch (numSpecialDices)
        {
            case 1:
                specialDice1.GetComponent<specialDices>().RollTheDice();
                break;

            case 2:
                specialDice1.GetComponent<specialDices>().RollTheDice();
                specialDice2.GetComponent<specialDices>().RollTheDice();
                break;
        }
    }

}
