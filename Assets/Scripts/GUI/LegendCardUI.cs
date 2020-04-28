using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class LegendCardUI : MonoBehaviour {
    GameObject panel;
    Text text;
    Text id;
    Button confirmBtn;
    LegendCard card;

    void OnEnable() {
        EventManager.LegendCard += Show;
    }

    void OnDisable() {
        EventManager.LegendCard -= Show;
    }

    void Awake()
    {
        panel = transform.Find("Panel").gameObject;
        text = transform.Find("Panel/Text").GetComponent<Text>();
        id = transform.Find("Panel/Id").GetComponent<Text>();
        confirmBtn = transform.Find("Panel/Confirm").GetComponent<Button>();

        confirmBtn.onClick.AddListener(delegate { Hide(); });
    }

    public void Show(LegendCard card) {
        this.card = card;
        text.text = card.story;
        text.text += "\n" + card.effect;
        id.text = "" + card.id;

        panel.SetActive(true);
    }

    /*public void Apply() {
        Debug.Log(card.id);
        card.ApplyEffect();
        Hide();
        card = null;
    }*/

    public void Hide() {
        panel.SetActive(false);
    }


}
