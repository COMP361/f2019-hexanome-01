using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willpoint : SmallToken
{
  public static string name = "Willpoint";
  public static string desc = "Willpoints are useful to increase your hero";
  public static Willpoints willpointType;

  public static GameObject token;
  public PhotonView photonView;

  public static Willpoint Factory(Willpoints type)
  {
    willpointType = type;
    GameObject willpointGo = PhotonNetwork.Instantiate("Prefabs/Tokens/" + willpointType, Vector3.zero, Quaternion.identity, 0);
    token = willpointGo;
    return willpointGo.GetComponent<Willpoint>();
  }

  public static Willpoint Factory(int cellID, Willpoints type)
  {
    Willpoint willpoint = Willpoint.Factory(type);
    willpoint.Cell = Cell.FromId(cellID);
    return willpoint;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }
}
