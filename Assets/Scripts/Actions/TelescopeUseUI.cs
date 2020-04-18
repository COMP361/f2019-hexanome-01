using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TelescopeUseUI : MonoBehaviour
{

  GameObject TelescopePanel;
  Button UnveilBtn, DoNotUnveilBtn;
  public PhotonView photonView;
  Token token;
  Token toUnveil;

  Text TelescopePanelTitle;
  Text TelescopePanelDesc;


  void OnEnable() {
    EventManager.TelescopeUseUI += ShowTelescopeUse;
  }

  void OnDisable() {
    EventManager.TelescopeUseUI -= ShowTelescopeUse;
  }


  void Awake() {
    TelescopePanel = transform.Find("Telescope Use UI").gameObject;

    TelescopePanelTitle = TelescopePanel.transform.Find("Panel Title").GetComponent<Text>();
    TelescopePanelDesc = TelescopePanel.transform.Find("Panel Description").GetComponent<Text>();

    TelescopePanelTitle.text  = "";
    TelescopePanelDesc.text = "";
    token = null;
    toUnveil = null;


    UnveilBtn = TelescopePanel.transform.Find("Unveil Button").GetComponent<Button>();
    UnveilBtn.onClick.AddListener(delegate { Unveil(); ((Telescope)token).instantiateUsePanels(); });

    DoNotUnveilBtn = TelescopePanel.transform.Find("Do Not Unveil Button").GetComponent<Button>();
    DoNotUnveilBtn.onClick.AddListener(delegate { HideTelescopeUse(); ((Telescope)token).instantiateUsePanels(); });

  }

  public void ShowTelescopeUse(Token item, Pair<Token, int> pair) {
    token = item;
    toUnveil = pair.First;
    TelescopePanelTitle.text  = "" + toUnveil.itemName + " on cell " + pair.Second;
    TelescopePanelDesc.text = "Do you want to unveil this item?";
    TelescopePanel.SetActive(true);
  }

  public void HideTelescopeUse() {
    TelescopePanel.SetActive(false);
  }

  public void Unveil(){
    if(toUnveil is Runestone){
      ((Runestone)toUnveil).uncoverRunestone();
    }
    else if (toUnveil is Fog){
      ((Fog)toUnveil).Reveal();
    }
    HideTelescopeUse();
  }


  public void clearToken(){
    this.token = null;
    this.toUnveil = null;
  }
}