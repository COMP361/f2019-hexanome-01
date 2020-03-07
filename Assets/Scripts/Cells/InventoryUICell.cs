using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUICell : Singleton<InventoryUICell>
{
    // Start is called before the first frame update
    public Transform  itemsParent;
    protected InventorySpotCell[] spots;

    protected string description;
    protected string title;
    protected Transform descTransform;
    protected Transform titleTransformText;
    protected Transform titleTransformGraphic;
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
    descTransform = transform.FindDeepChild("cellDescription");
    titleTransformText = transform.FindDeepChild("cellTitleText");
    titleTransformGraphic = transform.FindDeepChild("cellTitleGraphic");

    descTransform.gameObject.SetActive(false);
    titleTransformText.gameObject.SetActive(false);
    titleTransformGraphic.gameObject.SetActive(false);
    spots = itemsParent.GetComponentsInChildren<InventorySpotCell>();
  }

  void Update(){

//     if(Input.GetButtonDown("Inventory")){
//    inventoryUI.SetActive(!inventoryUI.activeSelf);
//     }
      if(Input.GetButtonDown("LockCellInventory")){
          isLocked = !isLocked;
          }
      if(Input.GetButtonDown("displayCellInv")){
          isText = !isText;
        }
    }


    void UpdateUIEnter(CellInventory cellInv, int index){
      if(!isLocked){
      if(isText){
        formatDescription(cellInv);
        formatTitle(index);
        descTransform.GetComponent<Text>().text = description;
        descTransform.gameObject.SetActive(true);
        titleTransformText.GetComponent<Text>().text = title;
        titleTransformText.gameObject.SetActive(true);
      } else {
      for(int i = 0; i < spots.Length; i++){
        if(i < cellInv.AllTokens.Count){
          spots[i].AddItem(cellInv.AllTokens[i]);
        }
        else{
         spots[i].ClearSpot();
         }
       }
       formatTitle(index);
       titleTransformGraphic.GetComponent<Text>().text = title;
       titleTransformGraphic.gameObject.SetActive(true);
     }
   }
   }

   void UpdateUIExit(){
     if(!isLocked){
     descTransform.gameObject.SetActive(false);
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

   public virtual void formatTitle(int index){
        title = "Cell: " + index;
   }
}
