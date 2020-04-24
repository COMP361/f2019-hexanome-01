using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Specialized;
public class HeroInventory
{
    public BigToken bigToken { get; private set; }
    public Helm helm { get; private set; }

    public string parentHero;
    private int spaceSmall;
    public int numOfGold { get; private set; }
    public PhotonView photonView;

    public OrderedDictionary golds { get; private set; }
    public OrderedDictionary AllTokens { get; private set; }
    public OrderedDictionary smallTokens { get; private set; }

    public HeroInventory(string parentHero){
      bigToken = null;
      helm = null;
      spaceSmall = 3;
      numOfGold = 0;
      this.parentHero = parentHero;
      golds = new OrderedDictionary();
      AllTokens = new OrderedDictionary();
      smallTokens = new OrderedDictionary();
      EventManager.TriggerInitHeroInv(this);
    }


#region AddTokens

    public bool HasSmallToken(Type type) {
      foreach (DictionaryEntry entry in smallTokens) {
        if(type.IsCompatibleWith(entry.Value.GetType())) return true;
      }
      return false;
    }

    public void Clear(){
      bigToken = null;
      helm = null;
      numOfGold = 0;
      golds = new OrderedDictionary();
      AllTokens = new OrderedDictionary();
      smallTokens = new OrderedDictionary();

      GameManager.instance.photonView.RPC("ClearRPC", RpcTarget.AllViaServer, new object[] {parentHero});
    }

    public void Clear2(){
      bigToken = null;
      helm = null;
      numOfGold = 0;
      golds = new OrderedDictionary();
      AllTokens = new OrderedDictionary();
      smallTokens = new OrderedDictionary();
    }


    public void AddGold(GoldCoin gold){
      int viewID = gold.GetComponent<PhotonView>().ViewID;
      GameManager.instance.photonView.RPC("AddGoldHeroRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});

    }

    public void addGold2(GoldCoin gold){
        string id = convertToKey(gold.GetComponent<PhotonView>().ViewID);
        golds.Add(id, (GoldCoin)gold);
        AllTokens.Add(id,(GoldCoin)gold);
        numOfGold++;
        if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
          EventManager.TriggerInventoryUIHeroUpdate(this);
          EventManager.TriggerGoldUpdate(GameManager.instance.findHero(parentHero));
        }
        else if(parentHero.Equals(CharChoice.choice.TokenName)){
          EventManager.TriggerInventoryUIHeroPeak(this);
        }
    }

