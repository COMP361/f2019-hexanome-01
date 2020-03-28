using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class GoldOptions : MonoBehaviour
{

    GameObject heroPanel;
    GameObject cellPanel;
    Button cancelBtnCell, cancelBtnHero, dropGoldBtn, pickGoldBtn;
    public PhotonView photonView;
    GoldCoin gold;
    // Start is called before the first frame update

    void OnEnable() {
        EventManager.heroGoldClick += ShowHero;
        EventManager.cellGoldClick += ShowCell;
        EventManager.dropGoldClick += Hide;
      }

    void OnDisable() {
        EventManager.heroGoldClick -= ShowHero;
        EventManager.cellGoldClick -= ShowCell;
        EventManager.dropGoldClick -= Hide;
    }


    void Awake() {
        heroPanel = transform.Find("Hero").gameObject;
        cellPanel = transform.Find("Cell").gameObject;
      //  gold = null;

        cancelBtnCell = cellPanel.transform.Find("Cancel Button").GetComponent<Button>();
    //    cancelBtnCell.onClick.AddListener(delegate { EventManager.TriggerGoldCellCancel(); });

        cancelBtnHero = heroPanel.transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtnHero.onClick.AddListener(delegate { Hide(); });

        pickGoldBtn = cellPanel.transform.Find("Button Pick Gold").GetComponent<Button>();
  //      pickGoldBtn.onClick.AddListener(delegate { EventManager.TriggerPickGold(); });

        dropGoldBtn = heroPanel.transform.Find("Button Drop Gold").GetComponent<Button>();
    //    dropGoldBtn.onClick.AddListener(delegate { EventManager.TriggerDropGold(); });
    }

    public void ShowHero(GoldCoin gold) {
     //   this.gold = gold;
        heroPanel.SetActive(true);
    }

    public void ShowCell(GoldCoin gold) {
        this.gold = gold;
        cellPanel.SetActive(true);
    }

    public void Hide() {
        cellPanel.SetActive(false);
        heroPanel.SetActive(false);
       // this.gold = null;
    }

    public void DropGold() {
        // EventManager.TriggerDropGoldClick();
       GameManager.instance.MainHero.heroInventory.RemoveGold(1);
       //  photonView.RPC("DropGoldRPC", RpcTarget.AllViaServer);
       int cellID = GameManager.instance.MainHero.Cell.Index;
       GameManager.instance.photonView.RPC("AddGoldCellRPC", RpcTarget.AllViaServer, cellID);

        Hide();
    }

    [PunRPC]
    public void DropGoldRPC(){
    //    GameObject goldCoinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/GoldCoin", Vector3.zero, Quaternion.identity, 0);
    //    Token goldCoin = goldCoinGO.GetComponent<GoldCoin>();
    //    int cellID = GameManager.instance.MainHero.Cell.Index;
    //    GameManager.instance.photonView.RPC("AddGoldCellRPC", RpcTarget.AllViaServer, cellID);
    }

    public void PickGold() {
        //  EventManager.TriggerDropGoldClick();
        Cell cell = Cell.FromId(GameManager.instance.MainHero.Cell.Index);
        cell.Inventory.RemoveToken(gold);
        InventoryUICell.instance.ForceUpdate(cell.Inventory, cell.Index);
        GameManager.instance.MainHero.heroInventory.AddGold(gold);
        Hide();
    }
}
