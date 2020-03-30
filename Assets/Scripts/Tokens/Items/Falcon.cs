public class Falcon {
    public static string name = "Falcon";
    public static string desc = "Two heroes can exchange as many small articles, gold, or gemstones at one time as they like even if they are not standing on the same space.";

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            
            
        }
   }
}