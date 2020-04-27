using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightAtADistance : MonoBehaviour
{
    private MapPath path;
    public Cell goal { get; set; }
    private List<Cell> freeCells;
    private List<Cell> extCells;

    GameObject monsterSelectPanel; 
    public Fight fight;
    public GameObject heroSelectPanel;

    public Text noBowText;

    private void OnEnable()
    {
        Debug.Log("OnEnable (fightAtADistance) started.");
        EventManager.CellClick += ChooseCellToAttack;
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable (fightAtADistance) started.");
        EventManager.CellClick -= ChooseCellToAttack;
    }

    private void Awake()
    {
        monsterSelectPanel = GameObject.Find("Canvas/Fight/Monster Select");
    }

    void ChooseCellToAttack(int cellID, Hero hero)
    {
        Debug.Log("ChooseCellToAttack started.");
        if (hero != GameManager.instance.CurrentPlayer) return;
        goal = Cell.FromId(cellID);
        //path.Extend(goal);
        //ShowAttackableArea();
        Debug.Log("the cell id is " + goal.Index);
        DisableAttackArea();
        monsterSelectPanel.transform.localScale = new Vector3(1, 1, 1);
        monsterSelectPanel.SetActive(false);
        fight.SetupFight();
        fight.ShowHeroSelectPanel();
        heroSelectPanel.gameObject.SetActive(true);
    }

    void DisableAttackArea()
    {
        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
            //cell.Disable();
        }
    }

    void ShowAttackableArea()
    {
        Debug.Log("ShowAttackableArea started.");
        freeCells = AdjacentMonstersToHero();

        foreach (Cell cell in Cell.cells)
        {
            cell.Reset();
            cell.Disable();
        }

        foreach (Cell cell in freeCells)
        {
            cell.Reset();
        }
        
    }

    public List<Cell> AdjacentMonstersToHero()
    {
        if (!GameManager.instance.CurrentPlayer.HasBow())
        {
            Debug.Log("Doesn't have a bow");
            return null;
        }

        List<Cell> adjacent_cells = new List<Cell>();

        foreach (Cell c in GameManager.instance.CurrentPlayer.Cell.WithinRange(1, 1))
        {
            if(c.Inventory.Enemies.Find(x => x is Enemy) != null)
            adjacent_cells.Add(c);
        }

        return adjacent_cells;
    }

    public List<Cell> AdjacentCellsWitHero(Cell cell)
    {
        List<Cell> adjacent_cells = new List<Cell>();

        foreach (Cell c in cell.WithinRange(1, 1))
        {
            if (c.Inventory.Heroes.Find(x => x is Hero) != null)
                adjacent_cells.Add(c);
        }

        return adjacent_cells;
    }

    public List<Hero> CellWithHeroHasBow(Cell cell)
    {
        List<Cell> adjacent_cells = new List<Cell>();
        List<Hero> heroes = new List<Hero>();

        foreach (Cell c in cell.WithinRange(1, 1))
        {
            if (c.Inventory.Heroes.Find(x => x is Hero) != null)
            {
                adjacent_cells.Add(c);
                foreach(Hero h in c.Inventory.Heroes)
                {
                    if (h.HasBow()) { heroes.Add(h); }
                }
            }
        }

        return heroes;
    }

    public void OnClickAdjacent()
    {
        if (!GameManager.instance.CurrentPlayer.HasBow())
        {
            noBowText.text = "You don't have a bow...";
        }
        noBowText.text = "";
        monsterSelectPanel = GameObject.Find("Canvas/Fight/Monster Select");
        monsterSelectPanel.transform.localScale = new Vector3(0, 0, 0);

        ShowAttackableArea();
    }

    public void ResetTextBow()
    {
        noBowText.text = "";
    }
}
