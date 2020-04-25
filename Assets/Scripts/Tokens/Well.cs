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

    if(hero.TokenName.Equals("Warrior")){
      hero.setWP((hero.Willpower + 5));
    }
    else{
      hero.setWP((hero.Willpower + 3));
    }
  }

  public void EmptyWell(string heroType) {
    DisplayWell(true);
    if (PhotonNetwork.IsMasterClient){
    Cell.Inventory.RemoveToken(this);
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
