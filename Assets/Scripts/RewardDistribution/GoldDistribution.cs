using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoldDistribution : MonoBehaviour
{
    public GameObject window;

    public Button acceptBtn;
    public GameObject warriorPanel, archerPanel, dwarfPanel, magePanel;
    public Text remainingGoldText, warriorGoldText, archerGoldText, dwarfGoldText, mageGoldText;

    private int remainingGold = 5;
    private int warriorGold = 0;
    private int archerGold = 0;
    private int dwarfGold = 0;
    private int mageGold = 0;

    void Awake()
    {
        acceptBtn.onClick.AddListener(delegate { EventManager.TriggerDistributeGoldClick(); });
    }

    void Start()
    {
        warriorPanel.SetActive(false);
        archerPanel.SetActive(false);
        dwarfPanel.SetActive(false);
        magePanel.SetActive(false);

        if (!PhotonNetwork.OfflineMode)
        {
            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                window.SetActive(false);
            }
            List<Player> players = PhotonNetwork.PlayerList.ToList();
            foreach (Player p in players)
            {
                string hero = (string)p.CustomProperties["Class"];

                if (hero != null)
                {
                    switch (hero)
                    {
                        case "Warrior":
                            warriorPanel.SetActive(true);
                            break;
                        case "Archer":
                            archerPanel.SetActive(true);
                            break;
                        case "Mage":
                            magePanel.SetActive(true);
                            break;
                        case "Dwarf":
                            dwarfPanel.SetActive(true);
                            break;
                    }
                }
            }
        }
        else
        {
            warriorPanel.SetActive(true);
            archerPanel.SetActive(true);
            dwarfPanel.SetActive(true);
            magePanel.SetActive(true);
        }

        SetRemainingGoldText();
    }

    void Update()
    {
        if (remainingGold == 0)
        {
            Buttons.Unlock(acceptBtn);
        }
        else
        {
            Buttons.Lock(acceptBtn);
        }
    }

    void SetRemainingGoldText()
    {
        remainingGoldText.text = "Remaining Gold: " + remainingGold;
    }

    public int getWarriorGold()
    {
        return this.warriorGold;
    }

    public int getArcherGold()
    {
        return this.archerGold;
    }

    public int getDwarfGold()
    {
        return this.dwarfGold;
    }

    public int getMageGold()
    {
        return this.mageGold;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #region Button Methods

    //public void OnAcceptClick()
    //{
    //    window.SetActive(false);
    //}

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
        if (mageGold > 0)
        {
            mageGold--;
            remainingGold++;
            mageGoldText.text = mageGold.ToString();
            SetRemainingGoldText();
        }
    }

    #endregion
}
