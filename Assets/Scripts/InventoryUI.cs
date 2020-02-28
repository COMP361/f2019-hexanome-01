using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform  itemsParent;
    public GameObject inventoryUI;
    InventorySpot[] spots;

    void Start()
    {

      EventManager.InventoryUI += UpdateUI;
      spots = itemsParent.GetComponentsInChildren<InventorySpot>();
    }

    void OnEnable() { 
    }

    void Update(){
      if(Input.GetButtonDown("Inventory")){
        inventoryUI.SetActive(!inventoryUI.activeSelf);
      }

    }

    void OnDisable() {
      EventManager.InventoryUI -= UpdateUI;
    }

     void UpdateUI(Inventory inventory){
       Debug.Log("Updating UI");

       for(int i = 0; i < spots.Length; i++){
         if(i < inventory.items.Count){
           spots[i].AddItem(inventory.items[i]);
         }
         else{
           spots[i].ClearSpot();
         }
              }
     }

}
