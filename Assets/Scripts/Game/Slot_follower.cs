using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_follower : MonoBehaviour{
  Slot [] Slots;
  public GameObject Hero;
    // Start is called before the first frame update

  public float MoveSpeed;

  int Timer;

  static Vector3 CurrentPositionHolder;
    void Start()  {
      Slots = GetComponentsInChildren<Slot> ();

      foreach(Slot n in Slots){
        Debug.Log (n.name);
      }
    }

    // Update is called once per frame
    void Update(){

    }
}
