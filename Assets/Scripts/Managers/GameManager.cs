using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
  private int playerCount;
  public List<Hero> players;
  public List<Farmer> farmers;
  public List<IEnemy> gors, skrals, trolls, wardraks;
  private int currentPlayerIndex = -1;
  public Token well;
  public Fog fog;
  public HeroState state;
  public LegendCards legendCards;
  public EventCards eventCards;
  private ICommand command;

  void Awake() {
    SceneManager.LoadScene("Map", LoadSceneMode.Additive);
    //SceneManager.LoadScene("Chat", LoadSceneMode.Additive);
    SceneManager.LoadScene("Tokens", LoadSceneMode.Additive);
    SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    
    base.Awake();
  }

  void OnEnable() {
    EventManager.MoveSelect += InitMove;
    EventManager.MoveCancel += ResetCommand;
    EventManager.MoveConfirm += ExecuteMove;
  }

  void OnDisable() {
    EventManager.MoveSelect -= InitMove;
    EventManager.MoveCancel -= ResetCommand;
    EventManager.MoveConfirm -= ExecuteMove;
  }

  void Start() {
    playerCount = 1;
    players = new List<Hero>();
    players.Add(Warrior.Instance);
    players.Add(Archer.Instance);
    players.Add(Mage.Instance);
    players.Add(Dwarf.Instance);

    farmers = new List<Farmer>();
    farmers.Add(Farmer.Factory(24));
    farmers.Add(Farmer.Factory(36)); 

    gors = new List<IEnemy>();
    gors.Add(Gor.Factory(8));
    gors.Add(Gor.Factory(20));
    gors.Add(Gor.Factory(21));
    gors.Add(Gor.Factory(26));
    gors.Add(Gor.Factory(48));

    skrals = new List<IEnemy>();
    skrals.Add(Skral.Factory(19));
   
    trolls = new List<IEnemy>();
    wardraks = new List<IEnemy>();
    
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

  void Update() { 
    //this doesnt really work it just changes the current player all the time.
    //if(CurrentPlayer.IsDone) {
    //  endTurn(currentPlayerIndex);
    //  giveTurn(++currentPlayerIndex % playerCount);
    //}
  }

  void giveTurn(int playerIndex) {
    currentPlayerIndex = playerIndex;
    EventManager.TriggerActionUpdate( Action.None );
    EventManager.TriggerPlayerUpdate( CurrentPlayer );
    state = (HeroState) CurrentPlayer.State.Clone();
  }

  void endTurn(int playerIndex) {
    CurrentPlayer.IsDone = false;
    CurrentPlayer.State.action = Action.None;
  }

  void InitMove() {
    command = new MoveCommand(CurrentPlayer, CurrentPlayer.State.cell);
  }

  void ExecuteMove() {
    command.Execute();
    //command.Dispose();
    //command = new MoveCommand(CurrentPlayer.Token, CurrentPlayer.State.cell);
  }

  void ResetCommand() {
    command.Dispose();
  }

  public Hero CurrentPlayer {
    get {
      return players[currentPlayerIndex];
    }
  }
}