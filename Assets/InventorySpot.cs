using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpot : MonoBehaviour
{
    // Start is called before the first frame update
    Items item;
    public Image icon;

    public void AddItem(Items newItem)
    {
      item = newItem;
      icon.sprite = item.icon;
      icon.enabled = true;
    }

    public void ClearSpot()
    {
      item = null;
      icon.sprite = null;
      icon.enabled = false;
    }

    public void UseItem(){

      if(item != null){
        item.use();
      }
    }
}
