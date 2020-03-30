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
        heroPanel = transform.Find("Gold Drop Action").gameObject;
        cellPanel = transform.Find("Gold Pick Action").gameObject;
      //  gold = null;

        cancelBtnCell = cellPanel.transform.Find("Cancel Button").GetComponent<Button>();
    //    cancelBtnCell.onClick.AddListener(delegate { EventManager.TriggerGoldCellCancel(); });

        cancelBtnHero = heroPanel.transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtnHero.onClick.AddListener(delegate { Hide(); });

        pickGoldBtn = cellPanel.transform.Find("Pick Gold Button").GetComponent<Button>();
  //      pickGoldBtn.onClick.AddListener(delegate { EventManager.TriggerPickGold(); });

        dropGoldBtn = heroPanel.transform.Find("Drop Gold Button").GetComponent<Button>();
    //    dropGoldBtn.onClick.AddListener(delegate { EventManager.TriggerDropGold(); });
    }

    public void ShowHero(GoldCoin gold) {
        heroPanel.SetActive(true);
    }

    public void ShowCell(GoldCoin gold) {
        this.gold = gold;
        cellPanel.SetActive(true);
    }

    public void Hide() {
        cellPanel.SetActive(false);
        heroPanel.SetActive(false);
    }

    public void DropGold() {

        GameManager.instance.MainHero.heroInventory.RemoveGold(1);
        Cell cell = GameManager.instance.MainHero.Cell;
        Token goldCoin = GoldCoin.Factory();
        cell.Inventory.AddToken(goldCoin);

        Hide();
    }

    public void PickGold() {
        Cell cell = Cell.FromId(GameManager.instance.MainHero.Cell.Index);
        cell.Inventory.RemoveToken(gold);
        GameManager.instance.MainHero.heroInventory.AddGold(gold);
        Hide();
    }
}
