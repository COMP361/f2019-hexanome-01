using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : SmallToken
{
  public static string itemName = "Telescope";
  public static string desc = "The telescope can be used to d reveal all hidden tokens on adjacent spaces.";

  public PhotonView photonView;

  public List<Pair<Token, int>> itemsToCheck;


  public static Telescope Factory()
  {
    GameObject telescopeGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Telescope", Vector3.zero, Quaternion.identity, 0);
    Telescope telescope = telescopeGO.GetComponent<Telescope>();
    telescope.Cell = null;
    return telescope;
  }

  public static Telescope Factory(int cellID)
  {
    Telescope telescope = Telescope.Factory();
    telescope.Cell = Cell.FromId(cellID);
    return telescope;
  }

  public static Telescope Factory(string hero)
  {
    Telescope telescope = Telescope.Factory();
    GameManager.instance.findHero(hero).heroInventory.AddItem(telescope);
    return telescope;
  }

  public void OnEnable(){
    itemsToCheck = new List<Pair<Token, int>>();
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    Hero hero = GameManager.instance.MainHero;
    List<Transform> cellsToCheck = hero.Cell.neighbours;
    foreach(Transform toCheck in cellsToCheck){
      foreach(Token item in toCheck.GetComponent<Cell>().Inventory.AllTokens){
        if(item is Fog){
           itemsToCheck.Add(new Pair<Token, int>(item, toCheck.GetComponent<Cell>().Index));
          }
        }
        foreach(DictionaryEntry item in toCheck.GetComponent<Cell>().Inventory.items){
          if( item.Value is Runestone){
            if(((Runestone)item.Value).isCovered){
              itemsToCheck.Add(new Pair<Token,int>(((Runestone)item.Value), toCheck.GetComponent<Cell>().Index));
            }
          }
        }
      }
    instantiateUsePanels();
  }

  public void instantiateUsePanels(){
    if(itemsToCheck.Count != 0){
      EventManager.TriggerTelescopeUseUI(this, itemsToCheck[0]);
      itemsToCheck.RemoveAt(0);
    }
    else{
      GameManager.instance.MainHero.heroInventory.RemoveSmallToken(this);
    }


  }

  public static void Buy() {
    Hero hero = GameManager.instance.MainHero;
    int cost = 2;
    if(hero.timeline.Index != 0){
      if(hero.heroInventory.numOfGold >= cost) {
        Telescope toAdd = Telescope.Factory();
        if(hero.heroInventory.AddSmallToken(toAdd)){
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
