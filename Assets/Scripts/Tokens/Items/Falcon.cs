using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : BigToken {

  public static string itemName = "Falcon";
  public static string desc = "Two heroes can exchange as many small articles, gold, or gemstones at one time as they like even if they are not standing on the same space.";
  public PhotonView photonView;
  public bool isTurned;
  Sprite spriteFront, spriteBack;

  void OnEnable() {
    this.isTurned = false;
    this.spriteBack = Resources.Load<Sprite>("Sprites/Tokens/FalconBack");
    this.spriteFront = Resources.Load<Sprite>("Sprites/Tokens/Falcon");
    EventManager.StartDay += TurnFalconNoRPC;
    base.OnEnable();
  }

  void OnDisable() {
    EventManager.StartDay -= TurnFalconNoRPC;
  }

  public static Falcon Factory()
  {
    GameObject falconGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Falcon", Vector3.zero, Quaternion.identity, 0);
    Falcon falcon = falconGO.GetComponent<Falcon>();


    return  falcon;
  }

  public static Falcon Factory(int cellID)
  {
    object[] myCustomInitData = {cellID};
    GameObject falconGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Falcon", Vector3.zero, Quaternion.identity, 0, myCustomInitData);
    Falcon falcon = falconGO.GetComponent<Falcon>();
    falcon.Cell = Cell.FromId(cellID);
    return falconGO.GetComponent<Falcon>();
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    EventManager.TriggerFalconUseUI(this);
  }

  [PunRPC]
  public void TurnFalconRPC(int viewID){
    if(!isTurned){
      isTurned = true;
      gameObject.GetComponent<SpriteRenderer>().sprite = spriteBack;
    } else {
      isTurned = false;
      gameObject.GetComponent<SpriteRenderer>().sprite = spriteFront;
    }
  }

  public void TurnFalconNoRPC(){
    if(!isTurned) return;
    int viewID = GetComponent<PhotonView>().ViewID;
    TurnFalconRPC(viewID);

    foreach (Hero hero in GameManager.instance.heroes) {
      if(hero.heroInventory.bigToken == this) {
        EventManager.TriggerInventoryUIHeroUpdate(hero.heroInventory);
        break;
      }
    }
  }

  public void TurnFalcon(){
    int viewID = GetComponent<PhotonView>().ViewID;
    photonView.RPC("TurnFalconRPC", RpcTarget.AllViaServer, new object[] {viewID});
  }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = 2;

    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
        Falcon toAdd = Falcon.Factory();
        if(hero.heroInventory.AddBigToken(toAdd)){
          hero.heroInventory.RemoveGold(cost);
        }
        else{
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
}
