using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{

    Button moveBtn, fightBtn, skipBtn, endDayBtn;

    void OnEnable() {
        EventManager.MoveSelect += LockMove;
        EventManager.MoveCancel += UnlockMove;
        //EventManager.MoveConfirm += UnlockMove;
    }

    void OnDisable() {
        EventManager.MoveSelect -= LockMove;
        EventManager.MoveCancel -= UnlockMove;
        //EventManager.MoveConfirm -= UnlockMove;
    }

    void Awake()
    {
        moveBtn = transform.Find("Move Button").GetComponent<Button>();
        moveBtn.onClick.AddListener(delegate { EventManager.TriggerMoveSelect(); });

        fightBtn = transform.Find("Fight Button").GetComponent<Button>();
        fightBtn.onClick.AddListener(delegate { EventManager.TriggerFightSelect(); });

        skipBtn = transform.Find("Skip Button").GetComponent<Button>();
        skipBtn.onClick.AddListener(delegate { EventManager.TriggerSkipSelect(); });

        endDayBtn = transform.Find("End Day Button").GetComponent<Button>();
        endDayBtn.onClick.AddListener(delegate { EventManager.TriggerEndDaySelect(); });
    }

    void UnlockMove() {
        Buttons.Unlock(moveBtn);
    }

    void LockMove() {
        Buttons.Lock(moveBtn);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}