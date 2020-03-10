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
      FormatTitle(index);
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
      buyStrengthPanel.SetActive(true);
    }

    public void BuyStrength(Hero hero)
    {
        //if (hero is Dwarf d)
        //{
        //   
        //}
    }


}
