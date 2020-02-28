using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{

    public string name = "New Item";
    public Sprite icon = null;
  //  public bool isDefault = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void use(){
        // Use the item
        // Something might happen

        Debug.Log("Using " + name );

    }
    
}
