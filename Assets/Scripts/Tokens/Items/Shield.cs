public class Shield {
    public static string name = "Shield";
    public static string desc = "Each side of the shield can be used once to help avoiding losing willpower points after a battle round, or against an event card.";

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            
            
        }
   }
}