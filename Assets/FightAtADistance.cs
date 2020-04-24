using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAtADistance : MonoBehaviour
{

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
                    heroes.Add(h);
                }
            }
        }

        return heroes;
    }
}
