using UnityEngine;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour
{
    Button saveBtn;
    void OnEnable() {
        EventManager.ActionUpdate += LockActions;
    }

    void OnDisable() {
        EventManager.ActionUpdate -= LockActions;
    }
    
    void Awake()
    {
        saveBtn = transform.Find("Save Button").GetComponent<Button>();
        saveBtn.onClick.AddListener(delegate { EventManager.TriggerSave(); });
    }

    void LockActions(int action) {
        if(Action.FromValue<Action>(action) == Action.Fight) { 
            Buttons.Lock(saveBtn);
        } else {
            Buttons.Unlock(saveBtn);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    
    public void Hide() {
        gameObject.SetActive(false);
    }
}