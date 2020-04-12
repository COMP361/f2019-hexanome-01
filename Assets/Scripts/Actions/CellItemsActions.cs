using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CellItemsActions : MonoBehaviour
{

  GameObject cellItemsPanel;
  Button cancelBtn, pickBtn, useBtn;
  public PhotonView photonView;
  Token token;

  Text cellItemsPanelTitle;
  Text cellItemsPanelDesc;


  void OnEnable() {
    EventManager.CellItemClick += ShowCellActions;
    }

  void OnDisable() {
    EventManager.CellItemClick -= ShowCellActions;
  }


  void Awake() {
    cellItemsPanel = transform.Find("Cell Items Actions").gameObject;

    cellItemsPanelTitle = cellItemsPanel.transform.Find("Panel Title").GetComponent<Text>();
    cellItemsPanelDesc = cellItemsPanel.transform.Find("Panel Description").GetComponent<Text>();


    token = null;



    cancelBtn= cellItemsPanel.transform.Find("Cancel Button").GetComponent<Button>();
    cancelBtn.onClick.AddListener(delegate { HideCellActions(); });

    pickBtn = cellItemsPanel.transform.Find("Pick Item Button").GetComponent<Button>();
    pickBtn.onClick.AddListener(delegate { PickItem(); });

    useBtn = cellItemsPanel.transform.Find("Use Button").GetComponent<Button>();
    useBtn.onClick.AddListener(delegate { UseItem(); });

    useBtn.interactable = false;
  }


  public void ShowCellActions(Token item) {
    token = item;

    if(token is Wineskin){
      cellItemsPanelTitle.text = Wineskin.itemName;
      cellItemsPanelDesc.text = Wineskin.desc;

    }
    else if(token is Potion){
      cellItemsPanelTitle.text = Potion.itemName;
      cellItemsPanelDesc.text = Potion.desc;

    }
    else if(token is Bow){
      cellItemsPanelTitle.text = Bow.itemName;
      cellItemsPanelDesc.text = Bow.desc;

    }
    else if(token is Falcon){
      cellItemsPanelTitle.text = Falcon.itemName;
      cellItemsPanelDesc.text = Falcon.desc;

    }
    else if(token is Helm){
      cellItemsPanelTitle.text = Helm.itemName;
      cellItemsPanelDesc.text = Helm.desc;

    }
    else if(token is Herb){
      cellItemsPanelTitle.text = Herb.itemName;
      cellItemsPanelDesc.text = Herb.desc;

    }
    else if(token is Runestone){
      cellItemsPanelTitle.text = Runestone.itemName;
      cellItemsPanelDesc.text = Runestone.desc;
      if(((Runestone)token).isCovered){
        useBtn.interactable = true;
        pickBtn.interactable = false;
      }

    }
    else if(token is Shield){
      cellItemsPanelTitle.text = Shield.itemName;
      cellItemsPanelDesc.text = Shield.desc;

    }
    else if(token is Telescope){
      cellItemsPanelTitle.text = Telescope.itemName;
      cellItemsPanelDesc.text = Telescope.desc;

    }
    else if(token is GoldCoin){
      cellItemsPanelTitle.text = GoldCoin.itemName;
      cellItemsPanelDesc.text = GoldCoin.desc;

    }
    else if (token is Well){
      cellItemsPanelTitle.text = Well.itemName;
      cellItemsPanelDesc.text = Well.desc;

    }
    cellItemsPanel.SetActive(true);
  }

  public void HideCellActions() {
    this.token = null;
    useBtn.interactable = false;
    pickBtn.interactable = true;
    cellItemsPanel.SetActive(false);
  }

  public void PickItem() {
    if(token is Well){
      ((Well)token).EmptyWell(GameManager.instance.MainHero);
    }
    else if(GameManager.instance.MainHero.heroInventory.AddItem(this.token)){
      Cell cell = GameManager.instance.MainHero.Cell;
      cell.Inventory.RemoveToken(this.token);
    }
    else{
      Debug.Log("Error PickItem inventory full");
    }

    HideCellActions();
  }

  public void UseItem(){
    Debug.Log("Items cell use");
    if(token is Runestone){
      token.UseEffect();
    }
    HideCellActions();
  }





}
