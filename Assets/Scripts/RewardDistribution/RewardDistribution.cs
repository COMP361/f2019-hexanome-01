using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardDistribution : MonoBehaviour
{
    public GameObject window;

    public Button acceptBtn;
    public GameObject warriorPanel, archerPanel, dwarfPanel, magePanel;
    public Text remainingRewardText, warriorGoldText, archerGoldText, dwarfGoldText, mageGoldText, warriorWPText, archerWPText, dwarfWPText, mageWPText;

    private int remainingReward = 0;
    private int warriorGold = 0;
    private int archerGold = 0;
    private int dwarfGold = 0;
    private int mageGold = 0;

    private int warriorWP= 0;
    private int archerWP = 0;
    private int dwarfWP= 0;
    private int mageWP = 0;

    void Awake()
    {
        acceptBtn.onClick.AddListener(delegate { });
        archerPanel.SetActive(false);
        dwarfPanel.SetActive(false);
        magePanel.SetActive(false);
        warriorPanel.SetActive(false);
        window.SetActive(false);
    }

    void ShowShareReward(int amt, Hero controller, List<Hero> heroes){
      remainingReward = amt;
      SetRemainingRewardText();

      foreach(Hero a in heroes){
        if(GameManager.instance.MainHero.TokenName.Equals(controller.TokenName)){
          window.SetActive(true);
        }
        else{
        // Trigger block panels for all others
        }
        if(a.TokenName.Equals("Archer")){
          archerGoldText.text = "" + 0;
          archerWPText.text = "" + 0;
          archerPanel.SetActive(true);
        }
        else if(a.TokenName.Equals("Dwarf")){
          dwarfGoldText.text = "" + 0;
          dwarfWPText.text = "" + 0;
          dwarfPanel.SetActive(true);
        }
        else if(a.TokenName.Equals("Mage")){
          mageGoldText.text = "" + 0;
          mageWPText.text = "" + 0;
          magePanel.SetActive(true);
        }
        else if(a.TokenName.Equals("Warrior")){
          warriorGoldText.text = "" + 0;
          warriorWPText.text = "" + 0;
          warriorPanel.SetActive(true);
        }
      }


    }

    void Update()
    {
        if (remainingReward == 0)
        {
            Buttons.Unlock(acceptBtn);
        }
        else
        {
            Buttons.Lock(acceptBtn);
        }
    }

    void SetRemainingRewardText()
    {
        remainingRewardText.text = "Remaining Reward: " + remainingReward;
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

    public int getWarriorWP()
    {
        return this.warriorWP;
    }

    public int getArcherWP()
    {
        return this.archerWP;
    }

    public int getDwarfWP()
    {
        return this.dwarfWP;
    }

    public int getMageWP()
    {
        return this.mageWP;
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

    public void OnWarriorIncrementGoldClick()
    {
        if (remainingReward > 0)
        {
            warriorGold++;
            remainingReward--;
            warriorGoldText.text = warriorGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnWarriorDecrementGoldClick()
    {
        if (warriorGold > 0)
        {
            warriorGold--;
            remainingReward++;
            warriorGoldText.text = warriorGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnArcherIncrementGoldClick()
    {
        if (remainingReward > 0)
        {
            archerGold++;
            remainingReward--;
            archerGoldText.text = archerGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnArcherDecrementGoldClick()
    {
        if (archerGold > 0)
        {
            archerGold--;
            remainingReward++;
            archerGoldText.text = archerGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnDwarfIncrementGoldClick()
    {
        if (remainingReward > 0)
        {
            dwarfGold++;
            remainingReward--;
            dwarfGoldText.text = dwarfGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnDwarfDecrementGoldClick()
    {
        if (dwarfGold > 0)
        {
            dwarfGold--;
            remainingReward++;
            dwarfGoldText.text = dwarfGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnMageIncrementGoldClick()
    {
        if (remainingReward > 0)
        {
            mageGold++;
            remainingReward--;
            mageGoldText.text = mageGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnMageDecrementGoldClick()
    {
        if (mageGold > 0)
        {
            mageGold--;
            remainingReward++;
            mageGoldText.text = mageGold.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnWarriorIncrementWPClick()
    {
        if (remainingReward > 0)
        {
            warriorWP++;
            remainingReward--;
            warriorWPText.text = warriorWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnWarriorDecrementWPClick()
    {
        if (warriorWP > 0)
        {
            warriorWP--;
            remainingReward++;
            warriorWPText.text = warriorWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnArcherIncrementWPClick()
    {
        if (remainingReward > 0)
        {
            archerWP++;
            remainingReward--;
            archerWPText.text = archerWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnArcherDecrementWPClick()
    {
        if (archerWP > 0)
        {
            archerWP--;
            remainingReward++;
            archerWPText.text = archerWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnDwarfIncrementWPClick()
    {
        if (remainingReward > 0)
        {
            dwarfWP++;
            remainingReward--;
            dwarfWPText.text = dwarfWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnDwarfDecrementWPClick()
    {
        if (dwarfWP > 0)
        {
            dwarfWP--;
            remainingReward++;
            dwarfWPText.text = dwarfWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnMageIncrementWPClick()
    {
        if (remainingReward > 0)
        {
            mageWP++;
            remainingReward--;
            mageWPText.text = mageWP.ToString();
            SetRemainingRewardText();
        }
    }

    public void OnMageDecrementWPClick()
    {
        if (mageWP > 0)
        {
            mageWP--;
            remainingReward++;
            mageWPText.text = mageWP.ToString();
            SetRemainingRewardText();
        }
    }

    #endregion

    #region
    public void OnDefaultClick() {
        OnWarriorIncrementGoldClick();
        OnArcherIncrementGoldClick();
        OnMageIncrementGoldClick();
        OnDwarfIncrementGoldClick();
        //window.SetActive(false);
    }
    #endregion
}
