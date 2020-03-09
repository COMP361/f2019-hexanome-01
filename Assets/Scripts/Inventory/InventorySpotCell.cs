using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InventorySpotCell : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    Token token;
    public static int cellIndex;
  //  protected GameManager gm;

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
     Debug.Log("What is going on " + GameManager.instance.MainHero.TokenName);

     if(token != null){

      if(GameManager.instance.MainHero.Cell.Index == cellIndex){
        if(token.TokenName.Equals("GoldCoin")){
        ((GoldCoin) token).useCell();
        }
        if(token.TokenName.Equals("Well")){
        ((Well) token).useCell();
        }
      }
      else{
        EventManager.TriggerBlockOnInventoryClick();
        }
      }
    }
}
