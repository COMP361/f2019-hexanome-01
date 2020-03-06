using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpotCell : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    Token token;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }


    public void AddItem(Token newToken)
    {
      token = newToken;
      icon.sprite = token.getSprite();
      icon.enabled = true;
    }

    public void ClearSpot()
    {
      token = null;
      icon.sprite = null;
      icon.enabled = false;
    }

    public void UseItem(){

      if(token != null){
  //      token.use();
        Debug.Log("wistiti + " + token.TokenName);
      }
    }
}
