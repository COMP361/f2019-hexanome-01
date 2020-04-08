using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : SmallToken
{
    // Start is called before the first frame update
    public static string name = "Herb";
    public static string desc = "Herb allows you to move around";
    public static Herbs herbType;


    public static GameObject token;
    public PhotonView photonView;

    public static Herb Factory(Herbs type)
    {
        herbType = type;

        GameObject herbGo = PhotonNetwork.Instantiate("Prefabs/Tokens/" + herbType, Vector3.zero, Quaternion.identity, 0);
        token = herbGo;
        return herbGo.GetComponent<Herb>();
    }

    public static Herb Factory(int cellID)
    {
        Herb herb = Herb.Factory(Herbs.Herb3);
        herb.Cell = Cell.FromId(cellID);
        return herb;
    }

    public override void UseCell(){
      EventManager.TriggerCellItemClick(this);
      Debug.Log("Use Herb Cell");
    }

    public override void UseHero(){
      Debug.Log("Use Herb Hero");
      EventManager.TriggerHeroItemClick(this);
    }

    public override void UseEffect(){
        Debug.Log("Use Herb Effect");
  }

}
