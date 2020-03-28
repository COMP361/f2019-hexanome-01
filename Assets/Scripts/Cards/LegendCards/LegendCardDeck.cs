﻿using UnityEngine;
using System.Collections.Generic;

public class LegendCardDeck {

    // Order of cards: A3-A4-C1-C2-witch-runestones-G-N
    List<LegendCard> cards = new List<LegendCard>();

    public LegendCardDeck(bool isEasy) {
        cards.Add(new A3(isEasy));
        cards.Add(new A4());
        cards.Add(new C1(isEasy));
        cards.Add(new C2());
        cards.Add(new WitchCard());
        cards.Add(new RunestoneCard(isEasy));
        cards.Add(new G());
        cards.Add(new N());
    }

    public LegendCard getCard(string id)
    {
        return cards.Find(x => x.id == id);
    }
}
