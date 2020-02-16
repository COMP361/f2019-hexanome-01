using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();


    private bool inventoryEnable;
    private int space = 3;


    void OnEnable() {
      EventManager.InventoryUI += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUI -= updateUI;
    }





    void start(){

    }

    void update(){
      
    }

    public bool  Add(Item item){
      if(items.Count >= space){
        Debug.Log("Not enough room ");
        return false;
      }
       items.Add(item);
       return true;
    }

    public void Remove(Item item){
      items.Remove(item);
    }


    public void updateUI(){}
}
