using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIHero : Singleton<InventoryUIHero>
{
    // Start is called before the first frame update

    public Transform  smallItemsParent;
    public Transform bigParent;
    public Transform  helmParent;
    public Transform goldParent;
    protected InventorySpotCell[] smallSpots;
    protected InventorySpotCell bigSpot;
    protected InventorySpotCell helmSpot;
    protected InventorySpotCell goldSpot;

    protected Transform goldText;




  void Start()
  {
    smallSpots = smallItemsParent.GetComponentsInChildren<InventorySpotCell>();
    bigSpot = bigParent.GetComponentInChildren<InventorySpotCell>();
    helmSpot = helmParent.GetComponentInChildren<InventorySpotCell>();
    goldSpot = goldParent.GetComponentInChildren<InventorySpotCell>();

    goldText = transform.FindDeepChild("GoldText");
  }


  protected virtual void OnEnable() {
    EventManager.InventoryUIHeroUpdate += UpdateUI;
  }

  protected virtual void OnDisable() {
    EventManager.InventoryUIHeroUpdate -= UpdateUI;
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


    void UpdateUI(HeroInventory heroInv){
/*
      for(int i = 0; i < smallSpots.Length; i++){
        if(i < cellInv.AllTokens.Count){
          smallSpots[i].AddItem(cellInv.AllTokens[i]);
          Debug.Log("There is an item");
        }
        else{
         smallSpots[i].ClearSpot();
         }
       }
       */
     }


}
