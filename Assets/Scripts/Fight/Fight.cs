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
    private bool WaitforCell;
    private int cellID;

    void OnEnable() {
        EventManager.Fight += SetupFight;  
        EventManager.CellClick += ChooseCellToAttack;  
    }

    void OnDisable() {
        EventManager.Fight -= SetupFight;
        EventManager.CellClick -= ChooseCellToAttack;
    }

    void Awake() {
        WaitforCell = false;

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

    void ChooseCellToAttack(int cellID, Hero hero)
    {
        if (!WaitforCell || hero != GameManager.instance.CurrentPlayer) return;
        
        WaitforCell = false;
        foreach (Cell cell in Cell.cells) {
            cell.Reset();
        }

        this.cellID = cellID;
        StartFight();
    }

    public void SetupFight() {
        selectedHeroes = new List<Hero>();
        selectedHeroes.Add(GameManager.instance.CurrentPlayer);
        closeHeroes = new List<Hero>();

        List<Cell> cells = GameManager.instance.CurrentPlayer.GetAttackableCells();
        if(cells.Count > 1) {
            WaitforCell = true;

            foreach (Cell cell in Cell.cells) {
                cell.Reset();
                cell.Disable();
            }

            foreach (Cell cell in cells) {
                cell.Reset();
            }

        } else if(cells.Count == 1) {
            cellID = cells[0].Index;
            StartFight();
        } else {
            return;
        }
    }

    private void StartFight() {
        foreach (Hero hero in GameManager.instance.heroes) {
            if (hero.Cell.Index == cellID) {
                if(hero.timeline.HasHoursLeft() && !hero.IsSleeping) closeHeroes.Add(hero);
            } else if(hero.HasBow()) {
                foreach(Transform t in hero.Cell.neighbours) {
                    Cell c = t.GetComponent<Cell>();
                    if (c.Index == cellID) {
                        if(hero.timeline.HasHoursLeft() && !hero.IsSleeping) closeHeroes.Add(hero);
                    }
                }
            }
        }
        
        if (closeHeroes.Count > 1) {
            ShowHeroSelectPanel();
        } else {
            ShowFightPanel();
        }
    }

    public void ShowHeroSelectPanel() {
        heroSelectPanel.SetActive(true);

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

        multiplayerFight.Init(selectedHeroes, cellID);
    }
}