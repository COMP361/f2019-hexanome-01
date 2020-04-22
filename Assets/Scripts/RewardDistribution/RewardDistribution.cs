using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardDistribution : Singleton<RewardDistribution>
{
    public GameObject window;

    List<Hero> allHeroes;

    public PhotonView photonView;

    public Button acceptBtn;
    public GameObject distributeRewardPanel, blockPanel, warriorPanel, archerPanel, dwarfPanel, magePanel;
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

    void OnEnable(){
      EventManager.ShareReward += ShowShareReward;
    }

    void OnDisable(){
      EventManager.ShareReward -= ShowShareReward;
    }

    void Awake()
    {
        acceptBtn.onClick.AddListener(delegate { HideShareReward();});
        archerPanel.SetActive(false);
        dwarfPanel.SetActive(false);
        magePanel.SetActive(false);
        warriorPanel.SetActive(false);
        distributeRewardPanel.SetActive(false);
        blockPanel.SetActive(false);
    }

    void ShowShareReward(int amt, List<Hero> heroes){
      remainingReward = amt;
      SetRemainingRewardText();

      allHeroes = heroes;

      archerGold = 0;
      archerWP = 0;
      dwarfGold = 0;
      dwarfWP = 0;
      mageGold =0;
      mageWP = 0;
      warriorGold = 0;
      warriorWP = 0;


      foreach(Hero a in allHeroes){
        if(GameManager.instance.MainHero.TokenName.Equals(a.TokenName)){
          distributeRewardPanel.SetActive(true);
        }
        else{
          photonView.RPC("blockHeroesRPC", RpcTarget.AllViaServer, new object[] {a.TokenName});
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

    void HideShareReward(){
      Finalize();
      warriorGoldText.text = "" + 0;
      warriorWPText.text = "" + 0;
      warriorPanel.SetActive(false);

      mageGoldText.text = "" + 0;
      mageWPText.text = "" + 0;
      magePanel.SetActive(false);

      dwarfGoldText.text = "" + 0;
      dwarfWPText.text = "" + 0;
      dwarfPanel.SetActive(false);

      archerGoldText.text = "" + 0;
      archerWPText.text = "" + 0;
      archerPanel.SetActive(false);

      foreach(Hero a in allHeroes){
        if(GameManager.instance.MainHero.TokenName.Equals(a.TokenName)){
          distributeRewardPanel.SetActive(false);
        }
        else{
          photonView.RPC("unblockHeroesRPC", RpcTarget.AllViaServer, new object[] {a.TokenName});
        }

    }
  }

  void Finalize(){
    while(archerGold != 0){
      GoldCoin.Factory("Archer");
      archerGold--;
    }
    while(dwarfGold != 0){
      GoldCoin.Factory("Dwarf");
      dwarfGold--;
    }
    while(mageGold != 0){
      GoldCoin.Factory("Mage");
      mageGold--;
    }
    while(warriorGold != 0){
      GoldCoin.Factory("Warrior");
      warriorGold--;
    }
    while(archerWP != 0){
      photonView.RPC("addWPRPC", RpcTarget.AllViaServer, new object[] {"Archer"});
      archerWP--;
    }
    while(dwarfWP != 0){
      photonView.RPC("addWPRPC", RpcTarget.AllViaServer, new object[] {"Dwarf"});
      dwarfWP--;
    }
    while(mageWP != 0){
      photonView.RPC("addWPRPC", RpcTarget.AllViaServer, new object[] {"Mage"});
      mageWP--;
    }
    while(warriorWP != 0){
      photonView.RPC("addWPRPC", RpcTarget.AllViaServer, new object[] {"Warrior"});
      warriorWP--;
    }
  }

  [PunRPC]
  void blockHeroesRPC(string heroName){
    if(heroName.Equals(GameManager.instance.MainHero.TokenName)){
      blockPanel.SetActive(true);
    }
  }

  [PunRPC]
  void unblockHeroesRPC(string heroName){
    if(heroName.Equals(GameManager.instance.MainHero.TokenName)){
      blockPanel.SetActive(false);
    }
  }

  [PunRPC]
  void addWPRPC(string heroName){
    Hero hero = GameManager.instance.findHero(heroName);
    int currWP = hero.Willpower;
    currWP++;
    hero.Willpower = currWP;
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

}