    public void RemoveGold(int amtToRemove){
      if(numOfGold >= amtToRemove){
        for(int i = 0; i< amtToRemove; i++){
          int viewID = ((GoldCoin)golds[i]).GetComponent<PhotonView>().ViewID;
          GameManager.instance.photonView.RPC("RemoveGoldHeroRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
        }
      }
      else{
      }
    }

    public void RemoveGold2(int viewID){
      numOfGold--;
      AllTokens.Remove(convertToKey(viewID));
      golds.Remove(convertToKey(viewID));
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
        EventManager.TriggerGoldUpdate(GameManager.instance.findHero(parentHero));
      } else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    //what to do with errors
    public bool AddSmallToken(SmallToken smallToken){
      if(smallTokens.Count >= spaceSmall){
        EventManager.TriggerError(1);
        return false;
      }
      else{
        int viewID = smallToken.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("AddSmallTokenRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
        return true;
      }
    }

    public void AddSmallToken2(SmallToken smallToken){
      string id = convertToKey(smallToken.GetComponent<PhotonView>().ViewID);
      smallTokens.Add(id, (SmallToken)smallToken);
      AllTokens.Add(id,(SmallToken)smallToken);

      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      } else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public void RemoveSmallToken(SmallToken smallToken){
      if(smallTokens.Contains(convertToKey(smallToken.GetComponent<PhotonView>().ViewID))){
        int viewID = smallToken.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("RemoveSmallTokenRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
      }
      else {
        Debug.Log("Error hero inventory remove smallToken");
      }

    }

    public void RemoveSmallToken2(int viewID){
      AllTokens.Remove(convertToKey(viewID));
      smallTokens.Remove(convertToKey(viewID));
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
      else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public void ReplaceSmallToken(SmallToken original, SmallToken newItem, bool destroy = false){
      int originalViewID = original.GetComponent<PhotonView>().ViewID;
      int newViewID = newItem.GetComponent<PhotonView>().ViewID;

      if(smallTokens.Contains(convertToKey(originalViewID))){
        GameManager.instance.photonView.RPC("ReplaceSmallTokenRPC", RpcTarget.AllViaServer, new object[] {originalViewID, newViewID, destroy, parentHero});
      } else {
        Debug.Log("Error hero inventory remove BigToken");
      }
    }

    public void ReplaceSmallToken2(int originalViewID, int newViewID, bool destroy){
      if(smallTokens.Contains(convertToKey(originalViewID))){
        SmallToken token = (SmallToken)PhotonView.Find(newViewID).GetComponent<Token>();
        string id = convertToKey(newViewID);

        AllTokens.Remove(convertToKey(originalViewID));
        smallTokens.Remove(convertToKey(originalViewID));

        smallTokens.Add(id, token);
        AllTokens.Add(id, token);

        if(destroy) {
          Token tkn = PhotonView.Find(originalViewID).GetComponent<Token>();
          if(tkn != null) GameObject.Destroy(tkn.gameObject);
        }
      }

      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      } else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public bool AddBigToken(BigToken item){
      if(bigToken == null){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("AddBigTokenRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
        return true;
      } else {
        Debug.Log("bigToken not null");
        EventManager.TriggerError(1);
        return false;
      }
    }

    public void AddBigToken2(BigToken item){
      string id = convertToKey(item.GetComponent<PhotonView>().ViewID);
      AllTokens.Add(id,(BigToken)item);
      bigToken = item;
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
      else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public void RemoveBigToken(BigToken item){
      if(bigToken.GetComponent<PhotonView>().ViewID == item.GetComponent<PhotonView>().ViewID){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("RemoveBigTokenRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
      } else {
        Debug.Log("Error hero inventory remove BigToken");
      }
    }

    public void RemoveBigToken2(int viewID){
      if(bigToken.GetComponent<PhotonView>().ViewID == viewID){
        bigToken = null;
        AllTokens.Remove(convertToKey(viewID));
      }

      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      } else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public void ReplaceBigToken(BigToken original, BigToken newItem, bool destroy = false){
      int originalViewID = original.GetComponent<PhotonView>().ViewID;
      int newViewID = newItem.GetComponent<PhotonView>().ViewID;

      if(bigToken.GetComponent<PhotonView>().ViewID == originalViewID){
        GameManager.instance.photonView.RPC("ReplaceBigTokenRPC", RpcTarget.AllViaServer, new object[] {originalViewID, newViewID, destroy, parentHero});
      } else {
        Debug.Log("Error hero inventory remove BigToken");
      }
    }

    public void ReplaceBigToken2(int originalViewID, int newViewID, bool destroy){
      if(bigToken.GetComponent<PhotonView>().ViewID == originalViewID){
        BigToken token = (BigToken)PhotonView.Find(newViewID).GetComponentInParent<Token>();
        string id = convertToKey(newViewID);
        AllTokens.Add(id, token);

        AllTokens.Remove(convertToKey(originalViewID));
        if(destroy) GameObject.Destroy(bigToken.gameObject);
        bigToken = token;
      }

      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      } else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public bool AddHelm(Helm item){
      if(helm == null){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("AddHelmRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
        return true;
      }
      else{
        //Error already a helm
        EventManager.TriggerError(1);
        return false;
      }
    }

    public void AddHelm2(Helm item){
      string id = convertToKey(item.GetComponent<PhotonView>().ViewID);
      helm = item;
      AllTokens.Add(id,(Helm)item);
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
      else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public void RemoveHelm(Helm item){
      if(helm.GetComponent<PhotonView>().ViewID == item.GetComponent<PhotonView>().ViewID){
        int viewID = item.GetComponent<PhotonView>().ViewID;
        GameManager.instance.photonView.RPC("RemoveHelmRPC", RpcTarget.AllViaServer, new object[] {viewID, parentHero});
      }
      else {
        Debug.Log("Error hero inventory remove helm");
      }
    }

    public void RemoveHelm2(int viewID){
      if(helm.GetComponent<PhotonView>().ViewID == viewID){
        helm = null;
        AllTokens.Remove(convertToKey(viewID));
      }
      if(GameManager.instance.MainHero.TokenName.Equals(parentHero)){
        EventManager.TriggerInventoryUIHeroUpdate(this);
      }
      else if(parentHero.Equals(CharChoice.choice.TokenName)){
        EventManager.TriggerInventoryUIHeroPeak(this);
      }
    }

    public string convertToKey(int a){
      string toReturn = "" + a;
      return toReturn;
    }

    public bool AddItem(Token item){


      if(item == null){
        return false;
      }
      if (item is GoldCoin){
        AddGold((GoldCoin) item);
        return true;
      }
      else if (item is Helm){
        return  AddHelm((Helm) item);
      }
      else if (item.GetType().IsSubclassOf(typeof(SmallToken))){
        return  AddSmallToken((SmallToken) item);
      }
      else if (item.GetType().IsSubclassOf(typeof(BigToken))){
        return  AddBigToken((BigToken) item);
      }
      return false;
    }

  #endregion

}
