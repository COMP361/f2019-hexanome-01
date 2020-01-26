using UnityEngine;
using UnityEngine.UI;

public class MoveOptions : MonoBehaviour {
    
    GameObject panel;

    Button cancelBtn, confirmBtn, setDestBtn;

    void OnEnable() {
        EventManager.MoveSelect += Show;
        EventManager.MoveCancel += Hide;
        EventManager.MoveCancel += LockConfirm;
        EventManager.CellClick += UnlockConfirm;
    }

    void OnDisable() {
        EventManager.MoveSelect -= Show;
        EventManager.MoveCancel -= Hide;
        EventManager.MoveCancel -= LockConfirm;
        EventManager.CellClick -= UnlockConfirm;
    }

    void Awake() {
        panel = transform.Find("Panel").gameObject;
        Hide();
        
        cancelBtn = panel.transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate { EventManager.TriggerMoveCancel(); });

        confirmBtn = panel.transform.Find("Confirm Button").GetComponent<Button>();
        confirmBtn.onClick.AddListener(delegate { EventManager.TriggerMoveConfirm(); });
    }

    void UnlockConfirm(int CellID) {
        confirmBtn.interactable = true;
    }

    void LockConfirm() {
        confirmBtn.interactable = false;
    }

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }
}