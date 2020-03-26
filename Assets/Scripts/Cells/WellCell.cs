using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WellCell : Cell
{
    public GameObject goFullWell;
    public GameObject goEmptyWell;
    bool isEmptied = false;
    public Token well;
    public PhotonView photonView;

    void OnEnable() {
        EventManager.pickWellClick += emptyWell;
        base.OnEnable();
    }

    void OnDisable() {
        EventManager.pickWellClick -= emptyWell;

      }
      protected virtual void Awake() {
          base.Awake();
      }

      protected virtual void Start() {
          base.Start();
      }



    public void emptyWell(Hero hero, Well well)
    {
      if(Index == hero.Cell.Index){
        int currWP = hero.Willpower;
        if(hero.TokenName.Equals("Warrior")){
          currWP = currWP + 5;
        }
        else{
        currWP = currWP + 3;
        }
        hero.Willpower = currWP;
        Debug.Log("Hero pick well: " + hero.TokenName + " WILLPOWER: " + hero.Willpower);
        EventManager.TriggerCurrentPlayerUpdate(hero);
    //    isEmptied = true;
    //    goFullWell.SetActive(false);
    //    goEmptyWell.SetActive(true);
        Inventory.RemoveToken(well);
    //    well = null;
        photonView.RPC("EmptyWellRPC", RpcTarget.AllViaServer, new object[] {this.Index});

      }
    }

    [PunRPC]
    public void EmptyWellRPC(int cellIndex){
      if(this.Index == cellIndex){
        isEmptied = true;
        goFullWell.SetActive(false);
        goEmptyWell.SetActive(true);
        well = null;
      }

    }


    public void resetWell()
    {

        isEmptied = false;
        goFullWell.SetActive(true);
        goEmptyWell.SetActive(false);
        well = Well.Factory();
        this.Inventory.AddToken(well);
    }

}
