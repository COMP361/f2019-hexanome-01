using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_follower : MonoBehaviour{

  public Transform[] slots;
  private GameObject Hero;

  // not sure what serializing does but the tutorial used it
  [SerializeField]
  private float moveSpeed = 2f;
    // Start is called before the first frame update

  // not sure what this do (it prevents the inspector to see what we serialized )but the tutorial had it
  //[HideInspector]
  // at the begining is initial position

  public int currentPosition = 0; // Corresponds to an index in the Transform array.
  public int destination = 0;
  public bool canMove = false;




  private void Start()  {
      //initialize the Player at the position he should be.
      transform.position = slots[currentPosition].transform.position;

    }

    // Update is called once per frame
  private void Update(){
      if (canMove){
        Move();
      }

    }

 public void Move() {
    transform.position = Vector2.MoveTowards(transform.position, slots[destination].transform.position, moveSpeed * Time.deltaTime);

    if(transform.position == slots[destination].transform.position ){
       currentPosition = destination;
    }
 }



}
