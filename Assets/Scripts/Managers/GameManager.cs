
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

    private ICommand command;
    public PhotonView photonView;
    List<Enemy> monstersToMove;

    bool IsCastle(Cell cell) {
        return cell.Index == 0;
    }
    #endregion

    #region Functions [Unity]
    void Awake()
    {
        //SceneManager.LoadScene("Map", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Chat", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Tokens", LoadSceneMode.Additive);
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        players = PhotonNetwork.PlayerList.ToList();
        base.Awake();
    }

    void OnEnable()
    {
        EventManager.MoveSelect += InitMove;
        EventManager.MoveCancel += ResetCommand;
        EventManager.MoveConfirm += ExecuteMove;
    }

    void OnDisable()
    {
        EventManager.MoveSelect -= InitMove;
        EventManager.MoveCancel -= ResetCommand;
        EventManager.MoveConfirm -= ExecuteMove;
    }

    void Start()
    {
        monstersToMove = new List<Enemy>();
        // PLAYERS
        playerCount = 1;
        heroes = new List<Hero>();
        heroes.Add(Warrior.Instance);
        heroes.Add(Archer.Instance);
        heroes.Add(Mage.Instance);
        heroes.Add(Dwarf.Instance);
        Cell.FromId(0).State.initGoldenShields(players.Count);

        // FARMERS
        farmers = new List<Farmer>();
        farmers.Add(Farmer.Factory(24));
        farmers.Add(Farmer.Factory(36));

        // MONSTERS
        gors = new List<Enemy>();
        //gors.Add(Gor.Factory(1));
        gors.Add(Gor.Factory(2));
        gors.Add(Gor.Factory(19));
        //gors.Add(Gor.Factory(20));
        gors.Add(Gor.Factory(48));
        gors.Add(Gor.Factory(84));

        skrals = new List<Enemy>();
        //skrals.Add(Skral.Factory(19));

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
        EventManager.EndDay += MonsterEndDayEvents;
        EventManager.MoveComplete += UpdateMonsterToMove;

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

        Debug.Log(monstersToMove.Count);

        monsterMove();
    }


    void UpdateMonsterToMove(Movable movable) {
        if(!typeof(Enemy).IsCompatibleWith(movable.GetType())) return;
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
        //foreach (var monster in enemy) {
        while(!move && monstersToMove.Count > 0) {
            Enemy monster = monstersToMove[0];
            Cell nextCell = monster.Cell.enemyPath;
            while (nextCell != null && nextCell.State.Enemies.Count > 0 && nextCell.Index != 0) nextCell = nextCell.enemyPath;
            if(nextCell != null) {
                monster.Move(nextCell);
                if (IsCastle(nextCell) && nextCell.State.decrementGoldenShields() == -1) { EventManager.TriggerGameOver(); }
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
        state = (HeroState)CurrentPlayer.State.Clone();
    }

    void endTurn(int playerIndex)
    {
        CurrentPlayer.IsDone = false;
        CurrentPlayer.State.action = Action.None;
    }

    void InitMove()
    {
        GameObject commandGO = PhotonNetwork.InstantiateSceneObject("Prefabs/Commands/MoveCommand", Vector3.zero, Quaternion.identity, 0);
        int viewId = commandGO.GetComponent<PhotonView>().ViewID;
        photonView.RPC("ReceiveInitMove", RpcTarget.AllBuffered, viewId);
        //command = new MoveCommand(CurrentPlayer);
    }

    [PunRPC]
    void ReceiveInitMove(int viewId)
    {
        Debug.Log("Init Move Reached");

        //if (PhotonNetwork.isMasterClient) {
        //GameObject commandGO = PhotonNetwork.InstantiateSceneObject("Prefabs/Commands/MoveCommand", Vector3.zero, Quaternion.identity, 0);
        //int viewId = commandGO.GetComponent<PhotonView>().ViewID;
        command = PhotonView.Find(viewId).GetComponentInParent<MoveCommand>();
        ((MoveCommand)command).Init(CurrentPlayer);
        //}
    }

    void ExecuteMove()
    {
        //command.Execute();
        photonView.RPC("ReceiveExecuteMove", RpcTarget.AllBuffered);
        //command.Dispose();
        //command = new MoveCommand(CurrentPlayer.Token, CurrentPlayer.State.cell);
    }

    [PunRPC]
    void ReceiveExecuteMove()
    {
        Debug.Log("Execute Move Reached");
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