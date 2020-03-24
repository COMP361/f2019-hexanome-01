using UnityEngine;
using UnityEngine.UI;

public class MoveOptions : MonoBehaviour {
    
    GameObject panel;

    Button cancelBtn, confirmBtn, clearPathBtn, pickFarmerBtn, dropFarmerBtn;

    void OnEnable() {
        EventManager.MoveSelect += Show;
        EventManager.MoveThorald += Show;
        EventManager.MoveThorald += DisableHeroOptions;
        EventManager.MoveCancel += EnableHeroOptions;
        EventManager.MoveConfirm += EnableHeroOptions;
        EventManager.MoveCancel += Hide;
        EventManager.MoveConfirm += Hide;
        EventManager.PathUpdate += LockConfirm;
        EventManager.PathUpdate += LockClearPath;

        EventManager.FarmersInventoriesUpdate += LockDropFarmer;
        EventManager.FarmersInventoriesUpdate += LockPickFarmer;
        EventManager.MoveStart += LockPickFarmer;
    }

    void OnDisable() {
        EventManager.MoveSelect -= Show;
        EventManager.MoveThorald -= Show;
        EventManager.MoveThorald -= DisableHeroOptions;
        EventManager.MoveCancel -= EnableHeroOptions;
        EventManager.MoveConfirm -= EnableHeroOptions;
        EventManager.MoveCancel -= Hide;
        EventManager.MoveConfirm -= Hide;
        EventManager.PathUpdate -= LockConfirm;
        EventManager.PathUpdate -= LockClearPath;
        
        EventManager.FarmersInventoriesUpdate -= LockPickFarmer;
        EventManager.FarmersInventoriesUpdate -= LockDropFarmer;
        EventManager.MoveStart -= LockPickFarmer;
    }

    void Awake() {
        panel = transform.Find("Panel").gameObject;
        
        cancelBtn = panel.transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate { EventManager.TriggerMoveCancel(); });

        clearPathBtn = panel.transform.Find("Clear Path Button").GetComponent<Button>();
        clearPathBtn.onClick.AddListener(delegate { EventManager.TriggerClearPath(); });

        confirmBtn = panel.transform.Find("Confirm Button").GetComponent<Button>();
        confirmBtn.onClick.AddListener(delegate { EventManager.TriggerMoveConfirm(); });

        pickFarmerBtn = panel.transform.Find("Button Pick Farmer").GetComponent<Button>();
        pickFarmerBtn.onClick.AddListener(delegate { EventManager.TriggerPickFarmer(); });

        dropFarmerBtn = panel.transform.Find("Button Drop Farmer").GetComponent<Button>();
        dropFarmerBtn.onClick.AddListener(delegate { EventManager.TriggerDropFarmer(); });
    }

    void LockConfirm(int count) {
        if(count > 0) {
            Buttons.Unlock(confirmBtn);
        } else {
            Buttons.Lock(confirmBtn);
        }
    }

    void LockClearPath(int count) {
        if(count > 0) {
            Buttons.Unlock(clearPathBtn);
        } else {
            Buttons.Lock(clearPathBtn);
        }
    }

    void LockPickFarmer(Movable movable) {
        Buttons.Lock(pickFarmerBtn);
    }

    void LockPickFarmer(int attachedFarmers, int noTargetFarmers, int detachedFarmers) {
        if(detachedFarmers > 0) {
            Buttons.Unlock(pickFarmerBtn);
        } else {
            Buttons.Lock(pickFarmerBtn);
        }
    }

    void LockDropFarmer(int attachedFarmers, int noTargetFarmers, int detachedFarmers) {
        if(noTargetFarmers > 0) {
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

    public void DisableHeroOptions() {
        pickFarmerBtn.gameObject.SetActive(false);
        dropFarmerBtn.gameObject.SetActive(false);
    }

    public void EnableHeroOptions() {
        pickFarmerBtn.gameObject.SetActive(true);
        dropFarmerBtn.gameObject.SetActive(true);
    }
}