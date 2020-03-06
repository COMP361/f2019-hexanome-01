
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
    private int mainHeroIndex = 0;
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

    #endregion

    #region Functions [Unity]
    void Awake()
    {
        PhotonNetwork.OfflineMode = true;
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
    }

    void RemoveEnemy(Enemy enemy) {
        if (typeof(Gor).IsCompatibleWith(enemy.GetType())) {
            gors.Remove((Gor)enemy);
        } else if (typeof(Skral).IsCompatibleWith(enemy.GetType())) {
            skrals.Remove((Skral)enemy);
        } else if (typeof(Troll).IsCompatibleWith(enemy.GetType())) {
            trolls.Remove((Troll)enemy);
        } else if (typeof(Wardrak).IsCompatibleWith(enemy.GetType())) {
            wardraks.Remove((Wardrak)enemy);
        }
    }

    void RemoveFarmer(Farmer farmer) {
        farmers.Remove(farmer);
    }

    void Start()
    {
        castle = Castle.Instance;
        castle.Init(players.Count);
        monstersToMove = new List<Enemy>();

        heroes = new List<Hero>();

        if (!PhotonNetwork.OfflineMode) {
            // Add each player's respective hero
            foreach(Player p in players) {
                string hero = (string)p.CustomProperties["Class"];

                if(hero != null) {
                    switch (hero) {
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

                    if(hero.Equals(mainHero)) {
                        mainHeroIndex = heroes.Count - 1;
                    }
                }
            }

            playerTurn = new Queue<Player>(players);
            EventManager.TriggerMainHeroInit(MainHero);

        } else {
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

    //    Token goldCoin;
  //      goldCoin = GoldCoin.Factory();
    //    heroes[0].State.heroInventory.AddGold(goldCoin);

        //well = new Token();
        //well.addToken(55, Color.blue);
        //well.addToken(35, Color.blue);
        //well.addToken(5, Color.blue);
        //well.addToken(45, Color.blue);

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
    void MonsterMove() {
        if(monstersToMove.Count == 0) {
            GiveTurn();
            return;
        }

        bool move = false;
        while (!move && monstersToMove.Count > 0) {
            Enemy monster = monstersToMove[0];
            Cell nextCell = monster.Cell.enemyPath;

            while (nextCell != null && nextCell.Inventory.Enemies.Count > 0 && !Castle.IsCastle(nextCell)) nextCell = nextCell.enemyPath;

            if(nextCell != null) {
                monster.Move(nextCell);
                move = true;
            } else {
                monstersToMove.Remove(monster);
            }
        }
    }


    void StartDay() {
        InitMonsterMove();
        playerTurn = new Queue<Player>(players);
    }

    void GiveTurn()
    {
        if (PhotonNetwork.OfflineMode || PhotonNetwork.LocalPlayer.Equals(playerTurn.Peek())) {
            actionOptions.Show();
        } else {
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
        playerTurn.Dequeue();
        if (playerTurn.Count() == 0) {
            EventManager.TriggerStartDay();
        } else {
            GiveTurn();
        }
    }

    void InitMove()
    {
        if(!PhotonNetwork.OfflineMode) {
            GameObject commandGO = PhotonNetwork.Instantiate("Prefabs/Commands/MoveCommand", Vector3.zero, Quaternion.identity, 0);
            int viewId = commandGO.GetComponent<PhotonView>().ViewID;
            photonView.RPC("InitMoveRPC", RpcTarget.AllViaServer, viewId);
        } else {
            GameObject commandGO = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/Commands/MoveCommand")) as GameObject;
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
        get {
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
        get {
            return heroes[mainHeroIndex];
        }
    }

    #endregion
}
