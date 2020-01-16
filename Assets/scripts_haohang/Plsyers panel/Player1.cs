using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject Player1Panel;
   
    public void OpenPlayer1Panel()
    {
    	if(Player1Panel != null )
    	{
    		bool isActive = Player1Panel.activeSelf;

    		Player1Panel.SetActive(!isActive);
    	}
    }
}
