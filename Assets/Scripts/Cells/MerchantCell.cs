using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantCell : Cell {

    #region || NOTES ||

    /* Cell Locations:
     * Standard Merchant - 18,57
     * Dwarf Merchant - 71
     * Witch - Random (one of the fog cells)
     *
     * Inventory Information:
     * Standard Items - shield, bow, falcon, wineskin, telescope
     * Special Items - strength, witch
     */

    #endregion

    #region Static Fields
    public static readonly Item[] BaseProducts = { Item.Bow, Item.Helm, Item.Falcon, Item.Wineskin, Item.Telescope};
    #endregion

    #region Fields
    public Dictionary<string, int> products;
    public Dictionary<Item, int> specialProducts;

    #endregion

    void OnEnable() {
        base.OnEnable();
    }

    void OnDisable() {
        base.OnDisable();
    }

    #region Functions [Unity] // Overriding
    protected override void Start() {
        // Initialize Merchant products with the basics
        products = new Dictionary<string, int>();
        foreach (Item pname in BaseProducts) {
            this.products.Add(pname.ToString(), 2);
        }
        base.Start();
    }

    protected override void Awake() {
      base.Awake();
    }

    protected override void OnMouseEnter() {
        if (!Active) return;

        base.OnMouseEnter();
        EventManager.TriggerMerchCellMouseEnter(Index);
    }

    protected override void OnMouseExit() {
        if (!Active) return;

        base.OnMouseExit();
        EventManager.TriggerMerchCellMouseLeave();
    }

    #endregion
}
