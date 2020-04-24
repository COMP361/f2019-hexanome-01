using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAtADistance : MonoBehaviour
{

    void TriggerChoiceCell()
    {
        bool hasBow = false;
        foreach(Token t in GameManager.instance.CurrentPlayer.heroInventory.AllTokens)
        {
            if(t is Bow)
            {
                hasBow = true;
            }
        }

        if (!hasBow)
        {
            Debug.Log("Doesn't have a bow");
            return;
        }

        List<Cell> adjacent_cells = new List<Cell>();

        foreach (Cell c in GameManager.instance.CurrentPlayer.Cell.WithinRange(1, 1))
        {
            if(c.Inventory.Enemies.Find(x => x is Enemy) != null)
            adjacent_cells.Add(c);
        }
    }

}
