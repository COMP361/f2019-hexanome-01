using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfShield : Shield {

  public static HalfShield Factory()
  {
    GameObject shieldGO = PhotonNetwork.Instantiate("Prefabs/Tokens/ShieldBack", Vector3.zero, Quaternion.identity, 0);
    HalfShield shield = shieldGO.GetComponent<HalfShield>();
    shield.Cell = null;
    return shield;
  }

  public static HalfShield Factory(int cellID)
  {
    HalfShield shield = HalfShield.Factory();
    shield.Cell = Cell.FromId(cellID);
    return shield;
  }

  public override void UseEffect(){
    Debug.Log("Use Half Shield Effect");
    GameManager.instance.MainHero.heroInventory.RemoveBigToken(this);
    Destroy(gameObject);
  }
}