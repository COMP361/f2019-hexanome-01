using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class Well : Token
{
  private GameObject goFullWell;
  private GameObject goEmptyWell;
  public static string itemName = "Well";
  public static string desc = "This is a Well! Pick it up to get willpower points.";

  public static Well Factory(int cellID, bool full = true) {
    GameObject wellGO = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Tokens/Well")) as GameObject;
    Well well = wellGO.GetComponent<Well>();
    
    well.Cell = Cell.FromId(cellID);
    SpriteRenderer sr = well.GetComponent<SpriteRenderer>();
    if(sr != null) sr.enabled = false;

    if(well.Cell == null) return well;

    well.goFullWell = well.Cell.transform.Find("well/well-full").gameObject;
    well.goEmptyWell = well.Cell.transform.Find("well/well-empty").gameObject;  

    if(full) {
      well.DisplayWell();
    } else {
      well.DisplayWell(true);
      well.Cell.Inventory.RemoveToken(well);
    }

    return well;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public static string Type { get => typeof(Well).ToString(); }

  public void DestroyWell() {
    Cell.Inventory.RemoveToken(this);
    GameManager.instance.wells.Remove(this);
    HideWell();
    Destroy(gameObject);
  }

  public void EmptyWell(Hero hero) {
    if(Cell.Index == hero.Cell.Index){
      GameManager.instance.photonView.RPC("EmptyWellRPC", RpcTarget.AllViaServer, new object[] {Cell.Index, hero.TokenName});
    }
  }

  public void EmptyWell(string heroType) {
    DisplayWell(true);

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

    Cell.Inventory.RemoveToken(this);

    if(GameManager.instance.MainHero.TokenName.Equals(heroType)) {
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.MainHero.heroInventory);
    } else if(heroType.Equals(CharChoice.choice.TokenName)){
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.findHero(heroType).heroInventory);
    }
  }

  void DisplayWell(bool empty = false) {
    if(goFullWell == null || goEmptyWell == null) return;

    if(empty) {
      goFullWell.SetActive(false);
      goEmptyWell.SetActive(true);
    } else {
      goFullWell.SetActive(true);
      goEmptyWell.SetActive(false);
    }
  }

  void HideWell() {
    if(goFullWell == null || goEmptyWell == null) return;

    goFullWell.SetActive(false);
    goEmptyWell.SetActive(false);
  }

  public void ResetWell() {
    if(Cell.Inventory.Well == null) {
      DisplayWell();
      Cell.Inventory.AddToken(this);
    }
  }
}
