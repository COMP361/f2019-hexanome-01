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
    protected Transform titleTransformGraphic;
    protected bool isLocked;
    protected int index;
    GameObject blockPanel;
    Button okBtn;


  protected virtual void OnEnable() {
    EventManager.InventoryUICellEnter += UpdateUIEnter;
    EventManager.blockOnInventoryClick += BlockOnClick;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUICellEnter -= UpdateUIEnter;
    EventManager.blockOnInventoryClick -= BlockOnClick;
  }


  void Start()
  {
    isLocked = false;
    titleTransformGraphic = transform.FindDeepChild("cellTitleGraphic");
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

      for(int i = 0; i < spots.Length; i++){
        if(i < cellInv.AllTokens.Count){
          if(cellInv.AllTokens[i] == null) continue;
          spots[i].AddItem(cellInv.AllTokens[i]);
        } else if(i < cellInv.items.Count + cellInv.AllTokens.Count){
          if(cellInv.items[i - cellInv.AllTokens.Count] == null) continue;
          spots[i].AddItem((Token) cellInv.items[i - cellInv.AllTokens.Count]);
        } else {
          spots[i].ClearSpot();
        }
      }

      titleTransformGraphic.GetComponent<Text>().text = "Cell: " + index;
      titleTransformGraphic.gameObject.SetActive(true);
    }
  }

  void BlockOnClick(){
    blockPanel.SetActive(true);
  }

  public void hideBlock(){
    blockPanel.SetActive(false);
  }

  public void ForceUpdate(CellInventory inv, int index){
    isLocked = false;
    UpdateUIEnter(inv,index);
  }
}
