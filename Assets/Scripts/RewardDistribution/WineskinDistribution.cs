using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WineskinDistribution : MonoBehaviour
{
    public GameObject window;

    public Button acceptBtn;
    public GameObject warriorPanel, archerPanel, dwarfPanel, magePanel;
    public Text remainingWineskinText, warriorWineskinText, archerWineskinText, dwarfWineskinText, mageWineskinText;

    private int remainingWineskins = 2;
    private int warriorWineskins = 0;
    private int archerWineskins = 0;
    private int dwarfWineskins = 0;
    private int mageWineskins = 0;

    void Awake()
    {
        acceptBtn.onClick.AddListener(delegate { EventManager.TriggerDistributeWineskinsClick(); });
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

        SetRemainingWineskinText();
    }

    void Update()
    {
        if (remainingWineskins == 0)
        {
            Buttons.Unlock(acceptBtn);
        }
        else
        {
            Buttons.Lock(acceptBtn);
        }
    }

    void SetRemainingWineskinText()
    {
        remainingWineskinText.text = "Remaining Wineskins: " + remainingWineskins;
    }

    public int getWarriorWineskins()
    {
        return this.warriorWineskins;
    }

    public int getArcherWineskins()
    {
        return this.archerWineskins;
    }

    public int getDwarfWineskins()
    {
        return this.dwarfWineskins;
    }

    public int getMageWineskins()
    {
        return this.mageWineskins;
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
        if (remainingWineskins > 0)
        {
            warriorWineskins++;
            remainingWineskins--;
            warriorWineskinText.text = warriorWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnWarriorDecrementClick()
    {
        if (warriorWineskins > 0)
        {
            warriorWineskins--;
            remainingWineskins++;
            warriorWineskinText.text = warriorWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnArcherIncrementClick()
    {
        if (remainingWineskins > 0)
        {
            archerWineskins++;
            remainingWineskins--;
            archerWineskinText.text = archerWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnArcherDecrementClick()
    {
        if (archerWineskins > 0)
        {
            archerWineskins--;
            remainingWineskins++;
            archerWineskinText.text = archerWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnDwarfIncrementClick()
    {
        if (remainingWineskins > 0)
        {
            dwarfWineskins++;
            remainingWineskins--;
            dwarfWineskinText.text = dwarfWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnDwarfDecrementClick()
    {
        if (dwarfWineskins > 0)
        {
            dwarfWineskins--;
            remainingWineskins++;
            dwarfWineskinText.text = dwarfWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnMageIncrementClick()
    {
        if (remainingWineskins > 0)
        {
            mageWineskins++;
            remainingWineskins--;
            mageWineskinText.text = mageWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    public void OnMageDecrementClick()
    {
        if (mageWineskins > 0)
        {
            mageWineskins--;
            remainingWineskins++;
            mageWineskinText.text = mageWineskins.ToString();
            SetRemainingWineskinText();
        }
    }

    #endregion
}
