using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class EventCardUI : MonoBehaviour {   
    GameObject panel;
    Text text;
    Text id;
    Button confirmBtn, shieldBtn;
    EventCard card;

    void OnEnable() {
        EventManager.EventCard += Show;
    }

    void OnDisable() {
        EventManager.EventCard -= Show;
    }
    
    void Awake()
    {
        panel = transform.Find("Panel").gameObject;
        text = transform.Find("Panel/Text").GetComponent<Text>();
        id = transform.Find("Panel/Id").GetComponent<Text>();
        shieldBtn = transform.Find("Panel/Shield").GetComponent<Button>();
        confirmBtn = transform.Find("Panel/Confirm").GetComponent<Button>();

        confirmBtn.onClick.AddListener(delegate { Apply(); });
        shieldBtn.onClick.AddListener(delegate { Skip(); });
    }

    public void Show(EventCard card) {
        this.card = card;
        text.text = card.intro;
        text.text += "\n" + card.effect;
        id.text = "" + card.id;
        
        if(card.shield) {
            shieldBtn.interactable = true;
        } else {
            shieldBtn.interactable = false;
        }

        panel.SetActive(true);
    }

    public void Apply() {
        card.ApplyEffect();
        Hide();
        card = null;
    }

    public void Hide() {
        panel.SetActive(false);
    }

    public void Skip() {
        if(!card.shield) return;
        Hide();
        card = null;
    }
}