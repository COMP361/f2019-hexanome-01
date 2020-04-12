using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : SmallToken
{
    // Start is called before the first frame update
    public static string name = "Herb";
    public static string desc = "Medicinal herb can help you do three things. Gain WillPower, get free moves, or add to your strength in battle";
    public  static Herbs herbType;
    public Herbs myType;

    public static GameObject token;
    public PhotonView photonView;

    public static Herb Factory(Herbs type)
    {
      herbType = type;
      GameObject herbGo = PhotonNetwork.Instantiate("Prefabs/Tokens/" + herbType, Vector3.zero, Quaternion.identity, 0);
      token = herbGo;
      return herbGo.GetComponent<Herb>();
    }

    public static Herb Factory(int cellID, Herbs type)
    {
      Herb herb = Herb.Factory(type);
      herb.Cell = Cell.FromId(cellID);
      return herb;
    }

    public void OnEnable(){
      myType = herbType;
    }

    public override void UseCell(){
      EventManager.TriggerCellItemClick(this);
    }

    public override void UseHero(){
      EventManager.TriggerHeroItemClick(this);
    }

    public override void UseEffect(){
      if(!InUse){
        InUse = true;
        EventManager.TriggerHerbUseUI(this);
      }
      else{
        EventManager.TriggerError(3);
      }
    }

}
