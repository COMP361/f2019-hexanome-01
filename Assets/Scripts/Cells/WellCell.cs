using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellCell : Cell
{
    public GameObject goFullWell;
    public GameObject goEmptyWell;
    bool isEmptied = false;
    public Token well;

    void OnEnable() {
        EventManager.pickWellClick += emptyWell;
        base.OnEnable();
    }

    void OnDisable() {
        EventManager.pickWellClick -= emptyWell;
        base.OnDisable();
    }

    public void emptyWell(Hero hero)
    {
        int currWP = hero.State.Willpower;
        currWP = currWP + 3;
        hero.State.Willpower = currWP;

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
        Debug.Log("WELLLLL "+ Index);
        well = Well.Factory(Index);
        Inventory.addToken(well);
    }

}
