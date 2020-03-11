
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    private int playerCount;
    public List<Player> players;
    public Queue<Player> playerTurn;
    public List<Hero> heroes;
    public List<Farmer> farmers;
    public List<Enemy> gors, skrals, trolls, wardraks;
    private int currentPlayerIndex = 0;
    private int mainHeroIndex = -1;
    public Token well;
    public Fog fog;
    public HeroState state;
    public LegendCards legendCards;
    public EventCards eventCards;
    public Castle castle;
    private ICommand command;
    public PhotonView photonView;
    public ActionOptions actionOptions;
    List<Enemy> monstersToMove;
    List<WellCell> wells;

    #endregion

    #region Functions [Unity]
    void Awake()
    {
        PhotonNetwork.OfflineMode = false;
        players = PhotonNetwork.PlayerList.ToList();
        base.Awake();
    }

    void OnEnable()
    {
        EventManager.MoveSelect += InitMove;
        EventManager.MoveCancel += ResetCommand;
        EventManager.MoveConfirm += ExecuteMove;
        EventManager.EnemyDestroyed += RemoveEnemy;
        EventManager.FarmerDestroyed += RemoveFarmer;
        EventManager.EndTurn += EndTurn;
        EventManager.EndDay += EndDay;
        EventManager.StartDay += StartDay;
        EventManager.MoveComplete += UpdateMonsterToMove;
        EventManager.DistributeGold += DistributeGold;
        EventManager.DistributeWinekins += DistributeWineskins;
    }

    void OnDisable()
    {
        EventManager.MoveSelect -= InitMove;
        EventManager.MoveCancel -= ResetCommand;
        EventManager.MoveConfirm -= ExecuteMove;
        EventManager.EnemyDestroyed -= RemoveEnemy;
        EventManager.FarmerDestroyed -= RemoveFarmer;
        EventManager.EndTurn -= EndTurn;
        EventManager.EndDay -= EndDay;
        EventManager.StartDay += StartDay;
        EventManager.MoveComplete -= UpdateMonsterToMove;
        EventManager.DistributeGold -= DistributeGold;
        EventManager.DistributeWinekins -= DistributeWineskins;
    }

    void RemoveEnemy(Enemy enemy)
    {
        if (typeof(Gor).IsCompatibleWith(enemy.GetType()))
        {
            gors.Remove((Gor)enemy);
        }
        else if (typeof(Skral).IsCompatibleWith(enemy.GetType()))
        {
            skrals.Remove((Skral)enemy);
        }
        else if (typeof(Troll).IsCompatibleWith(enemy.GetType()))
        {
            trolls.Remove((Troll)enemy);
        }
        else if (typeof(Wardrak).IsCompatibleWith(enemy.GetType()))
        {
            wardraks.Remove((Wardrak)enemy);
        }
    }

    void RemoveFarmer(Farmer farmer)
    {
        farmers.Remove(farmer);
    }

    void Start()
    {
        castle = Castle.Instance;
        castle.Init(players.Count);
        monstersToMove = new List<Enemy>();

        heroes = new List<Hero>();

        if (!PhotonNetwork.OfflineMode)
        {
            // Add each player's respective hero
            foreach (Player p in players)
            {
                string hero = (string)p.CustomProperties["Class"];

                if (hero != null)
                {
                    switch (hero)
                    {
                        case "Warrior":
                            heroes.Add(Warrior.Instance);
                            break;
                        case "Archer":
                            heroes.Add(Archer.Instance);
                            break;
                        case "Mage":
                            heroes.Add(Mage.Instance);
                            break;
                        case "Dwarf":
                            heroes.Add(Dwarf.Instance);
                            break;
                    }

                    string mainHero = (string)PhotonNetwork.LocalPlayer.CustomProperties["Class"];

                    if (hero.Equals(mainHero))
                    {
                        mainHeroIndex = heroes.Count - 1;
                    }
                }
            }

            playerTurn = new Queue<Player>(players);
            EventManager.TriggerMainHeroInit(MainHero);

        }
        else
        {
            heroes.Add(Warrior.Instance);
            heroes.Add(Archer.Instance);
            heroes.Add(Mage.Instance);
            heroes.Add(Dwarf.Instance);
        }

        // FARMERS
        farmers = new List<Farmer>();
        farmers.Add(Farmer.Factory(24));
        farmers.Add(Farmer.Factory(36));

        // MONSTERS
        gors = new List<Enemy>();
        gors.Add(Gor.Factory(8));
        gors.Add(Gor.Factory(20));
        gors.Add(Gor.Factory(21));
        gors.Add(Gor.Factory(26));
        gors.Add(Gor.Factory(48));

        skrals = new List<Enemy>();
        skrals.Add(Skral.Factory(19));


        trolls = new List<Enemy>();
        wardraks = new List<Enemy>();

        legendCards = new LegendCards();
        eventCards = new EventCards();

        fog = new Fog();

        //Token goldCoin;
        //goldCoin = GoldCoin.Factory();
        //heroes[0].State.heroInventory.AddGold(goldCoin);

        wells = new List<WellCell>();
        wells.Add(Cell.FromId(5) as WellCell);
        wells.Add(Cell.FromId(35) as WellCell);
        wells.Add(Cell.FromId(45) as WellCell);
        wells.Add(Cell.FromId(55) as WellCell);

        foreach (WellCell well in wells)
        {
            well.resetWell();
        }

        GiveTurn();

    }

    #endregion

    #region Functions [GameManager]
    void InitMonsterMove()
    {
        gors.Sort();
        skrals.Sort();
        wardraks.Sort();
        trolls.Sort();

        monstersToMove.AddRange(gors);
        monstersToMove.AddRange(skrals);
        monstersToMove.AddRange(wardraks);
        monstersToMove.AddRange(trolls);
        monstersToMove.AddRange(wardraks);

        MonsterMove();
    }


    void UpdateMonsterToMove(Movable movable)
    {
        if (!typeof(Enemy).IsCompatibleWith(movable.GetType())) return;
        monstersToMove.Remove((Enemy)movable);
        MonsterMove();
    }

    /*
     * Goes through a monster list and moves them in order.
     *
     */
    void MonsterMove()
    {
        if (monstersToMove.Count == 0)
        {
            GiveTurn();
            return;
        }

        bool move = false;
        while (!move && monstersToMove.Count > 0)
        {
            Enemy monster = monstersToMove[0];
            Cell nextCell = monster.Cell.enemyPath;

            while (nextCell != null && nextCell.Inventory.Enemies.Count > 0 && !Castle.IsCastle(nextCell)) nextCell = nextCell.enemyPath;

            if (nextCell != null)
            {
                monster.Move(nextCell);
                move = true;
            }
            else
            {
                monstersToMove.Remove(monster);
            }
        }
    }

    void DistributeGold(int warriorGold, int archerGold, int dwarfGold, int mageGold)
    {
        Hero warrior = heroes.Where(x => x.Type.ToString() == "Warrior").FirstOrDefault();
        if (warriorGold > 0 && warrior != null)
        {
            while (warriorGold != 0)
            {
                Token goldCoin = GoldCoin.Factory();
                warrior.State.heroInventory.AddGold(goldCoin);
                warriorGold--;
            }

        }
        Hero archer = heroes.Where(x => x.Type.ToString() == "Archer").FirstOrDefault();
        if (archerGold > 0 && archer != null)
        {
            while (archerGold != 0)
            {
                Token goldCoin = GoldCoin.Factory();
                archer.State.heroInventory.AddGold(goldCoin);
                archerGold--;
            }
        }
        Hero dwarf = heroes.Where(x => x.Type.ToString() == "Dwarf").FirstOrDefault();
        if (dwarfGold > 0 && dwarf != null)
        {
            while (dwarfGold != 0)
            {
                Token goldCoin = GoldCoin.Factory();
                dwarf.State.heroInventory.AddGold(goldCoin);
                dwarfGold--;
            }
        }
        Hero mage = heroes.Where(x => x.Type.ToString() == "Mage").FirstOrDefault();
        if (mageGold > 0 && mage != null)
        {
            while (mageGold != 0)
            {
                Token goldCoin = GoldCoin.Factory();
                mage.State.heroInventory.AddGold(goldCoin);
                mageGold--;
            }
        }

        GameObject distributeGoldGO = GameObject.Find("DistributeGold");
        if (PhotonNetwork.IsMasterClient)
        {
            distributeGoldGO.SetActive(false);
        }
    }

    void DistributeWineskins(int warriorWineskins, int archerWineskins, int dwarfWineskins, int mageWineskins)
    {
        Hero warrior = heroes.Where(x => x.Type.ToString() == "Warrior").FirstOrDefault();
        if (warriorWineskins > 0 && warrior != null)
        {
            while (warriorWineskins != 0)
            {
                Token wineskin = Wineskin.Factory();
                warrior.State.heroInventory.AddSmallToken(wineskin);
                warriorWineskins--;
            }

        }
        Hero archer = heroes.Where(x => x.Type.ToString() == "Archer").FirstOrDefault();
        if (archerWineskins > 0 && archer != null)
        {
            while (archerWineskins != 0)
            {
                Token wineskin = Wineskin.Factory();
                archer.State.heroInventory.AddSmallToken(wineskin);
                archerWineskins--;
            }
        }
        Hero dwarf = heroes.Where(x => x.Type.ToString() == "Dwarf").FirstOrDefault();
        if (dwarfWineskins > 0 && dwarf != null)
        {
            while (dwarfWineskins != 0)
            {
                Token wineskin = Wineskin.Factory();
                dwarf.State.heroInventory.AddSmallToken(wineskin);
                dwarfWineskins--;
            }
        }
        Hero mage = heroes.Where(x => x.Type.ToString() == "Mage").FirstOrDefault();
        if (mageWineskins > 0 && mage != null)
        {
            while (mageWineskins != 0)
            {
                Token wineskin = Wineskin.Factory();
                mage.State.heroInventory.AddSmallToken(wineskin);
                mageWineskins--;
            }
        }

        GameObject distributeWineskinsGO = GameObject.Find("DistributeWineskins");
        if (PhotonNetwork.IsMasterClient)
        {
            distributeWineskinsGO.SetActive(false);
        }
    }

    void StartDay()
    {
        InitMonsterMove();
        foreach (Hero h in heroes)
        {
            h.State.TimeOfDay.EndDay();
        }
        playerTurn = new Queue<Player>(players);
        Debug.Log("game manager; total players: " + players.Count);
    }

    void GiveTurn()
    {
        if (PhotonNetwork.OfflineMode || PhotonNetwork.LocalPlayer.Equals(playerTurn.Peek()))
        {
            actionOptions.Show();
        }
        else
        {
            actionOptions.Hide();
        }

        EventManager.TriggerActionUpdate(Action.None.Value);
        EventManager.TriggerCurrentPlayerUpdate(CurrentPlayer);
        state = (HeroState)CurrentPlayer.State.Clone();
    }

    void EndTurn()
    {
        CurrentPlayer.State.action = Action.None;
        playerTurn.Enqueue(playerTurn.Dequeue());
        GiveTurn();
    }

    void EndDay()
    {
        CurrentPlayer.State.action = Action.None;
        CurrentPlayer.State.resetTimeOfDay();
        playerTurn.Dequeue();
        if (playerTurn.Count() == 0)
        {
            EventManager.TriggerStartDay();
        }
        else
        {
            GiveTurn();
        }
    }

    void InitMove()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            GameObject commandGO = PhotonNetwork.Instantiate("Prefabs/Commands/MoveCommand", Vector3.zero, Quaternion.identity, 0);
            int viewId = commandGO.GetComponent<PhotonView>().ViewID;
            photonView.RPC("InitMoveRPC", RpcTarget.AllViaServer, viewId);
        }
        else
        {
            GameObject commandGO = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Commands/MoveCommand")) as GameObject;
            command = commandGO.GetComponent<MoveCommand>();
            ((MoveCommand)command).Init(CurrentPlayer);
        }
    }

    [PunRPC]
    void InitMoveRPC(int viewId)
    {
        Debug.Log("Init Move Reached");
        command = PhotonView.Find(viewId).GetComponentInParent<MoveCommand>();
        ((MoveCommand)command).Init(CurrentPlayer);
    }

    void ExecuteMove()
    {
        command.Execute();
    }

    void ResetCommand()
    {
        command.Dispose();
    }

    public Hero CurrentPlayer
    {
        get
        {
            if (!PhotonNetwork.OfflineMode)
            {
                Player currentPlayer = playerTurn.Peek();
                string playerHero = (string)currentPlayer.CustomProperties["Class"];
                return heroes.Where(x => x.Type.ToString() == playerHero).FirstOrDefault();
            }
            else
            {
                return heroes[0];
            }
        }
    }

    public Hero MainHero
    {
        get
        {
            if(mainHeroIndex == -1) return null;
            return heroes[mainHeroIndex];
        }
    }

    #endregion
}
