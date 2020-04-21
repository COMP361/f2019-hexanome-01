using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class Fight : MonoBehaviour
{
    GameObject heroSelectPanel;
    GameObject fightPanel;
    List<Hero> closeHeroes;
    List<Hero> selectedHeroes;

    Component[] heroSelectbtns;
    Button heroSelectConfirm;
    public MultiplayerFightPlayer multiplayerFight;

    public GameManager2 GameManager2;
    
    void OnEnable() {
        EventManager.Fight += SetupFight;    
    }

    void OnDisable() {
        EventManager.Fight -= SetupFight;
    }

    void Awake() {
        closeHeroes = new List<Hero>();
        selectedHeroes = new List<Hero>();
        heroSelectPanel = transform.Find("Multi-fight Choice").gameObject;
        fightPanel = transform.Find("Multiplayer-Fight").gameObject;

        heroSelectbtns = heroSelectPanel.transform.Find("Grid/").GetComponentsInChildren(typeof(Button));
        for(int i = 0; i < heroSelectbtns.Length; i++) {
            Button btn = (Button)heroSelectbtns[i];
            btn.onClick.AddListener(delegate { ToggleHeroSelect(btn.gameObject.name, btn); });
        }

        heroSelectConfirm = heroSelectPanel.transform.Find("Confirm button").GetComponent<Button>();
        heroSelectConfirm.onClick.AddListener(delegate { HideHeroSelectPanel(); ShowFightPanel(); });
    }

    void Start() {
        SetupFight();
    }

    void ToggleHeroSelect(string heroName, Button btn) {
        Image btnImage = btn.GetComponent<Image>();
        ButtonMultiSelect sprites = btn.GetComponent<ButtonMultiSelect>();
        
        foreach(Hero hero in selectedHeroes) {
            if(hero.TokenName == heroName) {
                selectedHeroes.Remove(hero);
                btnImage.sprite = sprites.off;
                return;
            }
        }

        foreach(Hero hero in GameManager2.heroes) {
            if(hero.TokenName == heroName) {
                selectedHeroes.Add(hero);
                btnImage.sprite = sprites.on;
                return;
            }
        }
    }

    void SetupFight() {
        selectedHeroes = new List<Hero>();
        selectedHeroes.Add(GameManager2.CurrentPlayer);
        
        closeHeroes = new List<Hero>();
        foreach(Hero hero in GameManager2.heroes) {
            // Missing Archer case
            if(hero.Cell.Index == GameManager2.CurrentPlayer.Cell.Index) closeHeroes.Add(hero);
        }

        if(closeHeroes.Count > 1) {
            ShowHeroSelectPanel();
        } else {
            ShowFightPanel();
        }
    }

    void ShowHeroSelectPanel() {
        heroSelectPanel.SetActive(true);

        foreach (Button btn in heroSelectbtns) {
            if(btn.gameObject.name == GameManager2.CurrentPlayer.TokenName) {
                btn.gameObject.SetActive(false);
                continue;
            }

            btn.gameObject.SetActive(true);
            btn.interactable = false;
            foreach(Hero hero in closeHeroes) {
                if(btn.gameObject.name == hero.TokenName) {
                    btn.interactable = true;
                    break;
                }
            }
        }
    }

    void HideHeroSelectPanel() {
        heroSelectPanel.SetActive(false);
    }

    void ShowFightPanel() {
        fightPanel.SetActive(true);

        Transform heroesGroup = fightPanel.transform.Find("Grid/");
        foreach (Transform heroTile in heroesGroup) {
            if (heroTile.gameObject.tag == "Hero") {
                heroTile.gameObject.SetActive(false);
                foreach(Hero hero in selectedHeroes) {
                    if(heroTile.name == hero.TokenName) {
                        heroTile.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }

        multiplayerFight.InitializeMonster();
        multiplayerFight.InitializeHeroes();
    }
}