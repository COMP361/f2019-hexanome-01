using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoard : MonoBehaviour
{
    Text farmerCount;
    public Text willPower;
    public Text strength;
    public Text specialInfo;
    public List<Hero> heroes;

    void OnEnable() {
        EventManager.FarmersInventoriesUpdate += UpdateFarmerCount;
        EventManager.CurrentPlayerUpdate += updatePlayerStats;
    }



    void OnDisable() {
        EventManager.FarmersInventoriesUpdate -= UpdateFarmerCount;
        EventManager.CurrentPlayerUpdate -= updatePlayerStats;
    }

    // Start is called before the first frame update
    void Awake() {
        farmerCount = transform.Find("Farmer/Count").GetComponent<Text>();
    }
    private void updatePlayerStats(Hero hero)
    {
        strength.text = hero.State.getStrength().ToString();
        willPower.text = hero.State.getWP().ToString();
        

    }

    void UpdateFarmerCount(int attachedFarmers, int noTargetFarmers, int detachedFarmers) {
        farmerCount.text = attachedFarmers.ToString();
    }
}
