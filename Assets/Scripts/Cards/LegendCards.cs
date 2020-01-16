using UnityEngine;
using System.Collections.Generic;

public class LegendCards {
    List<LegendCard> cards = new List<LegendCard>();

    public LegendCards() {
        LegendCard card = new LegendCard("A3");
        card.intro = "A gloomy mood has fallen upon the people. Rumors are making the rounds that skarls have set up a stronghold in some undisclosed location. " +
            "The heroes have scattered themselves acrosse the entire land in search of this location. The defense in the castle is in their hands alone. " +
            "Many farmers have asked for help and are seeking shelter behind the high walls of Rietburg Castle.";
        card.additionalInfo = "Farmers can be brought into the castle. The players can move their heroes to a farmer token and carry it along with them. " +
            "The players are allowed to carry multiple farmers. " +
            "If they encounter a creature while carrying farmers, the farmers immediately die. " +
            "For every farmer brought back to the castle, one more creature is allowed in the castle.";
        card.task = "";
        cards.Add(card);
        
        card = new LegendCard("A4");
        card.intro = "At first sunlight, the heroes receive a message: Old King Brandur's willpower seem to have weakened with the passage of time. " +
            "But there is said to be a herb growing in the mountain passes that can revive a person's life.";
        card.task = "The players must heal the king with the medicinal herb. " +
            "To do that, they must find the witch, only she knows where the locations where this herb grows. " +
            "She shall be hidden in the fog.";
        cards.Add(card);

        card = new LegendCard("C1");
        card.intro = "Rumors are circulating about cruel wardraks from the south. " +
            "They have not yet been sighted, but more and more farmers are losing their courage, leaving their farmsteads, and seeking safety in the castle.";
        card.additionalInfo = "The king's scouts have discovered the skarl stronghold.";
        card.task = "The skral on the tower must be defeated as soon as possible.";
        cards.Add(card);

        card = new LegendCard("Witch");
        card.intro = "You have discovered the witch named Reka. ";
        card.effect = "The hero standing on the witch's space activates the fog token and gets her magic potion for free. " +
            "From now on, the players can purchase witch's brews. " +
            "It doubles the values of one's die roll and each potion can be used twice.";
        card.additionalInfo = "She tells you the location of the medicinal herb.";
        card.task = "You have to bring the medicinal herb to the castle before the Narrator reaches the castle.";
        cards.Add(card);

        card = new LegendCard("RuneStonesFoundWitch");
        card.intro = "Reka the witch tells the players about an ancient magic that still holds power: rune stones!";
        card.effect = "If a hero has 3 different coloured rune stones on his hero board he gets one black die which has higher values than the hero dice. " +
            "As long as he possesses the runestones, he is allowed to use this black die in battle instead of his own die.";
        card.additionalInfo = "";
        card.task = "";
        cards.Add(card);

        card = new LegendCard("RuneStonesNotFoundWitch");
        card.intro = "The players learn about an ancient magic that still holds power: rune stones!";
        card.effect = "If a hero has 3 different coloured rune stones on his hero board he gets one black die which has higher values than the hero dice. " +
            "As long as he possesses the runestones, he is allowed to use this black die in battle instead of his own die.";
        card.additionalInfo = "";
        card.task = "";
        cards.Add(card);

        card = new LegendCard("C2");
        card.intro = "Good news from the south: Prince Thorald, just back from a battle on the edge of the southern forest, is preparing himself to help the heroes.";
        card.effect = "If Thorald is standing in the space as a creature, 4 extra strength points are added for the heroes in a battle with a creature.";
        card.additionalInfo = "A player can move Prince Thorald up to 4 spaces multiple times costing 1 hour each time. " +
            "Prince Thorald accompanies the heroes up to letter G on the Legend Track.";
        card.task = "Legend goals: \n-Bring the medicinal herb to the castle before the Narrator reaches the castle.\n" +
            "-Defend the castle.\n" +
            "-Defeat the skral on the tower.";
        cards.Add(card);

        card = new LegendCard("G");
        card.intro = "Prince Thorald joins up with a scouting patrol with the intention of leaving for just a few days. " +
            "He is not to be seen again for quite a long time.";
        card.effect = "Prince Thorald is removed from the game.";
        card.additionalInfo = "Black shadows are moving in the moonlight. The rumors were right the wardraks are coming!";
        card.task = "";
        cards.Add(card);

        card = new LegendCard("Win");
        card.intro = "Congrats! With the heroes' combined powers, you were able to take the skral's stronghold. The medicinal herb did its work as well, and King Brandur soon felt better. " +
            "And yet, the heroes still felt troubled. " +
            "The King's son, Prince Thorald, had not yet returned. What is keeping him so long?";
        card.effect = "";
        card.additionalInfo = "";
        card.task = "";
        cards.Add(card);

        card = new LegendCard("Loose");
        card.intro = "You failed.";
        card.effect = "";
        card.additionalInfo = "";
        card.task = "";
        cards.Add(card);
    }

    private class LegendCard {
        public string id;
        public string intro;
        public string task;
        public string effect;
        public string additionalInfo;
        public bool group = false;

        public LegendCard(string id) {
            this.id = id;
        }
    }
}
