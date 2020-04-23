using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wineskin : SmallToken
{
  public static string itemName = "Wineskin";
  public static string desc = "Each side of the wineskin can be used to advance 1 space without having to move the time marker.";
  public PhotonView photonView;

  public void OnEnable() {
    this.maxUse = 2;
    EventManager.ActionUpdate += FreeReservation;
    base.OnEnable();
  }

	public void OnDisable() {
    EventManager.ActionUpdate -= FreeReservation;
  }

  public static Wineskin Factory()
  {
    GameObject wineSkinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Wineskin", Vector3.zero, Quaternion.identity, 0);
    Wineskin wineskin = wineSkinGO.GetComponent<Wineskin>();
    wineskin.Cell = null;
    return wineskin;
  }

  public static Wineskin Factory(int cellID)
  {
    Wineskin wineskin = Wineskin.Factory();
    wineskin.Cell = Cell.FromId(cellID);
    return wineskin;
  }

  public static Wineskin Factory(string hero)
  {
    Wineskin wineskin = Wineskin.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(wineskin);
    return wineskin;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    EventManager.TriggerFreeMove(this);
  }

  public static string Type { get => typeof(Wineskin).ToString(); }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = 2;
    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
        Wineskin toAdd = Wineskin.Factory();
        if(hero.heroInventory.AddSmallToken(toAdd)){
          hero.heroInventory.RemoveGold(cost);
        } else{
          return;
        }

      }
      else{
        EventManager.TriggerError(0);
        return;
      }
    }
    else{
      EventManager.TriggerError(2);
      return;
    }
  }

  void FreeReservation(int action) {
    if(Action.FromValue<Action>(action) == Action.None) {
      reserved = 0;
    }
  }
}
