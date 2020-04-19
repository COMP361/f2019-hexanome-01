using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpotTrade : MonoBehaviour
{
  Image icon;
  Token token;

  void Awake() {
    icon = GetComponent<Image>();
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

  public void AddToTrade(){
    if(token != null){
      EventManager.TriggerTradeChange(token);
    }
  }

  public void RemoveFromTrade(){
    if(token != null){
      EventManager.TriggerTradeChange(token);
    }
  }
}