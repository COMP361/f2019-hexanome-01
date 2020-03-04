﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nroll : MonoBehaviour
{
    public regularDices regularDice1;
    public regularDices regularDice2;
    public regularDices regularDice3;
    public regularDices regularDice4;
    public specialDices specialDice1;
    public specialDices specialDice2;
    public static bool hasSpecial;
    public static int numRegularDices;
    public static int numSpecialDices;


    // Start is called before the first frame update
    void Start()
    {
        regularDice1 = GameObject.FindGameObjectWithTag("rd1").GetComponent<regularDices>();
        regularDice2 = GameObject.FindGameObjectWithTag("rd2").GetComponent<regularDices>();
        regularDice3 = GameObject.FindGameObjectWithTag("rd3").GetComponent<regularDices>();
        regularDice4 = GameObject.FindGameObjectWithTag("rd4").GetComponent<regularDices>();
        specialDice1 = GameObject.FindGameObjectWithTag("s1").GetComponent<specialDices>();
        specialDice2 = GameObject.FindGameObjectWithTag("s2").GetComponent<specialDices>();
    }

    // Update is called once per frame
    void Update()
    {
        regularDice1.RollTheDice();
    }
    private void OnMouseDown()
    {
        StartCoroutine("activatedAllDices");
    }

    public int activatedAllDices()
    {
        if (numRegularDices == 1)
        {
            
            regularDice1.RollTheDice();
            int rmax1 = regularDice1.getFinalSide();
            int smax1 = 0;
            if (hasSpecial == true)
            {
                smax1 = activeSpecialDice();
            }
            return getBigger(rmax1,smax1);
        }
        else if(numRegularDices == 2)
        {

            regularDice1.RollTheDice();
            regularDice2.RollTheDice();
            regularDices[] r2List = new regularDices[2];
            r2List[0] = regularDice1;
            r2List[1] = regularDice2;
            int rmax2 = getMaxValue(r2List);
            int smax2 = 0;
            if (hasSpecial == true)
            {
                smax2 = activeSpecialDice();
            }
            return getBigger(rmax2,smax2);
        }
        else if(numRegularDices == 3)
        {
            regularDice1.RollTheDice();
            regularDice2.RollTheDice();
            regularDice3.RollTheDice();
            regularDices[] r3List = new regularDices[3];
            r3List[0] = regularDice1;
            r3List[1] = regularDice2;
            r3List[2] = regularDice3;
            int rmax3 = getMaxValue(r3List);
            int smax3 = 0;
            if (hasSpecial == true)
            {
                smax3 = activeSpecialDice();
            }
            return getBigger(rmax3, smax3) ;
        }
        else if(numRegularDices == 4)
        {
            regularDice1.RollTheDice();
            regularDice2.RollTheDice();
            regularDice3.RollTheDice();
            regularDices[] r4List = new regularDices[4];
            r4List[0] = regularDice1;
            r4List[1] = regularDice2;
            r4List[2] = regularDice3;
            r4List[3] = regularDice4;
            int rmax4 = getMaxValue(r4List);
            int smax4 = 0;
            if (hasSpecial == true)
            {
                smax4 = activeSpecialDice();
            }
            return getBigger(rmax4,smax4);
        }
        else
        {
            return 0;
            Debug.Log("number of regular dice wrong");
           
        }
    }


    public int activeSpecialDice()
    {
        if (numSpecialDices == 1)
        {
            specialDice1.RollTheDice();
            return specialDice1.getFinalSide();
        }
        else if (numSpecialDices == 2)
        {
            specialDice1.RollTheDice();
            specialDice2.RollTheDice();
            int max1 = specialDice1.getFinalSide();
            int max2 = specialDice2.getFinalSide();
            if (max1 > max2)
            {
                return max1;
            }
            else
            {
                return max2;
            }

        }
        else
        {
            return 0;
            Debug.Log("number of special dice wrong");
        }
    }


    public int getMaxValue( regularDices[] rdList)
    {
        int max = 0;
        foreach (regularDices dice in rdList)
        {
            int cur = dice.getFinalSide();
            if (cur > max)
            {
                max = cur;
            }
        }
        return max;
    }
    
    public int getBigger(int rr, int ss)
    {
        if (rr > ss)
        {
            return rr;
        }
        else
        {
            return ss;
        }
    }
}