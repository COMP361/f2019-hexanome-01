using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUICell : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform  itemsParent;
    public GameObject inventoryUICell;
    InventorySpot[] spots;

    public static InventoryUICell instance;

    void Awake(){
      if(instance != null){
        Debug.LogWarning("More than one instance of Inventory Found!");
        return;
      }
      instance = this;
    }

    void Start()
    {

      EventManager.InventoryUICell += UpdateUI;
      spots = itemsParent.GetComponentsInChildren<InventorySpot>();
    }

    void OnEnable() {
    }

    void Update(){

//      if(Input.GetButtonDown("Inventory")){
//    inventoryUI.SetActive(!inventoryUI.activeSelf);
//      }

    }

    void OnDisable() {
      EventManager.InventoryUICell -= UpdateUI;
    }

     void UpdateUI(CellInventory cellInventory){
       Debug.Log("Updating UI");

       for(int i = 0; i < spots.Length; i++){
         if(i < cellInventory.items.Count){
           spots[i].AddItem(cellInventory.items[i]);
         }
         else{
           spots[i].ClearSpot();
         }
              }
     }

}
