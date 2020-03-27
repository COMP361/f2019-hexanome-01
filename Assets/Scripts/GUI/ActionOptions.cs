using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{
    Button moveBtn, fightBtn, skipBtn, moveThoraldBtn, endTurnBtn, endDayBtn;
    void OnEnable() {
        EventManager.ActionUpdate += LockActions;
    }

    void OnDisable() {
        EventManager.ActionUpdate -= LockActions;
    }
    void Awake()
    {
        moveBtn = transform.Find("Move Button").GetComponent<Button>();
        moveBtn.onClick.AddListener(delegate { EventManager.TriggerMoveSelect(); });

        fightBtn = transform.Find("Fight Button").GetComponent<Button>();
        fightBtn.onClick.AddListener(delegate { EventManager.TriggerFight(); });

        skipBtn = transform.Find("Skip Button").GetComponent<Button>();
        skipBtn.onClick.AddListener(delegate { EventManager.TriggerSkip(); });

        moveThoraldBtn = transform.Find("Move Thorald Button").GetComponent<Button>();
        moveThoraldBtn.onClick.AddListener(delegate { EventManager.TriggerMoveThorald(); });

        endTurnBtn = transform.Find("End Turn Button").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(delegate { EventManager.TriggerEndTurn(); });

        endDayBtn = transform.Find("End Day Button").GetComponent<Button>();
        endDayBtn.onClick.AddListener(delegate { EventManager.TriggerEndDay(); });
    }

    void LockActions(int action) {
        if(!GameManager.instance.MainHero.timeline.HasHoursLeft()) {
            Buttons.Lock(moveBtn);
            Buttons.Lock(fightBtn);
            Buttons.Lock(skipBtn);
            Buttons.Lock(moveThoraldBtn);
            Buttons.Lock(endTurnBtn);
        } else if(Action.FromValue<Action>(action) == Action.None) { 
            Buttons.Unlock(moveBtn);
            Buttons.Unlock(fightBtn);
            Buttons.Unlock(skipBtn);
            Buttons.Unlock(moveThoraldBtn);
            Buttons.Lock(endTurnBtn);
        } else {
            Buttons.Lock(moveBtn);
            Buttons.Lock(fightBtn);
            Buttons.Lock(skipBtn);
            Buttons.Lock(moveThoraldBtn);
            Buttons.Unlock(endTurnBtn);
        }

        if(GameManager.instance.MainHero.Cell.Inventory.Enemies.Count < 1) {
            Buttons.Lock(fightBtn);
        }
    }
    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}