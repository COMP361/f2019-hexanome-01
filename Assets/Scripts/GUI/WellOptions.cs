using UnityEngine;
using UnityEngine.UI;

public class WellOptions : MonoBehaviour
{


    GameObject cellPanel;
    Button cancelBtnCell, pickWellBtn;


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


    }

    public void ShowCell() {
        cellPanel.SetActive(true);
    }

    public void hide() {
        cellPanel.SetActive(false);
    }

    public void PickWell() {
      EventManager.TriggerPickWellClick(GameManager.instance.MainHero);
      hide();
    }

}
