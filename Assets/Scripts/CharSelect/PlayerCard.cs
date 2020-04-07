using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour {

    //Public Fields (Set by Inspector)
    public Button LockButton;
    public GameObject ActiveUI;

    //Private Members
    Text m_statusText;
    GameObject m_stage1;
    GameObject m_stage2;
    HeroUI[] m_heroUI;
    //private bool m_isMale = true;

    public PhotonView photonView;

    //Properties
    public HeroType CurrentHero { get; private set; }
    public string Status {
        get { return m_statusText.text; }
        set { m_statusText.text = value; }
    }

    void Awake() {
        m_stage1 = ActiveUI.transform.GetChild(0).gameObject;
        m_stage2 = ActiveUI.transform.GetChild(1).gameObject;
        m_statusText = LockButton.GetComponentInChildren<Text>();
        m_heroUI = new HeroUI[4];
        for (int i = 0; i < 4; i++) {
            Button heroButton = m_stage1.transform.GetChild(i).GetComponent<Button>();
            GameObject card_m = m_stage2.transform.GetChild(0).GetChild(i).gameObject;
            GameObject card_f = m_stage2.transform.GetChild(1).GetChild(i).gameObject;
            m_heroUI[i] = new HeroUI(heroButton, card_m, card_f);
        }
    }

    // Button Functions
    public void updateHero(int newHero) {
        if(!PhotonNetwork.OfflineMode) {
            photonView.RPC("receiveUpdateHero", RpcTarget.AllViaServer, newHero);
        } else {
            receiveUpdateHero(newHero);   
        }
    }

    [PunRPC]
    public void receiveUpdateHero(int newHero)
    {
        m_heroUI[(int)CurrentHero].toggleCards(false);
        m_heroUI[newHero].toggleCards(true);
        toggleHeroSelection(false);
        CurrentHero = (HeroType)newHero;
    }

     public void toggleHeroSelection(bool toggleOn) {
        
        m_stage1.SetActive(toggleOn);
        m_stage2.SetActive(!toggleOn);
        LockButton.interactable = !toggleOn;
        Status = toggleOn ? "Select a Character" : "LOCK";
    }

    // Card State Change Functions 
    public void setAsLocked() {
        LockButton.interactable = false;
        m_stage2.transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(2).GetChild((int)CurrentHero).gameObject.SetActive(true);
    }

    public void setAsCurrent() {
        ActiveUI.SetActive(true);
        toggleHeroSelection(true);
    }
    public void disableHero(HeroType hero) {
        m_heroUI[(int)hero].toggleButton(false);
    }
}

public class HeroUI {

    Button m_button;
    GameObject m_cardM;
    GameObject m_cardF;
    public HeroUI(Button button, GameObject card_m, GameObject card_f) {
        m_button = button;
        m_cardM = card_m;
        m_cardF = card_f;
    }

    public void toggleButton(bool isInteractable) {
        m_button.interactable = isInteractable;
    }
    
    public void toggleCards(bool toggleOn) {
        m_cardM.SetActive(toggleOn);
        m_cardF.SetActive(toggleOn);
    }


}
