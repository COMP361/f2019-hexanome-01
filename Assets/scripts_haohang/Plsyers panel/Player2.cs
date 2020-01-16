using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject Player2Panel;
   
    public void OpenPlayer2Panel()
    {
    	if(Player2Panel != null )
    	{
    		bool isActive = Player2Panel.activeSelf;

    		Player2Panel.SetActive(!isActive);
    	}
    }
}
