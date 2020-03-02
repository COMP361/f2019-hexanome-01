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

        EventManager.FarmersInventoriesUpdate += LockDropFarmer;
        EventManager.FarmersInventoriesUpdate += LockPickFarmer;
        
        EventManager.MoveStart += LockPickFarmer;
        EventManager.PickFarmer += UnlockDropFarmer;
    }

    void OnDisable() {
        EventManager.MoveSelect -= Show;
        EventManager.MoveCancel -= Hide;
        EventManager.MoveCancel -= LockConfirm;
        EventManager.CellClick -= UnlockConfirm;
        
        EventManager.FarmersInventoriesUpdate -= LockPickFarmer;
        EventManager.FarmersInventoriesUpdate -= LockDropFarmer;

        EventManager.MoveStart -= LockPickFarmer;
        EventManager.PickFarmer -= UnlockDropFarmer;
    }

    void Awake() {
        panel = transform.Find("Panel").gameObject;
        Hide();
        
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
        Buttons.Unlock(confirmBtn);
    }

    void LockConfirm() {
        Buttons.Lock(confirmBtn);
    }

    //void UnlockPickFarmer(int farmersWithHero, int farmersOnCell) {
    //    if(farmersOnCell > farmersWithHero) Buttons.Unlock(pickFarmerBtn);
    //}

    void LockPickFarmer(Movable movable) {
        Buttons.Lock(pickFarmerBtn);
    }

    void LockPickFarmer(int farmersWithHero, int farmersOnCell) {
        if(farmersOnCell > farmersWithHero) {
            Buttons.Unlock(pickFarmerBtn);
        } else {
            Buttons.Lock(pickFarmerBtn);
        }
    }

    void UnlockDropFarmer() {
        Buttons.Unlock(dropFarmerBtn);
    }

    void LockDropFarmer(int farmersWithHero, int farmersOnCell) {
        if(farmersWithHero > 0) {
            Buttons.Unlock(dropFarmerBtn);
        } else {
            Buttons.Lock(dropFarmerBtn);
        }
    }

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }
}