using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : Token
{
  public static string name = "Helm";
  public static string desc = "A helm allows you to total up all identical dice values in a battle.";
  public PhotonView photonView;

  public static Helm Factory()
  {
    GameObject helmGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Helm", Vector3.zero, Quaternion.identity, 0);
    return helmGO.GetComponent<Helm>();
  }

  public static Helm Factory(int cellID)
  {
      Helm helm = Helm.Factory();
      helm.Cell = Cell.FromId(cellID);
      return helm;
  }

  public static Helm Factory(string hero)
  {
    Helm helm = Helm.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(helm);
    return helm;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
    Debug.Log("Use Helm Cell");
  }

  public override void UseHero(){
    Debug.Log("Use helm Hero");
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
      Debug.Log("Use helm Effect");
}


  /*
  public void onEnable(){
    object[] data = photonView.InstantiationData;
    if(data == null){
      return;
    }
    this.Cell = Cell.FromId((int)data[0]);
  }*/


    public static string Type { get => typeof(Helm).ToString(); }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;

        if(hero.heroInventory.numOfGold >= cost) {
          Helm toAdd = Helm.Factory();
          if(hero.heroInventory.AddHelm(toAdd)){
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
  }
