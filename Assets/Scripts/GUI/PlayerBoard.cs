﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoard : MonoBehaviour
{
    Text farmerCount;
    Text willPower;
    Text strength;
    Text numOfDice;
    Text description;

    void OnEnable() {
        EventManager.FarmersInventoriesUpdate += UpdateFarmerCount;
        EventManager.UpdateHeroStats += UpdatePlayerStats;
        EventManager.MainHeroInit += InitHero;
        EventManager.CompleteHeroBoardUpdate += updateCompleteBoard;
    }

    void OnDisable() {
        EventManager.FarmersInventoriesUpdate -= UpdateFarmerCount;
        EventManager.UpdateHeroStats -= UpdatePlayerStats;
        EventManager.MainHeroInit -= InitHero;
        EventManager.CompleteHeroBoardUpdate -= updateCompleteBoard;

    }

    // Start is called before the first frame update
    void Awake() {
        farmerCount = transform.Find("Farmer/Count").GetComponent<Text>();
        strength = transform.Find("Strength/Count").GetComponent<Text>();
        willPower = transform.Find("Willpower/Count").GetComponent<Text>();
        numOfDice = transform.Find("Dice/Count").GetComponent<Text>();
        description = transform.Find("Description/HeroDesc").GetComponent<Text>();
    }

    private void UpdatePlayerStats(Hero hero) {
      if(hero.TokenName.Equals(GameManager.instance.MainHero.TokenName)){
        strength.text = hero.Strength.ToString();
        willPower.text = hero.Willpower.ToString();
        updateNumOfDice(hero);
        updateDescription(hero);
      }
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

        UpdatePlayerStats(hero);
    }

    //NEED SOMETHING FOR FARMERCOUNT
    void updateCompleteBoard(Hero hero){
        InitHero(hero);
        strength.text = hero.Strength.ToString();
        willPower.text = hero.Willpower.ToString();
        updateNumOfDice(hero);
        updateDescription(hero);
    }

    void updateDescription(Hero hero){
      description.text = hero.HeroDescription;
    }

   void updateNumOfDice(Hero hero){
     if(hero.TokenName.Equals("Archer")){
       if(hero.Willpower < 7){
         numOfDice.text = "3";
       }
       else if(hero.Willpower < 14){
         numOfDice.text = "4";
       }
       else{
         numOfDice.text = "5";
       }
     }
     if(hero.TokenName.Equals("Dwarf")){
       if(hero.Willpower < 7){
         numOfDice.text = "1";
       }
       else if(hero.Willpower < 14){
         numOfDice.text = "2";
       }
       else{
         numOfDice.text = "3";
       }
     }
     if(hero.TokenName.Equals("Mage")){
      numOfDice.text = "1";
     }
     if(hero.TokenName.Equals("Warrior")){
       if(hero.Willpower < 7){
         numOfDice.text = "2";
       }
       else if(hero.Willpower < 14){
         numOfDice.text = "3";
       }
       else{
         numOfDice.text = "4";
       }
     }
   }
}
