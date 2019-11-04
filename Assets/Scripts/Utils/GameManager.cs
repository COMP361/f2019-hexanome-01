using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{



  public static GameManager Instance {get; private set;}

  public int CurrentPlayer;
    // Start is called before the first frame update
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

    }
}
