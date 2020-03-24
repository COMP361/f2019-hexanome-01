using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{

    Button moveBtn, fightBtn, skipBtn, moveThoraldBtn, endTurnBtn, endDayBtn;
    bool actionsDisabled = false;
    bool fightDisabled = false;

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

        moveThoraldBtn = transform.Find("Move Thorald Button").GetComponent<Button>();
        moveThoraldBtn.onClick.AddListener(delegate { EventManager.TriggerMoveThorald(); });

        endTurnBtn = transform.Find("End Turn Button").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(delegate { EventManager.TriggerEndTurn(); });

        endDayBtn = transform.Find("End Day Button").GetComponent<Button>();
        endDayBtn.onClick.AddListener(delegate { EventManager.TriggerEndDay(); });
    }

    void LockActions() {
        Buttons.Lock(moveBtn);
        Buttons.Lock(skipBtn);
        Buttons.Lock(fightBtn);

        actionsDisabled = true;

    }

    void UnlockActions() {
        Buttons.Unlock(moveBtn);
        Buttons.Unlock(skipBtn);
        //if(!fightDisabled) 
        Buttons.Unlock(fightBtn);

        actionsDisabled = false;
    }

    void LockFight(Token token) {
        /*if(GameManager.instance.MainHero == null || !GameManager.instance.MainHero.GetType().IsCompatibleWith(token.GetType())) return;
        
        if(GameManager.instance.MainHero.Cell.Inventory.Enemies.Count < 1) {
            Buttons.Lock(fightBtn);
            fightDisabled = true;
        } else {
            if(!actionsDisabled) Buttons.Unlock(fightBtn);
            fightDisabled = false;
        }*/
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