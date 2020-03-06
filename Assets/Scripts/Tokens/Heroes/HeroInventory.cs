using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Token> smallTokens = new List<Token>();
    public Token bigToken;
    public Token helm;
    public Token gold;

    private int spaceSmall;
    private int numOfGold;


    void OnEnable() {
      EventManager.InventoryUICellEnter += updateUI;
    }

    void OnDisable() {
      EventManager.InventoryUICellEnter -= updateUI;
    }





    void start(){
      bigToken = null;
      helm = null;
      gold =null;
      spaceSmall = 3;
      numOfGold = 0;
    }



    public void  Add(Token item){
      /*
      if(items.Count >= space){
        Debug.Log("Not enough room ");
        return false;
      }
       items.Add(item);
       return true;
       */
    }

    public void Remove(Token item){
    //  tokens.Remove(item);
    }


    public void updateUI(CellInventory inventory, int index){}
}
