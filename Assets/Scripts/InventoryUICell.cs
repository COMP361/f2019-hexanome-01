using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUICell : Singleton<InventoryUICell>
{
    // Start is called before the first frame update
    public Transform  itemsParent;
    public GameObject inventoryUICell;
    InventorySpot[] spots;

    protected string description;
    protected Transform textTransform;
    protected bool isText;
    protected bool isLocked;


  protected virtual void OnEnable() {
    EventManager.InventoryUICellEnter += UpdateUIEnter;
    EventManager.InventoryUICellExit += UpdateUIExit;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUICellEnter -= UpdateUIEnter;
    EventManager.InventoryUICellExit += UpdateUIExit;
  }


  void Start()
  {
    isText = true;
    isLocked = false;
    textTransform = transform.Find("cellsDescription");
    textTransform.gameObject.SetActive(false);
    spots = itemsParent.GetComponentsInChildren<InventorySpot>();
  }

  void Update(){

//     if(Input.GetButtonDown("Inventory")){
//    inventoryUI.SetActive(!inventoryUI.activeSelf);
//     }
      if(Input.GetButtonDown("LockCellInventory")){
          isLocked = !isLocked;
          }
    }


    void UpdateUIEnter(CellInventory cellInv){
      if(!isLocked){
      if(isText){
        formatDescription(cellInv);
        textTransform.GetComponent<Text>().text = description;
        textTransform.gameObject.SetActive(true);
      }

      else{
      for(int i = 0; i < spots.Length; i++){
        if(i < cellInv.items.Count){
          spots[i].AddItem(cellInv.items[i]);
        }
        else{
         spots[i].ClearSpot();
         }
       }
     }
   }
   }

   void UpdateUIExit(){
     if(!isLocked){
     textTransform.gameObject.SetActive(false);
   }
   }


   public virtual void formatDescription(CellInventory cellInv) {


    this.description = "Heroes: \n";
    foreach (var hero in cellInv.Heroes) {
       this.description += "  - " + hero.TokenName + " \n";
     }

     this.description += "Monster: \n";
     foreach (var enemy in cellInv.Enemies) {
       this.description += "  - " + enemy.TokenName + " \n";
     }

     this.description += "Farmers: \n";
     foreach (var farmer in cellInv.Farmers) {
       this.description += "  - " + farmer.TokenName + " \n";
     }

     this.description += "Item: \n";
     foreach (var token in cellInv.Tokens) {
       this.description += "  - " + token.TokenName + " \n";
     }

     this.description = description + "Gold: \n";
     foreach (var gold in cellInv.Golds) {
       this.description += "  - " + gold.TokenName + " \n";
     }
   }
}
