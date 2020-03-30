public class Bow {
    public static string name = "Bow";
    public static string desc = "A hero with a bow may attack a creature in an adjacent space.";

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            
            
        }
   }
}