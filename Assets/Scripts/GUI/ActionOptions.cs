using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour {
    
    Button moveBtn, fightBtn, skipBtn;

    void Awake() {
        moveBtn = transform.Find("Move Button").GetComponent<Button>();
        moveBtn.onClick.AddListener(delegate { EventManager.TriggerMoveSelect(); });

        fightBtn = transform.Find("Fight Button").GetComponent<Button>();
        fightBtn.onClick.AddListener(delegate { EventManager.TriggerFightSelect(); });

        skipBtn = transform.Find("Skip Button").GetComponent<Button>();
        skipBtn.onClick.AddListener(delegate { EventManager.TriggerSkipSelect(); });
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}