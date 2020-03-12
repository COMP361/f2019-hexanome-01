using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{

    Button moveBtn, fightBtn, skipBtn, endTurnBtn, endDayBtn;
    bool actionsDisable = false;

    void OnEnable() {
        EventManager.MoveSelect += LockActions;
        EventManager.MoveCancel += UnlockActions;
        EventManager.EndTurn += UnlockActions;
        EventManager.CellUpdate += LockFight; 
        EventManager.MainHeroInit += LockFight;
    }

    void OnDisable() {
        EventManager.MoveSelect -= LockActions;
        EventManager.MoveCancel -= UnlockActions;
        EventManager.EndTurn -= UnlockActions;
        EventManager.CellUpdate -= LockFight; 
        EventManager.MainHeroInit -= LockFight;
    }

    void Awake()
    {
        moveBtn = transform.Find("Move Button").GetComponent<Button>();
        moveBtn.onClick.AddListener(delegate { EventManager.TriggerMoveSelect(); });

        fightBtn = transform.Find("Fight Button").GetComponent<Button>();
        fightBtn.onClick.AddListener(delegate { EventManager.TriggerFight(); });

        skipBtn = transform.Find("Skip Button").GetComponent<Button>();
        skipBtn.onClick.AddListener(delegate { EventManager.TriggerSkip(); });

        endTurnBtn = transform.Find("End Turn Button").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(delegate { EventManager.TriggerEndTurn(); });

        endDayBtn = transform.Find("End Day Button").GetComponent<Button>();
        endDayBtn.onClick.AddListener(delegate { EventManager.TriggerEndDay(); });
    }

    void LockActions() {
        Buttons.Lock(moveBtn);
        Buttons.Lock(skipBtn);
        Buttons.Lock(fightBtn);

        actionsDisable = true;

    }

    void UnlockActions() {
        Buttons.Unlock(moveBtn);
        Buttons.Unlock(skipBtn);
        Buttons.Unlock(fightBtn);

        actionsDisable = false;
    }

    void LockFight(Token token) {
        if(GameManager.instance.MainHero == null || !GameManager.instance.MainHero.GetType().IsCompatibleWith(token.GetType())) return;
        
        if(GameManager.instance.MainHero.Cell.Inventory.Enemies.Count < 1) {
            Buttons.Lock(fightBtn);
        } else if(!actionsDisable) {
            Buttons.Unlock(fightBtn);
        }
    }

    /*void UnlockMove() {
        Buttons.Unlock(moveBtn);
    }

    void LockMove() {
        Buttons.Lock(moveBtn);
    }*/

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}