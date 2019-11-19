using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour{


 // not sure what {get; private set} does
  public static GameManager Instance {get; private set;}

  private int playerCount;
  private Hero[] players;
  private int currentPlayerIndex;
  private Action currentPlayerAction;
  public GameObject UI;

    // Start is called before the first frame update

    void Start(){
      playerCount = 4;
      players = new Hero[playerCount];
      players[0] = GameObject.Find("Warrior").GetComponent<Hero>();
      players[1] = GameObject.Find("Archer").GetComponent<Hero>();
      players[2] = GameObject.Find("Mage").GetComponent<Hero>();
      players[3] = GameObject.Find("Dwarf").GetComponent<Hero>();

      currentPlayerIndex = 0;
      currentPlayerAction = Action.Think;

      UpdateUI();
    }

    private void Awake()
    {
      if (Instance == null) {
        Instance = this;
        // gameObject means the object the GameManager is currently located on
        // dont destroy when changing view
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }

    // Update is called once per frame
    void Update()
    { //this doesnt really work it just changes the current player all the time.
      if(players[currentPlayerIndex].IsDone) {
        players[currentPlayerIndex].IsDone = false;
        currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
        currentPlayerAction = Action.Think;
        UpdateUI();
      }
    }

    public void UpdateUI() {
      UI.transform.FindDeepChild("Hero").GetComponent<UnityEngine.UI.Text>().text = players[currentPlayerIndex].Type;
      UI.transform.FindDeepChild("Action").GetComponent<UnityEngine.UI.Text>().text = Action.GetName(typeof(Action), currentPlayerAction);
    }

    public Action CurrentPlayerAction {
      get {
        return currentPlayerAction;
      }
      set {
        currentPlayerAction = value;
      }
    }

    public Hero CurrentPlayer {
      get {
        return players[currentPlayerIndex];
      }
    }
}
