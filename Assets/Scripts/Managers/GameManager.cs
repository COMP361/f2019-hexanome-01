using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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
        playerCount = 1;
        heroes = new List<Hero>();
        heroes.Add(Warrior.Instance);
        heroes.Add(Archer.Instance);
        heroes.Add(Mage.Instance);
        heroes.Add(Dwarf.Instance);

        farmers = new List<Farmer>();
        farmers.Add(Farmer.Factory(24));
        farmers.Add(Farmer.Factory(36));

        gors = new List<Enemy>();

        //Gor newGor = Gor.Factory(8);
        gors.Add(Gor.Factory(3));
        //EventManager.EndDay += MonsterMove(newGor);
        gors.Add(Gor.Factory(2));
        gors.Add(Gor.Factory(19));
        gors.Add(Gor.Factory(20));
        gors.Add(Gor.Factory(48));

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

        giveTurn(0);

    }

    void Update()
    {
        //this doesnt really work it just changes the current player all the time.
        //if(CurrentPlayer.IsDone) {
        //  endTurn(currentPlayerIndex);
        //  giveTurn(++currentPlayerIndex % playerCount);
        //}
    }

    void MonsterEndDayEvents()
    {
        monsterMove(gors);
        monsterMove(skrals);
        monsterMove(wardraks);
        monsterMove(trolls);
    }


    /*
     * Goes through a monster list and moves them in order.
     *
     * Remaining: Allowing multiple enemies at the Zeroth cell.
     */
    void monsterMove(List<Enemy> enemy)
    {
        if (enemy != null)
        {
            foreach (var monster in enemy)
            {
                Cell nextCell = monster.Cell.enemyPath;

                // search for monster-free cell
                do
                {
                    if (nextCell.State.Enemies != null)
                    {

                        // update monster's prev cell's enemy state
                        monster.Cell.State.Enemies[0] = null;

                        // move monster to this cell
                        monster.Move(nextCell);

                        nextCell.State.Enemies.Add(monster);

                        nextCell = null;
                    }
                    else
                    {
                        // set next cell
                        nextCell = nextCell.enemyPath;
                    }

                } while (nextCell != null);

            }

            // sort the list according to monster cell position
            //enemy.Sort.((a = enemy.Cell)

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
        photonView.RPC("ReceiveInitMove", RpcTarget.AllBuffered);
        //command = new MoveCommand(CurrentPlayer);
    }

    [PunRPC]
    void ReceiveInitMove()
    {
        Debug.Log("Init Move Reached");

        //if (PhotonNetwork.isMasterClient) {
        GameObject commandGO = PhotonNetwork.Instantiate("Prefabs/Commands/MoveCommand", Vector3.zero, Quaternion.identity, 0);
        command = commandGO.GetComponent<MoveCommand>();
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

}
