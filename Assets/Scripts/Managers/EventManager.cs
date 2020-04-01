using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class EventManager : MonoBehaviour
{
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
    public delegate void ActionUpdateHandler(int action);
    public static event ActionUpdateHandler ActionUpdate;

    public static void TriggerActionUpdate(int action)
    {
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("TriggerActionUpdateRPC", RpcTarget.AllViaServer, action);
        }
        else
        {
            if (ActionUpdate != null) ActionUpdate(action);
        }
    }

    [PunRPC]
    public void TriggerActionUpdateRPC(int action)
    {
        if (ActionUpdate != null) ActionUpdate(action);

    }

    // Fired on each turn (player turn)
    public delegate void CurrentPlayerUpdateHandler(Hero hero);
    public static event CurrentPlayerUpdateHandler CurrentPlayerUpdate;
    public static void TriggerCurrentPlayerUpdate(Hero hero)
    {
        if (CurrentPlayerUpdate != null)
        {
            CurrentPlayerUpdate(hero);
        }
    }

    // Fired on hero setup
    public delegate void MainHeroInitHandler(Hero hero);
    public static event MainHeroInitHandler MainHeroInit;
    public static void TriggerMainHeroInit(Hero hero)
    {
        if (MainHeroInit != null)
        {
            MainHeroInit(hero);
        }
    }

    public delegate void FightHandler();
    public static event FightHandler Fight;
    public static void TriggerFight()
    {
        EventManager.TriggerActionUpdate(Action.Fight.Value);

        if (Fight != null)
        {
            Fight();
        }
    }

    // Fired if skip action is selected
    public delegate void SkipHandler();
    public static event SkipHandler Skip;
    public static void TriggerSkip()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("TriggerSkipRPC", RpcTarget.AllViaServer);
        }
        else
        {
            if (Skip != null) Skip();
        }
    }

    [PunRPC]
    public void TriggerSkipRPC()
    {
        EventManager.TriggerActionUpdate(Action.None.Value);
        if (Skip != null) Skip();
    }

    // Fired if skip action is selected
    public delegate void EndTurnHandler();
    public static event EndTurnHandler EndTurn;
    public static void TriggerEndTurn()
    {
        Debug.Log("Trigger End Turn");
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("TriggerEndTurnRPC", RpcTarget.AllViaServer);
        }
        else
        {
            if (EndTurn != null) EndTurn();
        }
    }

    [PunRPC]
    public void TriggerEndTurnRPC()
    {
        if (EndTurn != null) EndTurn();
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
        EventManager.TriggerActionUpdate(Action.Move.Value);

        if (MoveSelect != null)
        {
            MoveSelect();
        }
    }

    // if some functions are waiting for the event to happen, call the event.
    public delegate void CellHoverInHandler(int cellID);
    public static event CellHoverInHandler CellHoverIn;
    public static void TriggerCellHoverIn(int cellID)
    {
        if (CellHoverIn != null)
        {
            CellHoverIn(cellID);
        }
    }

    public delegate void CellHoverOutHandler();
    public static event CellHoverOutHandler CellHoverOut;
    public static void TriggerCellHoverOut()
    {
        if (CellHoverOut != null)
        {
            CellHoverOut();
        }
    }

    // Fired when the hero reached its final position after move
    public delegate void MoveHandler(Movable movable, int qty);
    public static event MoveHandler Move;
    public static void TriggerMove(Movable movable, int qty)
    {
        if (Move != null)
        {
            Move(movable, qty);
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

    // Fired when the hero reached its final position after move
    public delegate void MoveThoraldHandler();
    public static event MoveThoraldHandler MoveThorald;
    public static void TriggerMoveThorald()
    {
        EventManager.TriggerActionUpdate(Action.MoveThorald.Value);

        if (MoveThorald != null)
        {
            MoveThorald();
        }
    }

    public delegate void FarmersInventoriesUpdateHandler(int attachedFarmers, int noTargetFarmers, int detachedFarmers);
    public static event FarmersInventoriesUpdateHandler FarmersInventoriesUpdate;
    public static void TriggerFarmersInventoriesUpdate(int attachedFarmers, int noTargetFarmers, int detachedFarmers)
    {
        if (FarmersInventoriesUpdate != null)
        {
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
    public static void TriggerEndDay()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("TriggerEndDayRPC", RpcTarget.AllViaServer);
        }
        else
        {
            if (EndDay != null) EndDay();
        }
    }

    [PunRPC]
    public void TriggerEndDayRPC()
    {
        if (EndDay != null) EndDay();
    }

    // Fired when end day is triggered
    public delegate void StartDayHandler();
    public static event StartDayHandler StartDay;
    public static void TriggerStartDay()
    {
        if (StartDay != null) StartDay();
    }

    public delegate void InventoryUICellEnterHandler(CellInventory cellInventory, int index);
    public static event InventoryUICellEnterHandler InventoryUICellEnter;
    public static void TriggerInventoryUICellEnter(CellInventory cellInventory, int index)
    {
        if (InventoryUICellEnter != null)
        {
            InventoryUICellEnter(cellInventory, index);
        }
    }

    public delegate void InventoryUICellExitHandler();
    public static event InventoryUICellExitHandler InventoryUICellExit;
    public static void TriggerInventoryUICellExit()
    {
        if (InventoryUICellExit != null)
        {
            InventoryUICellExit();
        }
    }

    public delegate void InventoryUIHeroUpdateHandler(HeroInventory heroInventory);
    public static event InventoryUIHeroUpdateHandler InventoryUIHeroUpdate;
    public static void TriggerInventoryUIHeroUpdate(HeroInventory heroInventory) {
        if (InventoryUIHeroUpdate != null) {
            if (!PhotonNetwork.OfflineMode)
            {
                string playerHero = (string)PhotonNetwork.LocalPlayer.CustomProperties["Class"];
              //  if(heroInventory.parentHero.Equals(CharChoice.choice.TokenName))
                if (heroInventory.parentHero == playerHero)
                {
                    InventoryUIHeroUpdate(heroInventory);
                    Hero hero = GameManager.instance.findHero(heroInventory.parentHero);
                    TriggerCompleteHeroBoardUpdate(hero);
                }
            }
            else
            {
                InventoryUIHeroUpdate(heroInventory);
            }

        }
    }


    public delegate void InventoryUIHeroPeakHandler(HeroInventory heroInventory);
    public static event InventoryUIHeroPeakHandler InventoryUIHeroPeak;
    public static void TriggerInventoryUIHeroPeak(HeroInventory heroInventory)
    {
        if (InventoryUIHeroPeak != null)
        {
            InventoryUIHeroPeak(heroInventory);
            Hero hero = GameManager.instance.findHero(heroInventory.parentHero);
            TriggerCompleteHeroBoardUpdate(hero);
        }
    }

    public delegate void CompleteHeroBoardUpdateHandler(Hero hero);
    public static event CompleteHeroBoardUpdateHandler CompleteHeroBoardUpdate;
    public static void TriggerCompleteHeroBoardUpdate(Hero hero)
    {
        if (CompleteHeroBoardUpdate != null)
        {
            CompleteHeroBoardUpdate(hero);
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
        if (FarmerDestroyed != null)
        {
            FarmerDestroyed(farmer);
        }
    }

    public delegate void EnemyDestroyedHandler(Enemy enemy);
    public static event EnemyDestroyedHandler EnemyDestroyed;
    public static void TriggerEnemyDestroyed(Enemy enemy)
    {
        if (EnemyDestroyed != null)
        {
            EnemyDestroyed(enemy);
        }
    }

    public delegate void ShieldsUpdateHandler(int shields);
    public static event ShieldsUpdateHandler ShieldsUpdate;
    public static void TriggerShieldsUpdate(int shields)
    {
        if (ShieldsUpdate != null)
        {
            ShieldsUpdate(shields);
        }
    }




    public delegate void HeroGoldClickHandler(GoldCoin gold);
    public static event HeroGoldClickHandler heroGoldClick;
    public static void TriggerHeroGoldClick(GoldCoin gold)
    {
        if (heroGoldClick != null)
        {
            heroGoldClick(gold);
        }
    }

    public delegate void CellGoldClickHandler(GoldCoin gold);
    public static event CellGoldClickHandler cellGoldClick;
    public static void TriggerCellGoldClick(GoldCoin gold)
    {
        if (cellGoldClick != null)
        {
            cellGoldClick(gold);
        }
    }

    public delegate void DropGoldClickHandler();
    public static event DropGoldClickHandler dropGoldClick;
    public static void TriggerDropGoldClick()
    {
        if (dropGoldClick != null)
        {
            dropGoldClick();
        }
    }

    public delegate void PickWellClickHandler(Hero hero, Well well);
    public static event PickWellClickHandler pickWellClick;
    public static void TriggerPickWellClick(Hero hero, Well well)
    {
        if (pickWellClick != null)
        {
            pickWellClick(hero, well);
        }
    }

    public delegate void CellWellClickHandler(Well well);
    public static event CellWellClickHandler cellWellClick;
    public static void TriggerCellWellClick(Well well)
    {
        if (cellWellClick != null)
        {
            cellWellClick( well);
        }
    }

    public delegate void BlockOnInventoryClickHandler();
    public static event BlockOnInventoryClickHandler blockOnInventoryClick;
    public static void TriggerBlockOnInventoryClick()
    {
        if (blockOnInventoryClick != null)
        {
            blockOnInventoryClick();
        }
    }


    public delegate void DistributeGoldClickHandler(int warriorGold, int archerGold, int dwarfGold, int mageGold);
    public static event DistributeGoldClickHandler DistributeGold;
    public static void TriggerDistributeGoldClick()
    {
        GameObject distributeGoldGO = GameObject.Find("DistributeGold");
        GoldDistribution goldDistribution = distributeGoldGO.GetComponent<GoldDistribution>();
        int warriorGold = goldDistribution.getWarriorGold();
        int archerGold = goldDistribution.getArcherGold();
        int dwarfGold = goldDistribution.getDwarfGold();
        int mageGold = goldDistribution.getMageGold();
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("DistributeGoldRPC", RpcTarget.AllViaServer, warriorGold, archerGold, dwarfGold, mageGold);
        }
        else
        {
            if (DistributeGold != null)
            {
                DistributeGold(warriorGold, archerGold, dwarfGold, mageGold);
            }
        }
    }

    [PunRPC]
    public void DistributeGoldRPC(int warriorGold, int archerGold, int dwarfGold, int mageGold)
    {
        if (DistributeGold != null)
        {
            DistributeGold(warriorGold, archerGold, dwarfGold, mageGold);
        }
    }

    public delegate void DistributeWineskinsClickHandler(int warriorWineskins, int archerWineskins, int dwarfWineskins, int mageWinekins);
    public static event DistributeWineskinsClickHandler DistributeWinekins;
    public static void TriggerDistributeWineskinsClick()
    {
        GameObject distributeWineskinGO = GameObject.Find("DistributeWineskins");
        WineskinDistribution wineskinDistribution = distributeWineskinGO.GetComponent<WineskinDistribution>();
        int warriorWineskins = wineskinDistribution.getWarriorWineskins();
        int archerWineskins = wineskinDistribution.getArcherWineskins();
        int dwarfWineskins = wineskinDistribution.getDwarfWineskins();
        int mageWinekins = wineskinDistribution.getMageWineskins();
        if (!PhotonNetwork.OfflineMode)
        {
            GameManager.instance.photonView.RPC("DistributeWineskinsRPC", RpcTarget.AllViaServer, warriorWineskins, archerWineskins, dwarfWineskins, mageWinekins);
        }
        else
        {
            if (DistributeWinekins != null)
            {
                DistributeWinekins(warriorWineskins, archerWineskins, dwarfWineskins, mageWinekins);
            }
        }
    }

    [PunRPC]
    public void DistributeWineskinsRPC(int warriorWineskins, int archerWineskins, int dwarfWineskins, int mageWinekins)
    {
        if (DistributeWinekins != null)
        {
            DistributeWinekins(warriorWineskins, archerWineskins, dwarfWineskins, mageWinekins);
        }
    }

    public delegate void UpdateHeroStatsHandler(Hero hero);
    public static event UpdateHeroStatsHandler UpdateHeroStats;
    public static void TriggerUpdateHeroStats(Hero hero)
    {
        if (UpdateHeroStats != null)
        {
            UpdateHeroStats(hero);
        }
    }

    public delegate void EventCardHandler(EventCard card);
    public static event EventCardHandler EventCard;
    public static void TriggerEventCard(EventCard card)
    {
        if (EventCard != null)
        {
            EventCard(card);
        }
    }

    public delegate void BuyErrorHandler(int type);
    public static event BuyErrorHandler BuyError;
    public static void TriggerBuyError(int type)
    {
        if (BuyError != null)
        {
            BuyError(type);
        }
    }
}
