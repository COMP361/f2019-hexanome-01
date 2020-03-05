
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    private int playerCount;
    public List<Player> players;
    public List<Hero> heroes;
    public List<Farmer> farmers;
    public List<Enemy> gors, skrals, trolls, wardraks;
    private int currentPlayerIndex = -1;
    public Token well;
    public Fog fog;
    public HeroState state;
    public LegendCards legendCards;
    public EventCards eventCards;
    public Castle castle;
    private ICommand command;
    public PhotonView photonView;
    List<Enemy> monstersToMove;

    #endregion

    #region Functions [Unity]
    void Awake()
    {
        //PhotonNetwork.OfflineMode = true;
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
        EventManager.EndDay += MonsterEndDayEvents;
        EventManager.MoveComplete += UpdateMonsterToMove;
    }

    void OnDisable()
    {
        EventManager.MoveSelect -= InitMove;
        EventManager.MoveCancel -= ResetCommand;
        EventManager.MoveConfirm -= ExecuteMove;
        EventManager.EnemyDestroyed -= RemoveEnemy;
        EventManager.FarmerDestroyed -= RemoveFarmer;
        EventManager.EndDay -= MonsterEndDayEvents;
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
        heroes.Add(Warrior.Instance);
        heroes.Add(Archer.Instance);
        heroes.Add(Mage.Instance);
        heroes.Add(Dwarf.Instance);
        
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

        //well = new Token();
        //well.addToken(55, Color.blue);
        //well.addToken(35, Color.blue);
        //well.addToken(5, Color.blue);
        //well.addToken(45, Color.blue);

        giveTurn(0);

    }

    #endregion

    #region Functions [GameManager]
    void MonsterEndDayEvents()
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

        monsterMove();
    }


    void UpdateMonsterToMove(Movable movable)
    {
        if (!typeof(Enemy).IsCompatibleWith(movable.GetType())) return;
        monstersToMove.Remove((Enemy)movable);
        monsterMove();
    }
    
    /*
     * Goes through a monster list and moves them in order.
     *
     */
    void monsterMove() {
        if(monstersToMove.Count == 0) return;
        
        bool move = false;
        while (!move && monstersToMove.Count > 0) {
            Enemy monster = monstersToMove[0];
            Cell nextCell = monster.Cell.enemyPath;

            Debug.Log(monster.Cell);
            Debug.Log(nextCell);

            while (nextCell != null && nextCell.Inventory.Enemies.Count > 0 && !Castle.IsCastle(nextCell)) nextCell = nextCell.enemyPath;

            if(nextCell != null) {
                monster.Move(nextCell);
                move = true;
            } else {
                monstersToMove.Remove(monster);
            }
        }
    }

    void giveTurn(int playerIndex)
    {
        currentPlayerIndex = playerIndex;
        EventManager.TriggerActionUpdate(Action.None);
        EventManager.TriggerPlayerUpdate(CurrentPlayer);
        EventManager.TriggerCurrentPlayerUpdate(CurrentPlayer);
        state = (HeroState)CurrentPlayer.State.Clone();
    }

    void endTurn(int playerIndex)
    {
        CurrentPlayer.State.action = Action.None;
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
            return heroes[currentPlayerIndex];
        }
    }

    #endregion
}