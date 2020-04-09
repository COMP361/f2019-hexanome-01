using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
public class CharChoice : MonoBehaviour
{

  public static Hero choice { get; set; }

  static Button ArcherBtn, DwarfBtn, MageBtn, WarriorBtn;

  void Awake(){
    ArcherBtn = transform.Find("Archer").GetComponent<Button>();
    DwarfBtn = transform.Find("Dwarf").GetComponent<Button>();
    MageBtn = transform.Find("Mage").GetComponent<Button>();
    WarriorBtn = transform.Find("Warrior").GetComponent<Button>();

    ArcherBtn.onClick.AddListener(() => { clickArcher(); });
    DwarfBtn.onClick.AddListener(() => { clickDwarf(); });
    MageBtn.onClick.AddListener(() => { clickMage(); });
    WarriorBtn.onClick.AddListener(() => { clickWarrior(); });


    ArcherBtn.interactable = false;
    DwarfBtn.interactable = false;
    MageBtn.interactable = false;
    WarriorBtn.interactable = false;
  }

  public static void Init(List<Hero> heroes){

    foreach(Hero hero in heroes){
      if(hero.TokenName.Equals("Archer")){
        ArcherBtn.interactable = true;
      }
      else if(hero.TokenName.Equals("Dwarf")){
        DwarfBtn.interactable = true;
      }
      else if(hero.TokenName.Equals("Mage")){
        MageBtn.interactable = true;
      }
      else if(hero.TokenName.Equals("Warrior")){
        WarriorBtn.interactable = true;
      }
    }
  }

  public static void clickArcher(){
    Hero hero = GameManager.instance.findHero("Archer");
     CharChoice.choice = hero;
    EventManager.TriggerInventoryUIHeroPeak(hero.heroInventory);
  }
  public static void clickDwarf(){
    Hero hero = GameManager.instance.findHero("Dwarf");
    CharChoice.choice = hero;
    EventManager.TriggerInventoryUIHeroPeak(hero.heroInventory);
  }
  public static void clickMage(){
    Hero hero = GameManager.instance.findHero("Mage");
    CharChoice.choice = hero;
    EventManager.TriggerInventoryUIHeroPeak(hero.heroInventory);
  }
  public static  void clickWarrior(){
    Hero hero = GameManager.instance.findHero("Warrior");
    CharChoice.choice = hero;
    EventManager.TriggerInventoryUIHeroPeak(hero.heroInventory);
  }

  }
