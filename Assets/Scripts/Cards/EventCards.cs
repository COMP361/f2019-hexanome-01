using UnityEngine;
using System.Collections.Generic;

public class EventCards {
    List<Card> cards = new List<Card>();

    public void Shuffle() {
        for (int i = 0; i < cards.Count; i++) {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public EventCards() {
        Card card = new Card(1);
        card.intro = "The dwarf merchant Garz makes an offer.";
        card.effect = "Each hero can now purchase any article from the equipment store (except the witch brew) in exchange for 3 willpower points."; 
        cards.Add(card);

        card = new Card(2);
        card.intro = "A biting wind blows across the coast from the sea.";
        card.effect = "Each hero standing on a space with a number between 0 and 20 now loses 3 willpower points."; 
        card.shield = true;
        cards.Add(card);
        
        card = new Card(3);
        card.intro = "Wisdom from the Tree of Songs.";
        card.effect = "A hero who enters the Tree of Songs space or is already standing there gets 1 strength point. If more than one hero is standing there, the one with the highest rank gets the strength point. Now place this card on space 57 until a hero has gotten the strength point. Then remove it from the game."; 
        cards.Add(card);
        
        card = new Card(4);
        card.intro = "Poisonous vapors from the mountains are tormenting the heroes.";
        card.effect = "Each hero standing on a space with a number between 37 and 70 now loses 3 willpower points.";
        card.shield = true; 
        cards.Add(card);
        
        card = new Card(5);
        card.intro = "A farm girl sings a beautiful song that wafts across the northern woods. But it fails to stir the hearts of all the heroes.";
        card.effect = "The wizard and the archer immediately get 3 willpower points each.";
        cards.Add(card);

        card = new Card(6);
        card.intro = "The dwarf merchant Garz invites one of the heroes to have a drink.";
        card.effect = "The hero with the lowest rank gets to decide if he wants to roll one of his hero dice. If he rolls 1, 2, 3, or 4, he loses the rolled number of willpower points. If he rolls 5 or 6, he wins that number of willpower points.";
        cards.Add(card);

        card = new Card(7);
        card.intro = "Sulfurous mists surround the heroes.";
        card.effect = "The hero with the lowest rank rolls one of his hero dice. The group loses the rolled number of willpower points.";
        card.shield = true;
        cards.Add(card);

        card = new Card(8);
        card.intro = "Trading ships reach the coast of Andor.";
        card.effect = "A hero who enters space 9 or is already standing there can buy 2 strength points there for just 2 gold. Place this card on space 9 until a hero has made the purchase. Then remove it from the game.";
        cards.Add(card);

        card = new Card(9);
        card.intro = "Dark clouds cover the sun, filling all the good people of Andor with a strange foreboding.";
        card.effect = "On this day, no hero is allowed to use a 10th hour. Place this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
        card.shield = true;
        cards.Add(card);

        card = new Card(10);
        card.intro = "Jugglers from the north display their art.";
        card.effect = "Each hero can now purchase 3 willpower points in exchange for 1 gold.";
        cards.Add(card);

        card = new Card(11);
        card.intro = "The creatures gather their strength.";
        card.effect = "In this day, each creature has 1 extra strength point. Place this card next to the creature display. At the end of the day, it is removed from the game.";
        card.shield = true;
        cards.Add(card);
        
        card = new Card(12);
        card.intro = "The heroes replenish their water supplies at the river.";
        card.effect = "Each hero who is now standing on a space bordering the river gets a wineskin.";
        cards.Add(card);

        card = new Card(13);
        card.intro = "The lovely sound of a horn echoes across the land.";
        card.effect = "Each hero who has fewer than 10 willpower points can immediately raise his total to 10.";
        cards.Add(card);

        card = new Card(14);
        card.intro = "A fragment of a very old sculpture has been found. Not all of the heroes are able to appreciate that kind of handiwork.";
        card.effect = "The dwarf and the warrior immediately get 3 willpower points each.";
        cards.Add(card);

        card = new Card(15);
        card.intro = "Rampaging creatures despoil the well in the south of Andor.";
        card.effect = "The well token on space 35 is removed from the game.";
        card.shield = true;
        cards.Add(card);

        card = new Card(16);
        card.intro = "Royal falcons fly high above the land, keeping watch.";
        card.effect = "The hero with the highest rank is allowed to take a look at the top card on the event card deck. Then he gets to decide whether to remove the card from the game or to place it back on the deck.";
        card.shield = true;
        cards.Add(card);
        
        card = new Card(17);
        card.intro = "Heavy weather moves across the land.";
        card.effect = "Each hero with more than 12 willpower points immediately reduces his point total to 12.";
        card.shield = true;
        cards.Add(card);
        
        card = new Card(18);
        card.intro = "A wild gor storms forth.";
        card.effect = "The gor on the space with the lowest nummber now moves one space in the direction of the arrow. The group can prevent that by paying willpower points. For 3 heroes, 4 willpower points, for 4 heroes, 6 willpower points.";
        card.shield = true;
        cards.Add(card);
        
        card = new Card(19);
        card.intro = "An exhausting day.";
        card.effect = "On this day, the 9th and the 10th hours will each cost 3 willpower points instead of 2. Place this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
        card.shield = true;
        cards.Add(card);

        card = new Card(20);
        card.intro = "A farmer falls ill.";
        card.effect = "One farmer token on the game board that has not yet been taken to the castle must be removed from the game. The group can prevent that by paying gold and/or willpower point. For 3 heroes, 3 gold/willpower points, for 4 heroes, 4 gold/willpower points.";
        card.shield = true;
        card.group = true;
        cards.Add(card);

        card = new Card(21);
        card.intro = "A mysterious terror lurks in the southern woods.";
        card.effect = "A hero who enters space 22, 23, 24, or 25 or is already standing there will immediately lose 4 willpower points. If more then one hero is standing there, the one with the highest rank loses the points. Place this card next to space 24 until it is triggered. Then is is removed from the game.";
        card.shield = true;
        cards.Add(card);

        card = new Card(22);
        card.intro = "Rampaging creatures despoil the well at the foot of the mountains.";
        card.effect = "The well token on space 45 is removed from the game.";
        card.shield = true;
        cards.Add(card);
        
        card = new Card(23);
        card.intro = "The king's blacksmiths are laboring tirelessly.";
        card.effect = "Up to two heroes with 6 or fewer strength points can each add 1 strength point to what they already have. You can decide as a group which heroes those will be.";
        card.group = true;
        cards.Add(card);

        card = new Card(24);
        card.intro = "A storm moves across the countryside and weighs upon the mood of teh heroes.";
        card.effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
        card.shield = true;
        cards.Add(card);

        card = new Card(26);
        card.intro = "The minstrels sing a ballad about the deeds of the heroes, strengthening their determination.";
        card.effect = "On this day, the 8th hour costs no willpower points. Place this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
        card.group = true;
        cards.Add(card);

        card = new Card(27);
        card.intro = "The creature are possessed with blind fury.";
        card.effect = "The creature standing on the space with the highest number will now move one space along the arrow. The group can prevent that by paying gold and/or willpower point. For 3 heroes, 3 gold/willpower points, for 4 heroes, 4 gold/willpower points.";
        card.shield = true;
        card.group = true;
        cards.Add(card);

        card = new Card(28);
        card.intro = "A beautifully clear, starry night gives the heroes confidence.";
        card.effect = "Every hero whose time marker is presently in the sunrise box gets 2 willpower points.";
        cards.Add(card);

        card = new Card(29);
        card.intro = "The keepers of the Tree of Songs offer a gift."; 
        card.effect = "Now place a shield on space 57. A hero who enters space 57 or is already standing there can collect the shield. If more than one hero is standing there, the hero with the lowest rank gets the shield.";
        cards.Add(card);

        card = new Card(30);
        card.intro = "A drink in the tavern."; 
        card.effect = "Place a wineskin on the tavern space (72). A hero who enters space 72 or is already standing there can collect the wineskin. If more than one hero is standing there, the hero with the lowest rank gets the wineskin.";
        cards.Add(card);

        card = new Card(31);
        card.intro = "Hot rain from the south lashes the land."; 
        card.effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
        card.shield = true;
        cards.Add(card);

        card = new Card(32);
        card.intro = "A sleepless night awaits the heroes."; 
        card.effect = "Every hero whose time marker is presently in the sunrise box loses 2 willpower points.";
        card.shield = true;
        cards.Add(card);

        card = new Card(33);
        card.intro = "Their adventure is wearing down the heroes."; 
        card.effect = "One of the heroes immediatey loses 1 stength point. You can decide as a group which hero that will be. If no hero has more than 1 strength point, nothing happens.";
        card.shield = true;
        card.group = true;
        cards.Add(card);

        card = new Card(34);
        card.intro = "The dwarf merchant Garz meets one of the heroes and offers him a trade."; 
        card.effect = "One of the heroes can now purchase 10 willpower points in exchange for 2 strength points. You can decide as a group which hero that will be.";
        card.group = true;
        cards.Add(card);
    }

    private class Card {
        public int id;
        public string intro;
        public string effect;
        public bool shield;
        public bool group = false;

        public Card(int id) {
            this.id = id;
            shield = false;
        }
    }
}