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

      }

    void OnDisable() {
        EventManager.pickWellClick -= emptyWell;

      }

    public void emptyWell(Hero hero)
    {
      if(Index == hero.Cell.Index){
        int currWP = hero.State.getWP();
        currWP = currWP + 3;
        hero.State.setWP(currWP);
        isEmptied = true;
        goFullWell.SetActive(false);
        goEmptyWell.SetActive(true);
        Inventory.RemoveToken(well);
        InventoryUICell.instance.ForceUpdate(Inventory, Index);
        Debug.Log(hero.State.getWP());
        well = null;

      }
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
