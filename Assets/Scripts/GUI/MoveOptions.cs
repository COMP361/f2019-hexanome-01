using UnityEngine;
using UnityEngine.UI;

public class MoveOptions : MonoBehaviour {
    
    GameObject panel;

    Button cancelBtn, confirmBtn, setDestBtn, pickFarmerBtn, dropFarmerBtn;

    void OnEnable() {
        EventManager.MoveSelect += Show;
        EventManager.MoveCancel += Hide;
        EventManager.MoveCancel += LockConfirm;
        EventManager.CellClick += UnlockConfirm;
        EventManager.FarmerOnCell += UnlockPickFarmer;
    }

    void OnDisable() {
        EventManager.MoveSelect -= Show;
        EventManager.MoveCancel -= Hide;
        EventManager.MoveCancel -= LockConfirm;
        EventManager.CellClick -= UnlockConfirm;
        EventManager.FarmerOnCell -= UnlockPickFarmer;
    }

    void Awake() {
        panel = transform.Find("Panel").gameObject;
        //Hide();
        
        cancelBtn = panel.transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate { EventManager.TriggerMoveCancel(); });

        confirmBtn = panel.transform.Find("Confirm Button").GetComponent<Button>();
        confirmBtn.onClick.AddListener(delegate { EventManager.TriggerMoveConfirm(); });

        pickFarmerBtn = panel.transform.Find("Button Pick Farmer").GetComponent<Button>();
        pickFarmerBtn.onClick.AddListener(delegate { EventManager.TriggerPickFarmer(); });

        dropFarmerBtn = panel.transform.Find("Button Drop Farmer").GetComponent<Button>();
        dropFarmerBtn.onClick.AddListener(delegate { EventManager.TriggerDropFarmer(); });
    }

    void UnlockConfirm(int cellID) {
        UnlockBtn(confirmBtn);
    }

    void LockConfirm() {
        LockBtn(confirmBtn);
    }

    void UnlockPickFarmer() {
        UnlockBtn(pickFarmerBtn);
    }

    void UnlockBtn(Button btn) {
        btn.interactable = true;
    }

    void LockBtn(Button btn) {
        confirmBtn.interactable = false;
    }

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }
}