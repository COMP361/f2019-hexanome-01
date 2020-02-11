using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantInventory : CellInventory
{
    // Start is called before the first frame update
    GameObject displayUI;
    protected override void Start()
    {
        Transform uiTransform = transform.Find("MerchantUI");

        displayUI = uiTransform.gameObject;
        textTransform = uiTransform.Find("merchantDescription");

        displayUI.SetActive(false); 
    }

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

    protected override void Show(int CellId)
    {
        formatDescription(CellId);
        textTransform.GetComponent<Text>().text = description;
        displayUI.SetActive(true);
    }

    protected override void UnShow(int CellId)
    {
        displayUI.SetActive(false);
    }


    public override void formatDescription(int CellId)
    {
        MerchantCell merchCell = Cell.FromId(CellId) as MerchantCell;

        if (merchCell == null) return;

        this.description = string.Empty;

        foreach (KeyValuePair<string, int> product in merchCell.productsAvailable)
        {
            this.description += product.Key + " - " + product.Value + "\n";
        }
    }
}