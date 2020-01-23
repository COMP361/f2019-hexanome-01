using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Cross-Scene references are not supported, and are prevented in Edit mode. In Play mode they are allowed, because Scenes cannot be saved.

public class GameManager : Singleton<GameManager> 
{

  public GameObject farmer, gor, skral, troll, wardrak;
  private int playerCount;
  public List<Hero> players;
  public List<Farmer> farmers;
  public List<Enemy> gors, skrals, trolls, wardraks;
  private int currentPlayerIndex = -1;

  public UI ui;
  public EventManager em;
  public Token well;
  public Fog fog;

  public HeroState state;

  public LegendCards legendCards;
  public EventCards eventCards; 

  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  // Custom all scene loaded event
  void Awake() 
  {
    SceneManager.LoadScene("Map", LoadSceneMode.Additive);
    SceneManager.LoadScene("Chat", LoadSceneMode.Additive);
    SceneManager.LoadScene("Tokens", LoadSceneMode.Additive);
    SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    //SceneManager.UnloadScene
    
    em = Camera.main.GetComponent<EventManager>();
    
    base.Awake();
  }

  void Start()
  {
    ui = new UI();

    legendCards = new LegendCards();
    eventCards = new EventCards();

    //well = new Token();
    fog = new Fog();
    
    playerCount = 4;
    players = new List<Hero>();

    players.Add(Warrior.instance);
    players.Add(Archer.instance);
    players.Add(Mage.instance);
    players.Add(Dwarf.instance);

    addFarmer(24); 
    addFarmer(36); 

    addGor(8);
    addGor(20);
    addGor(21);
    addGor(26);
    addGor(48);
    
    addSkral(19);

    //well.addToken(55, Color.blue);
    //well.addToken(35, Color.blue);
    //well.addToken(5, Color.blue);
    //well.addToken(45, Color.blue);

    giveTurn(0);
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    //Debug.Log("OnSceneLoaded: " + scene.name);
  }

  // Update is called once per frame
  void Update() { //this doesnt really work it just changes the current player all the time.
    if(CurrentPlayer.IsDone) {
      endTurn(currentPlayerIndex);
      giveTurn(++currentPlayerIndex % playerCount);
    }
  }

  public void giveTurn(int playerIndex) {
    currentPlayerIndex = playerIndex;
    state = (HeroState) CurrentPlayer.State.Clone();
    ui.UpdatePlayerInfo();
  }

  public void endTurn(int playerIndex) {
    CurrentPlayer.IsDone = false;
    CurrentPlayer.State.action = Action.None;
  }

  public void SelectAction(Action action) {
    CurrentPlayer.State.action = action;
    ui.ShowOptions(action);   
    ui.UpdatePlayerInfo();
  }

  public void CancelAction() {
      CurrentPlayer.State.action = Action.None;
      ui.HideOptions(); 
      ui.UpdatePlayerInfo();
  }

  public void ConfirmAction() { 
    if(CurrentPlayer.State.action == Action.Move.SetDest) {
      ui.HidePath();
      CurrentPlayer.State = state;
      CurrentPlayer.IsDone = true;

      // Is done set by the Move function ?
      // HidePath too?
    }
    //send State to Server
  }

  public void SetDest(int cellID) {
    state.Goal = Cell.FromId(cellID);
    ui.DisplayPath(state.Path);
  }

  public void AddStop() {
    //state.action = Action.Move.AddStop;
  }

  public void FreeMove() {
    //state.action = Action.Move.FreeMove;
  }

  public Hero CurrentPlayer {
    get {
      return players[currentPlayerIndex];
    }
  }

  public void addFarmer(int cellID) {
    GameObject farmerGO = Instantiate(farmer) as GameObject;
    Farmer f = farmerGO.GetComponent<Farmer>();
    f.SetRank(cellID);
    farmers.Add(f);
  }

  public void addGor(int cellID) {
    GameObject gorGO = Instantiate(gor) as GameObject;
    Enemy g = gorGO.GetComponent<Enemy>();
    g.SetRank(cellID);
    gors.Add(g);
  }

  public void addSkral(int cellID) {
    GameObject skralGO = Instantiate(skral) as GameObject;
    Enemy s = skralGO.GetComponent<Enemy>();
    s.SetRank(cellID);
    skrals.Add(s);
  }

  public void addTrolls(int cellID) {
    GameObject trollGO = Instantiate(troll) as GameObject;
    Enemy t = trollGO.GetComponent<Enemy>();
    t.SetRank(cellID);
    trolls.Add(t);
  }

  public void addWardrak(int cellID) {
    GameObject wardrakGO = Instantiate(wardrak) as GameObject;
    Enemy w = wardrakGO.GetComponent<Enemy>();
    w.SetRank(cellID);
    wardraks.Add(w);
  }
}