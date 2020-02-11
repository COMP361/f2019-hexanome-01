using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveByDice : MonoBehaviour
{
    //stores waypoints
    public Transform[] waypoints;
    //initialize the waypoint to intex 0
    public int waypointIndex = 0;
    
    public bool canMove = false;
    private static GameObject player1, player2,player3,player4;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("p1");
        player2 = GameObject.Find("p2");
        player3 = GameObject.Find("p3");
        player4 = GameObject.Find("p4");
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
