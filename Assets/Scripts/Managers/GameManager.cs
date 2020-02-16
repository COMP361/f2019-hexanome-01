using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int playerCount;
    public List<Hero> players;
    public List<Farmer> farmers;
    public List<Enemy> gors, skrals, trolls, wardraks;
    private int currentPlayerIndex = -1;
    public Token well;
    public Fog fog;
    public HeroState state;
    public LegendCards legendCards;
    public EventCards eventCards;
    private ICommand command;
    List<Enemy> monstersToMove;

    void Awake()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Additive);
        SceneManager.LoadScene("Chat", LoadSceneMode.Additive);
        SceneManager.LoadScene("Tokens", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

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
        
        playerCount = 1;
        players = new List<Hero>();
        players.Add(Warrior.Instance);
        players.Add(Archer.Instance);
        players.Add(Mage.Instance);
        players.Add(Dwarf.Instance);

        farmers = new List<Farmer>();
        farmers.Add(Farmer.Factory(24));
        farmers.Add(Farmer.Factory(36));

        gors = new List<Enemy>();

        //Gor newGor = Gor.Factory(8);
        //gors.Add(Gor.Factory(3));
        //EventManager.EndDay += MonsterMove(newGor);
        gors.Add(Gor.Factory(1));
        gors.Add(Gor.Factory(2));
        gors.Add(Gor.Factory(19));  //
        gors.Add(Gor.Factory(20));
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
        while(!move && monstersToMove.Count > 0 && i < 20) {
            Enemy monster = monstersToMove[0];
            Cell nextCell = monster.Cell.enemyPath;
            while (nextCell != null && nextCell.State.Enemies.Count > 0 && nextCell.Index != 0) nextCell = nextCell.enemyPath;
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
        state = (HeroState)CurrentPlayer.State.Clone();
    }

    void endTurn(int playerIndex)
    {
        CurrentPlayer.IsDone = false;
        CurrentPlayer.State.action = Action.None;
    }

    void InitMove()
    {
        command = new MoveCommand(CurrentPlayer);
    }

    void ExecuteMove()
    {
        command.Execute();
        //command.Dispose();
        //command = new MoveCommand(CurrentPlayer.Token, CurrentPlayer.State.cell);
    }

    void ResetCommand()
    {
        command.Dispose();
    }

    public Hero CurrentPlayer
    {
        get
        {
            return players[currentPlayerIndex];
        }
    }

}
