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
      if(GameManager.instance.MainHero.Cell.Index == cellIndex){
        token.UseCell();
      } else {
        EventManager.TriggerBlockOnInventoryClick();
      }
    }
  }
}