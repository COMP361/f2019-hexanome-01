﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runestone : SmallToken
{
    public static string itemName = "Runestone";
    public static string desc = "Collect three of different colors to unlock an awesome power.";
    public RunestoneColor color;
    public bool isCovered;
    public static int runestoneCount = 0;
    public  GameObject token;
    public PhotonView photonView;

    public static Runestone Factory()
    {
        GameObject runestoneGo = PhotonNetwork.Instantiate("Prefabs/Tokens/Runestone", Vector3.zero, Quaternion.identity, 0);
      //  token = runestoneGo;
        runestoneCount++;
        Runestone rs = runestoneGo.GetComponent<Runestone>();
        rs.Cell = null;
        return rs;
    }

    public static Runestone Factory(int cellID)
    {
      Runestone runestone = Runestone.Factory();
      runestone.Cell = Cell.FromId(cellID);
      return runestone;
    }

    public void OnEnable(){
      isCovered = true;
      if(runestoneCount == 0 || runestoneCount == 1)
      {
          color = RunestoneColor.Blue;
      }
      if (runestoneCount == 2 || runestoneCount == 3)
      {
          color = RunestoneColor.Green;
      }
      if (runestoneCount == 4)
      {
          color = RunestoneColor.Yellow;
      }
      int viewID = this.GetComponent<PhotonView>().ViewID;
      token = PhotonView.Find(viewID).gameObject;
    }

    public RunestoneColor GetColor()
    {
      return color;
    }

    public override void UseCell(){
      EventManager.TriggerCellItemClick(this);
      Debug.Log("Use Runestone Cell");
    }

    public override void UseHero(){
      Debug.Log("Use Runestone Hero");
      EventManager.TriggerHeroItemClick(this);
    }

    public override void UseEffect(){
      Debug.Log("Use Runestone Effect");
      uncoverRunestone();
    }


    public void uncoverRunestone()
    {
      isCovered = false;
      string runestoneColor = color.ToString();
      Sprite uncoveredSprite = Resources.Load<Sprite>("Sprites/Tokens/Stone/Stone-" + runestoneColor);
      token.GetComponent<SpriteRenderer>().sprite = uncoveredSprite;

      InventoryUICell.instance.ForceUpdate(GameManager.instance.MainHero.Cell.Inventory, GameManager.instance.MainHero.Cell.Index );
    }

    public static string Type { get => typeof(Runestone).ToString(); }
}
