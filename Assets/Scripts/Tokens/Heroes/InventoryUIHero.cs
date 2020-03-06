using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIHero : Singleton<InventoryUIHero>
{
    // Start is called before the first frame update
    public Transform  itemsParent;
    protected InventorySpotCell[] spots;





  void Start()
  {

    /*descTransform = transform.FindDeepChild("cellDescription");
    titleTransform = transform.FindDeepChild("cellTitle");
    descTransform.gameObject.SetActive(false);
    titleTransform.gameObject.SetActive(false);
    */
    spots = itemsParent.GetComponentsInChildren<InventorySpotCell>();
  }

  void Update(){

/*
      if(Input.GetButtonDown("LockCellInventory")){
          isLocked = !isLocked;
          }
      if(Input.GetButtonDown("displayCellInv")){
          isText = !isText;
        }

        */
    }


    void UpdateUIEnter(CellInventory cellInv, int index){



      for(int i = 0; i < spots.Length; i++){
        if(i < cellInv.AllTokens.Count){
          spots[i].AddItem(cellInv.AllTokens[i]);
          Debug.Log("There is an item");
        }
        else{
         spots[i].ClearSpot();
         }
       }
     }


   void UpdateUIExit(){

     /*
     if(!isLocked){
     descTransform.gameObject.SetActive(false);
   } */
   }


}
