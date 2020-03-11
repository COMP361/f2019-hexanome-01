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

    protected string title;

    protected Transform titleTransformGraphic;
    protected bool isLocked;
    protected int index;
    GameObject blockPanel;
    Button okBtn;


  protected virtual void OnEnable() {
    EventManager.InventoryUICellEnter += UpdateUIEnter;
    EventManager.InventoryUICellExit += UpdateUIExit;
    EventManager.blockOnInventoryClick += BlockOnClick;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUICellEnter -= UpdateUIEnter;
    EventManager.InventoryUICellExit -= UpdateUIExit;
    EventManager.blockOnInventoryClick -= BlockOnClick;
  }


  void Start()
  {
    isLocked = false;
  //  descTransform = transform.FindDeepChild("cellDescription");
  //  titleTransformText = transform.FindDeepChild("cellTitleText");
    titleTransformGraphic = transform.FindDeepChild("cellTitleGraphic");
    //descTransform.gameObject.SetActive(false);
  //  titleTransformText.gameObject.SetActive(false);
    titleTransformGraphic.gameObject.SetActive(false);
    spots = itemsParent.GetComponentsInChildren<InventorySpotCell>();
    blockPanel = transform.Find("Block").gameObject;
    okBtn = blockPanel.transform.Find("OK Button").GetComponent<Button>();
  }

  void Update(){
      if(Input.GetButtonDown("LockCellInventory")){
          isLocked = !isLocked;
          }
    }

    void UpdateUIEnter(CellInventory cellInv, int index){
        if(!isLocked){
          this.index = index;
          InventorySpotCell.cellIndex = index;
      //    formatDescription(cellInv);
          formatTitle(index);
      //    descTransform.GetComponent<Text>().text = description;
      //    descTransform.gameObject.SetActive(true);
        //  titleTransformText.GetComponent<Text>().text = title;
        //  titleTransformText.gameObject.SetActive(true);
          for(int i = 0; i < spots.Length; i++){
          if(i < cellInv.AllTokens.Count){
            spots[i].AddItem(cellInv.AllTokens[i]);
          }
          else{
           spots[i].ClearSpot();
           }
         formatTitle(index);
         titleTransformGraphic.GetComponent<Text>().text = title;
         titleTransformGraphic.gameObject.SetActive(true);
        }
      }
    }

   void UpdateUIExit(){
     if(!isLocked){
  //   descTransform.gameObject.SetActive(false);
    }
   }

   void BlockOnClick(){
        blockPanel.SetActive(true);
   }

   public void hideBlock(){
        blockPanel.SetActive(false);
   }
/*
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
   */

   public virtual void formatTitle(int index){
        title = "Cell: " + index;
   }

   public void ForceUpdate(CellInventory inv, int index){
     isLocked = false;
     UpdateUIEnter(inv,index);
   }
 }
