using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantCell : Cell
{
    ///NOTES///
    /*
     * Cell Locations:
     * Standard Merchant - 18,57
     * Dwarf Merchant - 71
     * Witch - Random (one of the fog cells)
     * 
     * Inventory Information:
     * Standard Items - shield, bow, falcon, wineskin, telescope
     * Special Items - strength, witch
     * 
     */

    /****** Fields ******/
    public static readonly string[] StandardProducts = new string[] { "shield", "bow", "falcon", "wineskin", "telescope", "strength" };

    public Dictionary<string, int> productsAvailable = new Dictionary<string, int>();

    /****** Functions [Unity] ******/
    protected override void Start() {

        initStandardInventory();
        color = new Color(1f, 0.875f, 0f, 0.1f);

        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        EventManager.TriggerMerchCellMouseEnter(Index);
    }

    protected override void OnMouseExit()
    {
        base.OnMouseExit();
        EventManager.TriggerMerchCellMouseLeave(Index);
    }

    /****** Functions [Private] ******/
    private void initStandardInventory() {
        foreach (string pname in StandardProducts) {
            this.productsAvailable.Add(pname, 2);
        }
    }



}
