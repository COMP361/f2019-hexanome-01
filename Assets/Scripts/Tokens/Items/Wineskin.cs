using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wineskin : SmallToken
{
    public static Wineskin Factory()
    {
      GameObject wineSkinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/Wineskin", Vector3.zero, Quaternion.identity, 0);
      return wineSkinGO.GetComponent<Wineskin>();
    }

    public static Wineskin Factory(int cellID)
    {
        Wineskin wineskin = Wineskin.Factory();
        wineskin.Cell = Cell.FromId(cellID);
        return wineskin;
    }


    public static string Type { get => typeof(Wineskin).ToString(); }
}
