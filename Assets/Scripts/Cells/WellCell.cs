using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellCell : Cell
{
    public GameObject goFullWell;
    public GameObject goEmptyWell;
    bool isEmptied = false;
    public Token well;

    public void emptyWell(Hero hero)
    {
        int currWP = hero.State.getWP();
        currWP++;
        hero.State.setWP(currWP);

        isEmptied = true;
        goFullWell.SetActive(false);
        goEmptyWell.SetActive(true);
        Inventory.RemoveToken(well);
        well = null;


    }

    public void resetWell()
    {
        isEmptied = false;
        goFullWell.SetActive(true);
        goEmptyWell.SetActive(false);
        well = Well.Factory();
        Inventory.addToken(well);
        Debug.Log("SuckMyDick");

    }

}
