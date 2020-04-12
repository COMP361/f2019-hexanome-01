using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : SmallToken
{
    // Start is called before the first frame update
    public static string itemName = "Herb";
    public static string desc = "Herb allows you to move around";
    public static Herbs herbType;


    public static GameObject token;
    public PhotonView photonView;

    public static Herb Factory(Herbs type)
    {
      herbType = type;
      GameObject herbGo = PhotonNetwork.Instantiate("Prefabs/Tokens/" + herbType, Vector3.zero, Quaternion.identity, 0);
      token = herbGo;
      Herb herb = herbGo.GetComponent<Herb>();
      herb.Cell = null;
      return herb;
    }

    public static Herb Factory(int cellID, Herbs type)
    {
      Herb herb = Herb.Factory(type);
      herb.Cell = Cell.FromId(cellID);
      return herb;
    }

    public override void UseCell(){
      EventManager.TriggerCellItemClick(this);
    }

    public override void UseHero(){
      EventManager.TriggerHeroItemClick(this);
    }

    public override void UseEffect(){
      Debug.Log("Use Herb Effect");
    }

}
