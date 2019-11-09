using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveButtonHandler : MonoBehaviour{
    // Start is called before the first frame update
     public void Move()
    {
      Debug.Log ("On click");
      var destination = Random.Range(0,3);
      GameManager.Move(destination);
    }

    // Update is called once per frame

}
