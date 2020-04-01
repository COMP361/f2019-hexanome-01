using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WellCell : Cell {
  public GameObject goFullWell;
  public GameObject goEmptyWell;
  bool isEmptied = false;
  bool isDestroyed = false;
  public Token well;
  public PhotonView photonView;

  void OnEnable() {
    EventManager.pickWellClick += EmptyWell;
    base.OnEnable();
  }

  void OnDisable() {
    EventManager.pickWellClick -= EmptyWell;
  }

  protected virtual void Awake() {
    base.Awake();
  }

  protected virtual void Start() {
    base.Start();
  }

  public void DestroyWell() {
    isDestroyed = true;
    Inventory.RemoveToken(well);
    goFullWell.SetActive(false);
    goEmptyWell.SetActive(false);
    well = null;
  }

  public void EmptyWell(Hero hero, Well well) {
    if(isDestroyed) return;

    if(Index == hero.Cell.Index){
      Inventory.RemoveToken(well);
      photonView.RPC("EmptyWellRPC", RpcTarget.AllViaServer, new object[] {this.Index, GameManager.instance.MainHero.TokenName});
    }
  }

  [PunRPC]
  public void EmptyWellRPC(int cellIndex, string heroType) {
    if(isDestroyed) return;

    if(this.Index == cellIndex){
      isEmptied = true;
      goFullWell.SetActive(false);
      goEmptyWell.SetActive(true);
      well = null;
    }

    foreach(Hero hero in GameManager.instance.heroes) {
      if(hero.TokenName.Equals(heroType)){
        int currWP = hero.Willpower;
        if(hero.TokenName.Equals("Warrior")){
          currWP = currWP + 5;
        } else {
          currWP = currWP + 3;
        }

        hero.Willpower = currWP;
      }
    }
    if(GameManager.instance.MainHero.TokenName.Equals(heroType)) {
  //    Debug.Log("WHAT IS HAPPENING: " + GameManager.instance.MainHero);
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.MainHero.heroInventory);
    }
    else if(heroType.Equals(CharChoice.choice.TokenName)){
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.findHero(heroType).heroInventory);
    }
  }

  public void ResetWell() {
    if(isDestroyed) return;

    isEmptied = false;
    goFullWell.SetActive(true);
    goEmptyWell.SetActive(false);
    well = Well.Factory();
    Inventory.AddToken(well);
  }
}
