using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{
    Button moveBtn, fightBtn, skipBtn, moveThoraldBtn, endTurnBtn, endDayBtn;
    
    void OnEnable() {
        EventManager.ActionUpdate += LockActions;
        EventManager.MoveConfirm += UnlockEndTurn;
    }

    void OnDisable() {
        EventManager.ActionUpdate -= LockActions;
        EventManager.MoveConfirm -= UnlockEndTurn;
    }

    void Awake() {
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

    void UnlockEndTurn() {
        Buttons.Unlock(endTurnBtn);
    }

    void LockActions(int action) {
        if(Action.FromValue<Action>(action) == Action.None) {
            Buttons.Lock(endTurnBtn);
        } else if(Action.FromValue<Action>(action) != Action.Move){
            Buttons.Unlock(endTurnBtn);
        }

        if(Action.FromValue<Action>(action) == Action.None && GameManager.instance.MainHero.timeline.HasHoursLeft()) { 
            Buttons.Unlock(fightBtn);
            Buttons.Unlock(skipBtn);
        } else {
            Buttons.Lock(fightBtn);
            Buttons.Lock(skipBtn);
        }

        if(Action.FromValue<Action>(action) == Action.None && (
            GameManager.instance.MainHero.timeline.HasHoursLeft() || GameManager.instance.MainHero.heroInventory.HasSmallToken(typeof(Wineskin))
        )) {
            Buttons.Unlock(moveBtn);
        } else {
            Buttons.Lock(moveBtn);
        }

        if(Action.FromValue<Action>(action) == Action.None && GameManager.instance.thorald != null && (
            GameManager.instance.MainHero.timeline.HasHoursLeft() || GameManager.instance.MainHero.heroInventory.HasSmallToken(typeof(Wineskin))
        )) {
            Buttons.Unlock(moveThoraldBtn);
        } else {
            Buttons.Lock(moveThoraldBtn);
        }
        
        if(GameManager.instance.MainHero.GetAttackableCells().Count < 1) {
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