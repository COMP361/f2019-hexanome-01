using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player3Panel;
   
    public void OpenPlayer3Panel()
    {
    	if(Player3Panel != null )
    	{
    		bool isActive = Player3Panel.activeSelf;

    		Player3Panel.SetActive(!isActive);
    	}
    }
}
