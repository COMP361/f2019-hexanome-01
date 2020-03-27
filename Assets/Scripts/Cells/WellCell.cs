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
        Inventory.RemoveToken(well);
        photonView.RPC("EmptyWellRPC", RpcTarget.AllViaServer, new object[] {this.Index, GameManager.instance.MainHero.TokenName});
      }

    }

    [PunRPC]
    public void EmptyWellRPC(int cellIndex, string heroType){

      if(this.Index == cellIndex){
        isEmptied = true;
        goFullWell.SetActive(false);
        goEmptyWell.SetActive(true);
        well = null;
      }

      foreach(Hero hero in GameManager.instance.heroes){
        if(hero.TokenName.Equals(heroType)){
          int currWP = hero.Willpower;
          if(hero.TokenName.Equals("Warrior")){
            currWP = currWP + 5;
          }
          else{
          currWP = currWP + 3;
          }
          hero.Willpower = currWP;

        }

        if(GameManager.instance.MainHero.TokenName.Equals(heroType)){
          EventManager.TriggerUpdateHeroStats(hero);
        }
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
