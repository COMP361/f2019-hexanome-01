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

    GameObject monsterSelectPanel;
    public FightAtADistance fightAtADistance;
    public Cell goal;
    public bool distanceFight;

    void OnEnable() {
        EventManager.Fight += SetupFight;    
    }

    void OnDisable() {
        EventManager.Fight -= SetupFight;
    }

    void Awake() {
        closeHeroes = new List<Hero>();
        selectedHeroes = new List<Hero>();
        heroSelectPanel = transform.Find("Hero Select").gameObject;
        fightPanel = transform.Find("Fight Rounds/Panel").gameObject;

        heroSelectbtns = heroSelectPanel.transform.Find("Grid/").GetComponentsInChildren(typeof(Button));
        for(int i = 0; i < heroSelectbtns.Length; i++) {
            Button btn = (Button)heroSelectbtns[i];
            btn.onClick.AddListener(delegate { ToggleHeroSelect(btn.gameObject.name, btn); });
        }

        heroSelectConfirm = heroSelectPanel.transform.Find("Confirm button").GetComponent<Button>();
        heroSelectConfirm.onClick.AddListener(delegate { HideHeroSelectPanel(); ShowFightPanel(); });
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

        foreach(Hero hero in GameManager.instance.heroes) {
            if(hero.TokenName == heroName) {
                selectedHeroes.Add(hero);
                btnImage.sprite = sprites.on;
                return;
            }
        }
    }

    public void SetupFight()
    {
        selectedHeroes = new List<Hero>();
        selectedHeroes.Add(GameManager.instance.CurrentPlayer);

        closeHeroes = new List<Hero>();

        if (distanceFight)
        {
            Cell cell = fightAtADistance.goal;
            List<Hero> heroes = fightAtADistance.CellWithHeroHasBow(cell);
            foreach (Hero hero in cell.Inventory.Heroes)
            {
                // Missing Archer case
                if (hero.Cell.Index == cell.Index) closeHeroes.Add(hero);
            }
            foreach(Hero hero in heroes)
            {
                closeHeroes.Add(hero);
            }
        }
        else
        {
            foreach (Hero hero in GameManager.instance.heroes)
            {
                // Missing Archer case
                if (hero.Cell.Index == GameManager.instance.CurrentPlayer.Cell.Index) closeHeroes.Add(hero);
            }
        }

        if (closeHeroes.Count > 1)
        {
            ShowHeroSelectPanel();
        }
        else
        {
            ShowFightPanel();
        }
    }

    public void ShowHeroSelectPanel()
    {
        heroSelectPanel.SetActive(true);
        goal = fightAtADistance.goal;

        foreach (Button btn in heroSelectbtns)
        {
            if (btn.gameObject.name == GameManager.instance.CurrentPlayer.TokenName)
            {
                btn.gameObject.SetActive(false);
                continue;
            }

            btn.gameObject.SetActive(true);
            btn.interactable = false;
            foreach (Hero hero in closeHeroes)
            {
                if (btn.gameObject.name == hero.TokenName)
                {
                    btn.interactable = true;
                    Image btnImage = btn.GetComponent<Image>();
                    ButtonMultiSelect sprites = btn.GetComponent<ButtonMultiSelect>();
                    btnImage.sprite = sprites.off;
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

        multiplayerFight.Init(selectedHeroes);
    }

    public void SetDistanceTrue()
    {
        this.distanceFight = true;
    }

    public void SetDistanceFalse()
    {
        this.distanceFight = false;
    }
}