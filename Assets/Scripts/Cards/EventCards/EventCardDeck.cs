using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class EventCardDeck {
    static EventCardDeck _instance = null;
    static List<EventCard> cards;

    static List<EventCard> deck = new List<EventCard>() {
        new LC2(),
        new LC4(),
        new LC5(),
        new LC12(),
        new LC13(),
        new LC14(),
        new LC15(),
        new LC17(),
        new LC22(),
        new LC25(),
        new LC28(),
        new LC29(),
        new LC30(),
        new LC31(),
        new LC32()
    };

    private EventCardDeck() {
        int[] shuffledCardIndex = PhotonNetwork.CurrentRoom.CustomProperties["EventCardsIndex"] as int[];
        if(shuffledCardIndex == null) return;

        cards = new List<EventCard>( new EventCard[deck.Count] );

        for(int i = 0; i < deck.Count; i++) {
            if(i < shuffledCardIndex.Length) {
                int index = shuffledCardIndex[i];
                if(index < cards.Count && i < deck.Count) cards[index] = deck[i];
            }
        }
    }

    public void GetCard() {
        if(cards == null || cards.Count == 0) return;

        // Card is shifted to the end of the deck
        EventCard card = cards[0];

        cards.RemoveAt(0);
        cards.Add(card);

        EventManager.TriggerEventCard(cards[0]);
    }

    public static int NumCards() {
        return deck.Count;
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
