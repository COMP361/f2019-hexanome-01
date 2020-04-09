using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpotHero : MonoBehaviour
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

    public void UseItem(){
      if(token != null){
        if(CharChoice.choice.TokenName.Equals(GameManager.instance.MainHero.TokenName)){
        token.UseHero();
      }
      }
    }
}
