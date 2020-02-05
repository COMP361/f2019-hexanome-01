using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager
{
    //GameManager gm;

    public delegate void PlayerUpdateHandler(Hero hero);
    public static event PlayerUpdateHandler PlayerUpdate;

    public delegate void ActionUpdateHandler(Action action);
    public static event ActionUpdateHandler ActionUpdate;

    public delegate void MoveSelectHandler();

    public static event MoveSelectHandler MoveSelect;

    public delegate void SkipSelectHandler();
    public static event SkipSelectHandler SkipSelect;

    public delegate void FightSelectHandler();
    public static event FightSelectHandler FightSelect;

    public delegate void CellClickHandler(int cellID);
    public static event CellClickHandler CellClick;

    public delegate void MoveConfirmHandler();
    public static event MoveConfirmHandler MoveConfirm;

    public delegate void MoveCancelHandler();
    public static event MoveCancelHandler MoveCancel;

    public delegate void StartTurnHandler();
    public static event StartTurnHandler StartTurn;

    public delegate void EndTurnHandler();
    public static event EndTurnHandler EndTurn;


    public delegate void EndDayHandler();
    // This is the publisher to which others can subscribe 
    public static event EndDayHandler EndDay;

    /*void Start() {
        gm = GameManager.instance;
    }*/

    /*void Update() {
        // Check if click is not over a UI element
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(hit.collider != null) {
                hit.collider.gameObject.GetComponent<IClickHandler>().OnClick();
            }
        }
    }*/

    public static void TriggerEndDaySelect()
    {
        //Debug.Log(" THE TRIGGER END DAY SELECT WORKS ");
        if (EndDay != null)
        {
            EndDay();
        }
    }

    public static void TriggerCellClick(int cellID)
    {
        if (CellClick != null)
        {
            CellClick(cellID);
        }
    }

    public static void TriggerActionUpdate(Action action)
    {
        if (ActionUpdate != null)
        {
            ActionUpdate(action);
        }
    }

    public static void TriggerPlayerUpdate(Hero hero)
    {
        if (PlayerUpdate != null)
        {
            PlayerUpdate(hero);
        }
    }

    public static void TriggerMoveSelect()
    {
        EventManager.TriggerActionUpdate(Action.Move);

        if (MoveSelect != null)
        {
            MoveSelect();
        }
    }

    public static void TriggerFightSelect()
    {
        EventManager.TriggerActionUpdate(Action.Fight);

        if (FightSelect != null)
        {
            FightSelect();
        }
    }

    public static void TriggerSkipSelect()
    {
        EventManager.TriggerActionUpdate(Action.Skip);

        if (SkipSelect != null)
        {
            SkipSelect();
        }
    }

    public static void TriggerMoveCancel()
    {
        EventManager.TriggerActionUpdate(Action.None);

        if (MoveCancel != null)
        {
            MoveCancel();
        }
    }

    public static void TriggerMoveConfirm()
    {
        if (MoveConfirm != null)
        {
            MoveConfirm();
        }
    }

    public static void TriggerStartTurn()
    {
        if (StartTurn != null)
        {
            StartTurn();
        }
    }

    public static void TriggerEndTurn()
    {
        if (EndTurn != null)
        {
            EndTurn();
        }
    }
}