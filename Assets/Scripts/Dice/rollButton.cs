using System.Collections;

public class roolButton : MonoBehaviour
{

    //public static GameObject regularDice1, regularDice2, regularDice3, regularDice4;
    //public static GameObject specialDice1, specialDice2;
    regularDices regularDice1 = new regularDices();
    regularDices regularDice2 = new regularDices();
    regularDices regularDice3 = new regularDices();
    regularDices regularDice4 = new regularDices();
    specialDices specialDice1 = new specialDices();
    specialDices specialDice2 = new specialDices();
    public static bool hasSpecial;
    public static int numRegularDices;
    public static int numSpecialDices;
    //store dice objects in arraylsit
    ArrayList rdList = new ArrayList();
    ArrayList sdList = new ArrayList();

    void start()
    {

        regularDice1 = GameObject.Find("rd1");
        if (regularDice1 != null)
        {
            regularDice1.GetComponent<regularDices>();
        }

        regularDice2 = GameObject.Find("rd2");
        if (regularDice2 != null)
        {
            regularDice2.GetComponent<regularDices>();
        }

        regularDice3 = GameObject.Find("rd3");
        if (regularDice3 != null)
        {
            regularDice3.GetComponent<regularDices>();
        }

        regularDice4 = GameObject.Find("rd4");
        if (regularDice4 != null)
        {
            regularDice4.GetComponent<regularDices>();
        }

        specialDice1 = GameObject.Find("sd1");
        if (specialDice1 != null)
        {
            specialDice1.GetComponent<regularDices>();
        }

        specialDice2 = GameObject.Find("sd2");
        if (specialDice2 != null)
        {
            specialDice2.GetComponent<regularDices>();
        }


    }


    private void OnMouseDown()
    {
        StartCoroutine("activatedAllDices");
    }

    public static int activatedAllDices()
    {


        switch (numRegularDices)
        {
            case 1:
                regularDice1.RollTheDice();
                ArrayList List = new ArrayList();
                List.add(regularDice1);
                int max = getMaxResults(List);
                if (hasSpecial)
                {
                    int smax = actSpecialDice();
                    if (max > smax)
                    {
                        return max;
                    }
                    else
                    {
                        return smax;
                    }
                }
                else
                {

                    return max;
                }


            case 2:
                regularDice1.RollTheDice();
                regularDice2.RollTheDice();
                ArrayList List = new ArrayList();
                List.add(regularDice1);
                List.add(regularDice2);
                int max = getMaxResults(List);
                if (hasSpecial)
                {
                    actSpecialDice();
                }
                else
                {
                    break;
                }
            case 3:
                regularDice1.RollTheDice();
                regularDice2.RollTheDice();
                regularDice3.RollTheDice();
                ArrayList List = new ArrayList();
                List.add(regularDice1);
                List.add(regularDice2);
                List.add(regularDice3);
                int max = getMaxResults(List);
                if (hasSpecial)
                {
                    int smax = actSpecialDice();
                    if (max > smax)
                    {
                        return max;
                    }
                    else
                    {
                        return smax;
                    }
                }
                else
                {

                    return max;
                }
            case 4:
                regularDice1.RollTheDice();
                regularDice2.RollTheDice();
                regularDice3.RollTheDice();
                regularDice4.RollTheDice();
                ArrayList List = new ArrayList();
                List.add(regularDice1);
                List.add(regularDice2);
                List.add(regularDice3);
                List.add(regularDice4);
                int max = getMaxResults(List);
                if (hasSpecial)
                {
                    int smax = actSpecialDice();
                    if (max > smax)
                    {
                        return max;
                    }
                    else
                    {
                        return smax;
                    }
                }
                else
                {

                    return max;
                }
        }
    }

    public int actSpecialDice()
    {
        switch (numSpecialDices)
        {
            case 1:
                specialDice1.RollTheDice();
                return specialDice1.getFinalSide();
                break;

            case 2:
                specialDice1.RollTheDice();
                specialDice2.RollTheDice();
                ArrayList sList = new ArrayList();
                sList.add(specialDice1);
                sList.add(specialDice2);
                int smax = getMaxResults(sList);
                return smax;
                break;
        }
    }

    public static int getMaxResults(ArrayList nList)
    {
        int maxValue = 0;
        int[] diceValues;
        foreach (int dice in nList)
        {
            if (dice.getFinalSide() > maxValue)
            {
                maxValue = dice.getFinalSide();
            }

        }
        return maxValue;
    }

}
