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
      protected virtual void Awake() {
          base.Awake();
      }

      protected virtual void Start() {
          base.Start();
          Debug.Log("child start");
      }



    public void emptyWell(Hero hero, Well well)
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
        well = null;
      }
    }

    public void resetWell()
    {
        Debug.Log("My index is: " + Index);
        isEmptied = false;
        goFullWell.SetActive(true);
        goEmptyWell.SetActive(false);
        well = Well.Factory();
        this.Inventory.addToken(well);
    }

}
