using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Token
{

      public PhotonView photonView;
    
      public void Awake() {
        TokenName = Type;
      }

      public void useCell(){
        EventManager.TriggerCellGoldClick(this);
      }
      public void useHero(){
        EventManager.TriggerHeroGoldClick(this);
      }

      public static string Type { get => typeof(GoldCoin).ToString(); }

    }
