using UnityEngine;
using UnityEngine.UI;

public class WellOptions : MonoBehaviour
{


    GameObject cellPanel;
    Button cancelBtnCell, pickWellBtn;
    Well well;

    // Start is called before the first frame update

    void OnEnable() {
        EventManager.cellWellClick += ShowCell;

      }

    void OnDisable() {
        EventManager.cellWellClick -= ShowCell;

      }


    void Awake() {
        cellPanel = transform.Find("Cell").gameObject;

        cancelBtnCell = cellPanel.transform.Find("Cancel Button").GetComponent<Button>();
    //    cancelBtnCell.onClick.AddListener(delegate { EventManager.TriggerGoldCellCancel(); });

        pickWellBtn = cellPanel.transform.Find("Button Pick Well").GetComponent<Button>();
  //      pickGoldBtn.onClick.AddListener(delegate { EventManager.TriggerPickGold(); });
        well = null;

    }

    public void ShowCell(Well well) {
        this.well = well;
        cellPanel.SetActive(true);
    }

    public void Hide() {
      this.well = null;
      cellPanel.SetActive(false);
    }

    public void PickWell() {
      EventManager.TriggerPickWellClick(GameManager.instance.MainHero, this.well);
      Hide();
    }

}
