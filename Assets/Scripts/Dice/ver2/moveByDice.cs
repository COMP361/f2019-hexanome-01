using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveByDice : MonoBehaviour
{
    //stores waypoints
    public Transform[] waypoints;
    //initialize the waypoint to intex 0
    //the current waipoint for player will be store in the attribute in player class
    //and then take as input into waypointIndex
    public int waypointIndex = 0;
    private float moveSpeed = 1f;

    public bool canMove = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (canMove)
        //{
            
        //}
        //while(whosTurn > 4)
        //{
        //    //if whosTurn>4 -> the 4th plyaer has done his/her action to restart from player1
        //    whosTurn = 1;
        //}
        //if (canMove) {
        //    switch (whosTurn)
        //    {
        //        case 1:

        //            break;

        //        case 2:

        //            break;
        //        case 3:
        //    }
        //}
    }

    //private void Move()
    //{
    //    if (waypointIndex <= waypoints.Length - 1)
    //    {

    //        // move sprite towards the target location
    //        // transform.position = Vector2.MoveTowards(transform.position, target, step);
    //        transform.position = Vector2.MoveTowards(transform.position,
    //        waypoints[waypointIndex].transform.position,
    //        moveSpeed * Time.deltaTime);

    //        if (transform.position == waypoints[waypointIndex].transform.position)
    //        {
    //            waypointIndex += 1;
    //        }
    //    }
    //}

    

}
