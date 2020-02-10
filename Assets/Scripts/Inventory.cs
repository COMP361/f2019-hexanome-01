using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnable;
    public GameObject inventory;

    private int allSpots;
    private int enabledSpots;
    private GameObject[] spot;

    public GameObject spotHolder ;

    void start(){
      allSpots = 3;
      spot = new GameObject[allSpots];
      for(int i = 0; i < allSpots; i++){
        spot[i] = spotHolder.transform.GetChild(i).gameObject;
      }
    }

    void update(){
       if(Input.GetKeyDown(KeyCode.I)){
         inventoryEnable = !inventoryEnable;
       }

       if(inventoryEnable){
         inventory.SetActive(true);
       }else{
         inventory.SetActive(false);
       }

    }
}
