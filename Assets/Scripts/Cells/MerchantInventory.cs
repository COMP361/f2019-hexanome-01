using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantInventory : CellInventory
{
    // Start is called before the first frame update
    GameObject displayUI;
    
    protected override void OnEnable()
    {
        EventManager.MerchCellMouseEnter += Show;
        EventManager.MerchCellMouseLeave += UnShow;
    }

    protected override void OnDisable()
    {
        EventManager.MerchCellMouseEnter -= Show;
        EventManager.MerchCellMouseLeave -= UnShow;
    }

    public void formatDescription(int CellId)
    {
        MerchantCell merchCell = Cell.FromId(CellId) as MerchantCell;

        if (merchCell == null) return;

        this.description = string.Empty;

        foreach (KeyValuePair<string, int> product in merchCell.products)
        {
            this.description += product.Key + " - " + product.Value + "\n";
        }
    }
}