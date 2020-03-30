using UnityEngine;
using System.Collections.Generic;

public class EventCardDeck {
    static EventCardDeck _instance = null;
    static List<EventCard> cards = new List<EventCard>();

    private EventCardDeck() {
        cards.Add(new LC2());
        cards.Add(new LC4());
        cards.Add(new LC5());
        cards.Add(new LC12());
        cards.Add(new LC13());
        cards.Add(new LC14());
        cards.Add(new LC15());
        cards.Add(new LC17());
        cards.Add(new LC22());
        cards.Add(new LC25());
        cards.Add(new LC28());
        //cards.Add(new LC29());
        //cards.Add(new LC30());
        cards.Add(new LC31());
        cards.Add(new LC32());

        cards.Shuffle();
    }

    public void GetCard() {
        // Card is shifted to the end of the deck
        EventCard card = cards[0];
        cards.RemoveAt(0);
        cards.Add(card);
        
        EventManager.TriggerEventCard(cards[0]);
    }

    public static EventCardDeck Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventCardDeck();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}