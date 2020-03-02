using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager {
    //GameManager gm;

    // this class helps to register the events that are happening and trigger all the functions that are linked to this event.


  //  public delegate void PlayerUpdateHandler(Hero hero);
  //  public static event PlayerUpdateHandler PlayerUpdate;

  //  public delegate void ActionUpdateHandler(Action action);
  //  public static event ActionUpdateHandler ActionUpdate;

  //  public delegate void MoveSelectHandler();
//    public static event MoveSelectHandler MoveSelect;

//    public delegate void SkipSelectHandler();
//    public static event SkipSelectHandler SkipSelect;

  //  public delegate void FightSelectHandler();
  //  public static event FightSelectHandler FightSelect;

    public delegate void CellClickHandler(int cellID);
    public static event CellClickHandler CellClick;

  //  public delegate void MoveConfirmHandler();
  //  public static event MoveConfirmHandler MoveConfirm;

//    public delegate void MoveCancelHandler();
//    public static event MoveCancelHandler MoveCancel;

//    public delegate void StartTurnHandler();
//    public static event StartTurnHandler StartTurn;

//    public delegate void EndTurnHandler();
  //  public static event EndTurnHandler EndTurn;

    //
    public delegate void CellHoverInHandler(int cellID);
    public static event CellHoverInHandler CellHoverIn;

    public delegate void CellHoverOutHandler();
    public static event CellHoverOutHandler CellHoverOut;

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

    public static void TriggerCellClick(int cellID) {
        if (CellClick != null) {
            CellClick(cellID);
        }
    }

    // Fired when a new action is chosen (Fight/Skip/Move/End day)
    public delegate void ActionUpdateHandler(Action action);
    public static event ActionUpdateHandler ActionUpdate;
    public static void TriggerActionUpdate(Action action)
    {
        if (ActionUpdate != null)
        {
            ActionUpdate(action);
        }
    }

    // Fired on each turn (player turn)
    public delegate void PlayerUpdateHandler(Hero hero);
    public static event PlayerUpdateHandler PlayerUpdate;
    public static void TriggerPlayerUpdate(Hero hero)
    {
        if (PlayerUpdate != null)
        {
            PlayerUpdate(hero);
        }
    }

  /*  public static void TriggerMoveSelect() {
        EventManager.TriggerActionUpdate(Action.Move);

        if (MoveSelect != null) {
            MoveSelect();
        }
    }
*/
    public delegate void FightSelectHandler();
    public static event FightSelectHandler FightSelect;
    public static void TriggerFightSelect()
    {
        EventManager.TriggerActionUpdate(Action.Fight);

        if (FightSelect != null)
        {
            FightSelect();
        }
    }

    // Fired if skip action is selected
    public delegate void SkipSelectHandler();
    public static event SkipSelectHandler SkipSelect;
    public static void TriggerSkipSelect()
    {
        EventManager.TriggerActionUpdate(Action.Skip);

        if (SkipSelect != null)
        {
            SkipSelect();
        }
    }

    // Fired at the beginning of turn
    public delegate void StartTurnHandler();
    public static event StartTurnHandler StartTurn;
    public static void TriggerStartTurn()
    {
        if (StartTurn != null)
        {
            StartTurn();
        }
    }

    // Fired at the end of turn
    public delegate void EndTurnHandler();
    public static event EndTurnHandler EndTurn;
    public static void TriggerEndTurn()
    {
        if (EndTurn != null)
        {
            EndTurn();
        }
    }

    // Fired when we enter a cell with mouse
    public delegate void CellMouseEnterHandler(int cellID);
    public static event CellMouseEnterHandler CellMouseEnter;
    public static void TriggerCellMouseEnter(int cellID)
    {
        if (CellMouseEnter != null)
        {
            CellMouseEnter(cellID);
        }
    }

    // Fired when we leave a cell with mouse
    public delegate void CellMouseLeaveHandler(int cellID);
    public static event CellMouseLeaveHandler CellMouseLeave;
    public static void TriggerCellMouseLeave(int cellID)
    {
        if (CellMouseLeave != null)
        {
            CellMouseLeave(cellID);
        }
    }

    // Fired when we enter a merchant cell with mouse
    public delegate void MerchCellMouseEnterHandler(int cellID);
    public static event MerchCellMouseEnterHandler MerchCellMouseEnter;
    public static void TriggerMerchCellMouseEnter(int cellID)
    {
        if (MerchCellMouseEnter != null)
        {
            MerchCellMouseEnter(cellID);
        }
    }

    // Fired when we enter a merchant cell with mouse
    public delegate void MerchCellMouseLeaveHandler(int cellID);
    public static event MerchCellMouseEnterHandler MerchCellMouseLeave;
    public static void TriggerMerchCellMouseLeave(int cellID)
    {
        if (MerchCellMouseLeave != null)
        {
            MerchCellMouseLeave(cellID);
        }
    }

    // Fired when we cancel the move action before confirming
    public delegate void MoveCancelHandler();
    public static event MoveCancelHandler MoveCancel;
    public static void TriggerMoveCancel()
    {
        EventManager.TriggerActionUpdate(Action.None);

        if (MoveCancel != null)
        {
            MoveCancel();
        }
    }

    // Fired when we confirm the move action
    public delegate void MoveConfirmHandler();
    public static event MoveConfirmHandler MoveConfirm;
    public static void TriggerMoveConfirm()
    {
        if (MoveConfirm != null)
        {
            MoveConfirm();
        }
    }

    // Fired when we select the move action (Move button)
    public delegate void MoveSelectHandler();
    public static event MoveSelectHandler MoveSelect;
    public static void TriggerMoveSelect()
    {
        EventManager.TriggerActionUpdate(Action.Move);

        if (MoveSelect != null)
        {
            MoveSelect();
        }
    }

/*
    public static void TriggerEndTurn() {
        if (EndTurn != null) {
            EndTurn();
        }
    } */

    // if some functions are waiting for the event to happen, call the event.
    public static void TriggerCellHoverIn(int cellID) {
        if (CellHoverIn != null) {
            CellHoverIn(cellID);
        }
    }

    public static void TriggerCellHoverOut() {
        if (CellHoverOut != null) {
            CellHoverOut();
        }
      }
    public delegate void MoveCompleteHandler(Movable movable);
    public static event MoveCompleteHandler MoveComplete;
    public static void TriggerMoveComplete(Movable movable)
    {
        if (MoveComplete != null)
        {
            MoveComplete(movable);
        }
    }

    // Fired when a farmer is on the final position
    public delegate void FarmerOnCellHandler();
    public static event FarmerOnCellHandler FarmerOnCell;
    public static void TriggerFarmerOnCell()
    {
        if (FarmerOnCell != null)
        {
            FarmerOnCell();
        }
    }

    // Fired when a farmer is picked up
    public delegate void PickFarmerHandler();
    public static event PickFarmerHandler PickFarmer;
    public static void TriggerPickFarmer()
    {
        if (PickFarmer != null)
        {
            PickFarmer();
        }
    }

    // Fired when a farmer is dropped
    public delegate void DropFarmerHandler();
    public static event DropFarmerHandler DropFarmer;
    public static void TriggerDropFarmer()
    {
        if (DropFarmer != null)
        {
            DropFarmer();
        }
    }

    // Fired when end day is triggered
    public delegate void EndDayHandler();
    public static event EndDayHandler EndDay;
    public static void TriggerEndDaySelect() {
          if (EndDay != null) {
            EndDay();
          }
    }

    public delegate void InventoryUICellEnterHandler(CellInventory cellInventory, int index);
    public static event InventoryUICellEnterHandler InventoryUICellEnter;
    public static void TriggerInventoryUICellEnter(CellInventory cellInventory, int index) {
          if (InventoryUICellEnter != null) {
            InventoryUICellEnter(cellInventory,index);
          }
    }

    public delegate void InventoryUICellExitHandler();
    public static event InventoryUICellExitHandler InventoryUICellExit;
    public static void TriggerInventoryUICellExit() {
          if (InventoryUICellExit != null) {
            InventoryUICellExit();
          }
    }

    public delegate void GameOverHandler();
    public static event GameOverHandler GameOver;
    public static void TriggerGameOver()
    {
        if (EndDay != null)
        {
            GameOver();
        }
    }


}
