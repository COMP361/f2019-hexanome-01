using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandEquip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HandPanel;
   
    public void OpenHeadPanel()
    {
    	if(HandPanel != null )
    	{
    		bool isActive = HandPanel.activeSelf;

    		HandPanel.SetActive(!isActive);
    	}
    }
    
}
