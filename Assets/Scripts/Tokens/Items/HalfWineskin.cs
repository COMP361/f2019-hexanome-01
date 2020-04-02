using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfWineskin : SmallToken
{
  public PhotonView photonView;

  public static HalfWineskin Factory() {
    GameObject halfWineskinGO = PhotonNetwork.Instantiate("Prefabs/Tokens/HalfWineskin", Vector3.zero, Quaternion.identity, 0);
    return halfWineskinGO.GetComponent<HalfWineskin>();
  }

public void OnEnable(){
}

  public void Awake() {
    TokenName = Type;
  }

  public override void UseCell(){
    Debug.Log("Use HalfWineskin Cell");
  }

  public override void UseHero(){
    Debug.Log("Use HalfWineskin Hero");
  }

  public static string Type { get => typeof(HalfWineskin).ToString(); }
}
