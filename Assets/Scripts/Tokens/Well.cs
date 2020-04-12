using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class Well : Token
{
  public GameObject goFullWell;
  public GameObject goEmptyWell;

  public PhotonView photonView;
  public static string itemName = "Well";
  public static string desc = "This is a Well! Pick it up to get willpower points.";

  public static Well Factory(int cellID, bool full = true) {
    GameObject wellGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Well", Vector3.zero, Quaternion.identity, 0);
    Well well = wellGO.GetComponent<Well>();

    well.Cell = Cell.FromId(cellID);
    well.goFullWell = well.Cell.transform.Find("well/well-full").gameObject;
    well.goEmptyWell = well.Cell.transform.Find("well/well-empty").gameObject;
    if(well.goFullWell == null || well.goEmptyWell == null) return null;

    if(full) {
      well.goFullWell.SetActive(true);
      well.goEmptyWell.SetActive(false);
    } else {
      well.goFullWell.SetActive(false);
      well.goEmptyWell.SetActive(true);
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
    goFullWell.SetActive(false);
    goEmptyWell.SetActive(false);
    Destroy(gameObject);
  }

  public void EmptyWell(Hero hero) {
    if(Cell.Index == hero.Cell.Index){
      Cell.Inventory.RemoveToken(this);
      photonView.RPC("EmptyWellRPC", RpcTarget.AllViaServer, new object[] {Cell.Index, GameManager.instance.MainHero.TokenName});
    }
  }

  [PunRPC]
  public void EmptyWellRPC(int cellIndex, string heroType) {
    goFullWell.SetActive(false);
    goEmptyWell.SetActive(true);

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
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.MainHero.heroInventory);
    } else if(heroType.Equals(CharChoice.choice.TokenName)){
      EventManager.TriggerInventoryUIHeroPeak(GameManager.instance.findHero(heroType).heroInventory);
    }
  }

  public void ResetWell() {
    if(Cell.Inventory.Well == null) {
      goFullWell.SetActive(true);
      goEmptyWell.SetActive(false);
      Cell.Inventory.AddToken(this);
    }
  }
}
