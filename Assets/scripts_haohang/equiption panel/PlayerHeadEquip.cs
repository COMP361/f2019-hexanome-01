using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadEquip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HeadPanel;
   
    public void OpenHeadPanel()
    {
    	if(HeadPanel != null )
    	{
    		bool isActive = HeadPanel.activeSelf;

    		HeadPanel.SetActive(!isActive);
    	}
    }


}
