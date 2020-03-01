using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager {
    public delegate void CellClickHandler(int cellID);
    public static event CellClickHandler CellClick;
    public static void TriggerCellClick(int cellID) {
        if (CellClick != null) {
            CellClick(cellID);
        }
    }

    public delegate void ActionUpdateHandler(Action action);
    public static event ActionUpdateHandler ActionUpdate;
    public static void TriggerActionUpdate(Action action) {
        if (ActionUpdate != null) {
            ActionUpdate(action);
        }
    }

    public delegate void PlayerUpdateHandler(Hero hero);
    public static event PlayerUpdateHandler PlayerUpdate;
    public static void TriggerPlayerUpdate(Hero hero) {
        if (PlayerUpdate != null) {
            PlayerUpdate(hero);
        }
    }

    public delegate void FightSelectHandler();
    public static event FightSelectHandler FightSelect;
    public static void TriggerFightSelect() {

        EventManager.TriggerActionUpdate(Action.Fight);

        if (FightSelect != null)
        {
            FightSelect();
        }
    }

    public delegate void SkipSelectHandler();
    public static event SkipSelectHandler SkipSelect;
    public static void TriggerSkipSelect() {
        EventManager.TriggerActionUpdate(Action.Skip);

        if (SkipSelect != null)
        {
            SkipSelect();
        }
    }

    public delegate void StartTurnHandler();
    public static event StartTurnHandler StartTurn;
    public static void TriggerStartTurn() {
        if (StartTurn != null) {
            StartTurn();
        }
    }

    public delegate void EndTurnHandler();
    public static event EndTurnHandler EndTurn;
    public static void TriggerEndTurn() {
        if (EndTurn != null) {
            EndTurn();
        }
    }

    public delegate void CellMouseEnterHandler(int cellID);
    public static event CellMouseEnterHandler CellMouseEnter;
    public static void TriggerCellMouseEnter(int cellID) {
        if (CellMouseEnter != null) {
            CellMouseEnter(cellID);
        }
    }

    public delegate void CellMouseLeaveHandler(int cellID);
    public static event CellMouseLeaveHandler CellMouseLeave;
    public static void TriggerCellMouseLeave(int cellID) {
        if (CellMouseLeave != null) {
            CellMouseLeave(cellID);
        }
    }

    public delegate void MoveCancelHandler();
    public static event MoveCancelHandler MoveCancel;
    public static void TriggerMoveCancel() {
        EventManager.TriggerActionUpdate(Action.None);

        if (MoveCancel != null)
        {
            MoveCancel();
        }
    }

    public delegate void MoveConfirmHandler();
    public static event MoveConfirmHandler MoveConfirm;
    public static void TriggerMoveConfirm() {
        if (MoveConfirm != null) {
            MoveConfirm();
        }
    }

    public delegate void MoveSelectHandler();
    public static event MoveSelectHandler MoveSelect;
    public static void TriggerMoveSelect() {
        EventManager.TriggerActionUpdate(Action.Move);

        if (MoveSelect != null) {
            MoveSelect();
        }
    }

    public delegate void MoveCompleteHandler(Movable movable);
    public static event MoveCompleteHandler MoveComplete;
    public static void TriggerMoveComplete(Movable movable) {
        if (MoveComplete != null) {
            MoveComplete(movable);
        }
    }

    public delegate void FarmerOnCellHandler();
    public static event FarmerOnCellHandler FarmerOnCell;
    public static void TriggerFarmerOnCell() {
        if (FarmerOnCell != null) {
            FarmerOnCell();
        }
    }

    public delegate void PickFarmerHandler();
    public static event PickFarmerHandler PickFarmer;
    public static void TriggerPickFarmer() {
        if (PickFarmer != null) {
            PickFarmer();
        }
    }

    public delegate void DropFarmerHandler();
    public static event DropFarmerHandler DropFarmer;
    public static void TriggerDropFarmer() {
        if (DropFarmer != null) {
            DropFarmer();
        }
    }

    public delegate void EndDayHandler();
    public static event EndDayHandler EndDay;
    public static void TriggerEndDaySelect() {
          if (EndDay != null) {
            EndDay();   
          }
    }
}
