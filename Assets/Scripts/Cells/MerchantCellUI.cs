using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;


public class MerchantCellUI : Singleton<MerchantCellUI>
{

    GameObject merchantPanel;
    GameObject buyStrengthPanel;
    protected Transform titleTransform;
    protected bool isLocked;
    protected  int cellIndex;

    void Awake() {
        isLocked = false;
        merchantPanel = transform.Find("MerchantUI").gameObject;
        buyStrengthPanel = transform.FindDeepChild("BuyStrength").gameObject;
        titleTransform = transform.FindDeepChild("MerchantTitle");
    }

    void Update(){
        if(Input.GetButtonDown("LockMerchantCell")){
            isLocked = !isLocked;
            }
      }

    void OnEnable() {
        EventManager.MerchCellMouseEnter += Enter;
        EventManager.MerchCellMouseLeave += Exit;

      }

    void OnDisable() {
        EventManager.MerchCellMouseEnter -= Enter;
        EventManager.MerchCellMouseLeave -= Exit;
      }

    void Enter(int index){
      if(!isLocked){
      cellIndex = index;
      FormatTitle(cellIndex);
      merchantPanel.SetActive(true);
    }
    }

    void Exit(){
      if(!isLocked){
      merchantPanel.SetActive(false);
    }
    }

    void FormatTitle(int index){
      titleTransform.GetComponent<Text>().text = "Merchant:" + index;
    }

    public void ShowBuyStrength(){
      if(cellIndex == GameManager.instance.MainHero.Cell.Index){
      buyStrengthPanel.SetActive(true);
    }
    else {
      Debug.Log("You must be on same cell " + GameManager.instance.MainHero.Cell.Index );
    }
    }

    public void HideBuyStrength(){
      buyStrengthPanel.SetActive(false);
    }

    public void BuyStrength(){
      Debug.Log("I bought some strength");
      HideBuyStrength();
    }


}
