using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runestone : SmallToken
{
  public static string name = "Runestone";
  public static string desc = "Collect three of different colors to unlock an awesome power.";
  public static RunestoneColor color;
  public static bool isCovered;

  public static GameObject token;
  public PhotonView photonView;

  public static Runestone Factory(RunestoneColor runestoneColor)
  {
    color = runestoneColor;
    isCovered = true;
    GameObject runestoneGo = PhotonNetwork.Instantiate("Prefabs/Tokens/Runestone", Vector3.zero, Quaternion.identity, 0);
    token = runestoneGo;
    return runestoneGo.GetComponent<Runestone>();
  }

  public static Runestone Factory(int cellID, RunestoneColor runestoneColor)
  {
    Runestone runestone = Runestone.Factory(runestoneColor);
    runestone.Cell = Cell.FromId(cellID);
    return runestone;
  }

  public override void UseCell(){
    EventManager.TriggerCellItemClick(this);
  }

  public override void UseHero(){
    EventManager.TriggerHeroItemClick(this);
  }

  public override void UseEffect(){
    Debug.Log("Use Runestone Effect");
  }


  public void uncoverRunestone()
  {
      string runestoneColor = color.ToString();
      Sprite uncoveredSprite = Resources.Load<Sprite>("Sprites/Tokens/Stone/Stone-" + runestoneColor);
      token.GetComponent<SpriteRenderer>().sprite = uncoveredSprite;
  }

    public static string Type { get => typeof(Runestone).ToString(); }
}
