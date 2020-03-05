using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EventManager {
   // this class helps to register the events that are happening and trigger all the functions that are linked to this event.
    
    // Fired when a cell is clicked
    public delegate void CellClickHandler(int cellID);
    public static event CellClickHandler CellClick;
    public static void TriggerCellClick(int cellID)
    {
        if (CellClick != null)
        {
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

    // Fired when we confirm the move action
    public delegate void ClearPathHandler();
    public static event ClearPathHandler ClearPath;
    public static void TriggerClearPath()
    {
        if (ClearPath != null)
        {
            ClearPath();
        }
    }

    public delegate void PathUpdateHandler(int count);
    public static event PathUpdateHandler PathUpdate;
    public static void TriggerPathUpdate(int count)
    {
        if (PathUpdate != null)
        {
            PathUpdate(count);
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

    // if some functions are waiting for the event to happen, call the event.
    public delegate void CellHoverInHandler(int cellID);
    public static event CellHoverInHandler CellHoverIn;
    public static void TriggerCellHoverIn(int cellID) {
        if (CellHoverIn != null) {
            CellHoverIn(cellID);
        }
    }

    public delegate void CellHoverOutHandler();
    public static event CellHoverOutHandler CellHoverOut;
    public static void TriggerCellHoverOut() {
        if (CellHoverOut != null) {
            CellHoverOut();
        }
    }

    // Fired when the hero reached its final position after move
    public delegate void MoveStartHandler(Movable movable);
    public static event MoveStartHandler MoveStart;
    public static void TriggerMoveStart(Movable movable)
    {
        if (MoveStart != null) {
            MoveStart(movable);
        }
    }

    // Fired when the hero reached its final position after move
    public delegate void MoveCompleteHandler(Movable movable);
    public static event MoveCompleteHandler MoveComplete;
    public static void TriggerMoveComplete(Movable movable)
    {
        if (MoveComplete != null)
        {
            MoveComplete(movable);
        }
    }

    public delegate void FarmersInventoriesUpdateHandler(int attachedFarmers, int noTargetFarmers, int detachedFarmers);
    public static event FarmersInventoriesUpdateHandler FarmersInventoriesUpdate;
    public static void TriggerFarmersInventoriesUpdate(int attachedFarmers, int noTargetFarmers, int detachedFarmers)
    {
        if (FarmersInventoriesUpdate != null) {
            FarmersInventoriesUpdate(attachedFarmers, noTargetFarmers, detachedFarmers);
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
            InventoryUICellEnter(cellInventory, index);
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
        if (GameOver != null)
        {
            GameOver();
        }
    }

    public delegate void CellUpdateHandler(Token token);
    public static event CellUpdateHandler CellUpdate;
    public static void TriggerCellUpdate(Token token)
    {
        if (CellUpdate != null)
        {
            CellUpdate(token);
        }
    }

    public delegate void FarmerDestroyedHandler(Farmer farmer);
    public static event FarmerDestroyedHandler FarmerDestroyed;
    public static void TriggerFarmerDestroyed(Farmer farmer)
    {
        if (FarmerDestroyed != null) {
            FarmerDestroyed(farmer);
        }
    }

    public delegate void EnemyDestroyedHandler(Enemy enemy);
    public static event EnemyDestroyedHandler EnemyDestroyed;
    public static void TriggerEnemyDestroyed(Enemy enemy)
    {
        if (EnemyDestroyed != null) {
            EnemyDestroyed(enemy);
        }
    }

    public delegate void ShieldsUpdateHandler(int shields);
    public static event ShieldsUpdateHandler ShieldsUpdate;
    public static void TriggerShieldsUpdate(int shields) {
        if (ShieldsUpdate != null) {
            ShieldsUpdate(shields);
        }
    }
}