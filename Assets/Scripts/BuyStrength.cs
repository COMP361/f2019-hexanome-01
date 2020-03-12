using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyStrength : MonoBehaviour
{

    Button buyBtn, cancelBtn;

    void Awake() {
        buyBtn = transform.Find("Buy Button").GetComponent<Button>();
        buyBtn.onClick.AddListener(delegate { Buy(); });

        cancelBtn = transform.Find("Cancel Button").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate { Hide(); });
    }
    
    public void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = (hero.GetType() == typeof(Dwarf) && hero.Cell.Index == 71) ? 1 : 2;
        if(hero.State.heroInventory.numOfGold >= 2) {
            hero.State.heroInventory.RemoveGold(cost);
            hero.State.Strength = hero.State.Strength + 1;
            EventManager.TriggerCurrentPlayerUpdate(hero);
        }
        Hide();
    }

    void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }
}
