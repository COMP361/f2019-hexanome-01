using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoard : MonoBehaviour
{
    Text farmerCount;
    Text willPower;
    Text strength;
    
    void OnEnable() {
        EventManager.FarmersInventoriesUpdate += UpdateFarmerCount;
        EventManager.CurrentPlayerUpdate += UpdatePlayerStats;
        EventManager.MainHeroInit += InitHero;
    }

    void OnDisable() {
        EventManager.FarmersInventoriesUpdate -= UpdateFarmerCount;
        EventManager.CurrentPlayerUpdate -= UpdatePlayerStats;
        EventManager.MainHeroInit += InitHero;
    }

    // Start is called before the first frame update
    void Awake() {
        farmerCount = transform.Find("Farmer/Count").GetComponent<Text>();
        strength = transform.Find("Strength/Count").GetComponent<Text>();
        willPower = transform.Find("Willpower/Count").GetComponent<Text>();
    }

    private void UpdatePlayerStats(Hero hero) {
        strength.text = hero.State.getStrength().ToString();
        willPower.text = hero.State.getWP().ToString();
    }

    void UpdateFarmerCount(int attachedFarmers, int noTargetFarmers, int detachedFarmers) {
        farmerCount.text = attachedFarmers.ToString();
    }

    void InitHero(Hero hero) {
        Transform heroes = transform.Find("Heroes");

        foreach (Transform child in heroes) {
            foreach (Transform grandChild in child) {
                grandChild.gameObject.SetActive(false);
            }
        }

        GameObject go = heroes.Find(hero.name + "/" + hero.HeroName).gameObject;
        go.SetActive(true);
    }
}
