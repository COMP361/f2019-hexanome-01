using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CellItemsActions : MonoBehaviour
{

  GameObject cellItemsPanel;
  Button cancelBtn, pickBtn;
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
  }


  public void ShowCellActions(Token item) {
      token = item;

      if(token is Wineskin){
      cellItemsPanelTitle.text = Wineskin.name;
      cellItemsPanelDesc.text = Wineskin.desc;
      }
      else if(token is Potion){
      cellItemsPanelTitle.text = Potion.name;
      cellItemsPanelDesc.text = Potion.desc;
      }
      else if(token is Bow){
      cellItemsPanelTitle.text = Bow.name;
      cellItemsPanelDesc.text = Bow.desc;
      }
      else if(token is Falcon){
      cellItemsPanelTitle.text = Falcon.name;
      cellItemsPanelDesc.text = Falcon.desc;
      }
      else if(token is Helm){
      cellItemsPanelTitle.text = Helm.name;
      cellItemsPanelDesc.text = Helm.desc;
      }
      else if(token is Herb){
      cellItemsPanelTitle.text = Herb.name;
      cellItemsPanelDesc.text = Herb.desc;
      }
      else if(token is Runestone){
      cellItemsPanelTitle.text = Runestone.name;
      cellItemsPanelDesc.text = Runestone.desc;
      }
      else if(token is Shield){
      cellItemsPanelTitle.text = Shield.name;
      cellItemsPanelDesc.text = Shield.desc;
      }
      else if(token is Telescope){
      cellItemsPanelTitle.text = Telescope.name;
      cellItemsPanelDesc.text = Telescope.desc;
      }

      else if(token is GoldCoin){
      //
      }


      cellItemsPanel.SetActive(true);
  }



  public void HideCellActions() {
      this.token = null;
      cellItemsPanel.SetActive(false);
  }

  public void PickItem() {

    if(GameManager.instance.MainHero.heroInventory.AddItem(this.token)){
      Cell cell = GameManager.instance.MainHero.Cell;
      cell.Inventory.RemoveToken(this.token);
    }
    else{
      Debug.Log("Error PickItem inventory full");
    }
/*
      if(token is SmallToken){
      GameManager.instance.MainHero.heroInventory.RemoveSmallToken((SmallToken) this.token);
      Cell cell = GameManager.instance.MainHero.Cell;
      cell.Inventory.AddToken(this.token);
      }
      else if (token is BigToken){
        GameManager.instance.MainHero.heroInventory.RemoveBigToken((BigToken) this.token);
        Cell cell = GameManager.instance.MainHero.Cell;
        cell.Inventory.AddToken(this.token);
      }
      else if (token is Helm){
        GameManager.instance.MainHero.heroInventory.RemoveHelm((Helm) this.token);
        Cell cell = GameManager.instance.MainHero.Cell;
        cell.Inventory.AddToken(this.token);
      }
*/
      HideCellActions();
  }





}
