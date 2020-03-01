using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GoldDistribution : MonoBehaviour
{
    public GameObject window;

    public Button acceptBtn;

    public Text remainingGoldText, warriorGoldText, archerGoldText, dwarfGoldText, mageGoldText;

    private int remainingGold = 5;
    private int warriorGold = 0;
    private int archerGold = 0;
    private int dwarfGold = 0;
    private int mageGold = 0;

    void Start()
    {
        SetRemainingGoldText();
    }

    void SetRemainingGoldText()
    {
        remainingGoldText.text = "Remaining Gold: " + remainingGold;
    }

    void Update()
    {
        if(remainingGold == 0)
        {
            acceptBtn.enabled = true;
        }
        else
        {
            acceptBtn.enabled = false;
        }
    }

    #region Button Methods

    public void OnAcceptClick()
    {
        window.SetActive(false);
    }

    public void OnWarriorIncrementClick()
    {
        if (remainingGold > 0)
        {
            warriorGold++;
            remainingGold--;
            warriorGoldText.text = warriorGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnWarriorDecrementClick()
    {
        if (warriorGold > 0)
        {
            warriorGold--;
            remainingGold++;
            warriorGoldText.text = warriorGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnArcherIncrementClick()
    {
        if (remainingGold > 0)
        {
            archerGold++;
            remainingGold--;
            archerGoldText.text = archerGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnArcherDecrementClick()
    {
        if (archerGold > 0)
        {
            archerGold--;
            remainingGold++;
            archerGoldText.text = archerGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnDwarfIncrementClick()
    {
        if (remainingGold > 0)
        {
            dwarfGold++;
            remainingGold--;
            dwarfGoldText.text = dwarfGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnDwarfDecrementClick()
    {
        if (dwarfGold > 0)
        {
            dwarfGold--;
            remainingGold++;
            dwarfGoldText.text = dwarfGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnMageIncrementClick()
    {
        if (remainingGold > 0)
        {
            mageGold++;
            remainingGold--;
            mageGoldText.text = mageGold.ToString();
            SetRemainingGoldText();
        }
    }

    public void OnMageDecrementClick()
    {
        if (warriorGold > 0)
        {
            mageGold--;
            remainingGold++;
            mageGoldText.text = mageGold.ToString();
            SetRemainingGoldText();
        }
    }

    #endregion

    #region
    public void OnDefaultClick() {
        mageGold = 1;
        warriorGold = 1;
        archerGold = 1;
        remainingGold = 0;
        dwarfGold = 2;
        window.SetActive(false);
    }
    #endregion
}
