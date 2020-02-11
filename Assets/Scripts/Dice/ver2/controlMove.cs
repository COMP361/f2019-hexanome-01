using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//
//
//
public class controlMove : MonoBehaviour
{       
    private static GameObject player1, player2, player3, player4;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;
    public static int player3StartWaypoint = 0;
    public static int player4StartWaypoint = 0;
    //the value we get after throw the dice, get from rollButton class when we get the maximum number
    public static int diceSideThrown = 0;
    // Start is called before the first frame update
    //void Start()
    //{
    //    player1 = GameObject.Find("p1");
    //    player2 = GameObject.Find("p2");
    //    player3 = GameObject.Find("p3");
    //    player4 = GameObject.Find("p4");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //if current player's position is greater than its start point + dice value it get, it stops
    //    if (player1.GetComponent<moveByDice>().waypointIndex > player1StartWaypoint + diceSideThrown)
    //    {   
    //        //then we not allowed it to move
    //        player1.GetComponent<moveByDice>().canMove = false;
    //        //and set the current position as the start position for next move
    //        player1StartWaypoint = player1.GetComponent<moveByDice>().waypointIndex - 1;
    //    }
    //}


    //public static void MovePlayer(int playerToMove)
    //{
    //    switch (playerToMove)
    //    {
    //        case 1:
    //            player1.GetComponent<moveByDice>().canMove = true;
    //            break;

    //        case 2:
    //            player2.GetComponent<moveByDice>().canMove = true;
    //            break;
    //        case 3:
    //            player3.GetComponent<moveByDice>().canMove = true;
    //            break;
    //        case 4:
    //            player4.GetComponent<moveByDice>().canMove = true;
    //            break;
    //    }
    //}
}
