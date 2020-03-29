public class Telescope {
    public static string name = "Telescope";
    public static string desc = "The telescope can be used to d reveal all hidden tokens on adjacent spaces.";

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            
            
        }
   }
}