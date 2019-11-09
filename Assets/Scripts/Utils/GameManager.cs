using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour{


 // not sure what {get; private set} does
  public static GameManager Instance {get; private set;}

  public static GameObject currentPlayer;
  public static GameObject secondPlayer;
  public static bool isOver;
  public static int currentPlayerStart = 0;
    // Start is called before the first frame update

    void Start(){

      currentPlayer = GameObject.Find("Player1");
      secondPlayer = GameObject.Find("Player2");
    //  currentPlayer.GetComponent<Slot_follower>().canMove = true;

    }

   public static void Move(int destination){
     currentPlayer.GetComponent<Slot_follower>().destination = destination;
     currentPlayer.GetComponent<Slot_follower>().canMove = true;
     currentPlayer.GetComponent<Slot_follower>().Move();
     //currentPlayer.GetComponent<Slot_follower>().canMove = false;
    //currentPlayer
   }

    private void Awake()
    {
      if (Instance == null)
       {
        Instance = this;
        // gameObject means the object the GameManager is currently located on
        // dont destroy when changing view
        DontDestroyOnLoad(gameObject);
      }
      else{
        Destroy(gameObject);
      }
      //not sure what that is
      InitGame();
    }

    void InitGame()
    {
      ///
    }

    // Update is called once per frame
    void Update()
    {
      if(currentPlayer.GetComponent<Slot_follower>().currentPosition == currentPlayer.GetComponent<Slot_follower>().destination){
        currentPlayer.GetComponent<Slot_follower>().canMove = false;
        GameObject temp = currentPlayer;
        currentPlayer = secondPlayer;
        secondPlayer = temp;
      }
    }
}
