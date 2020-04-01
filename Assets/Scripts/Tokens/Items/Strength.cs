using Photon.Pun;
using Photon.Realtime;

public class Strength {
    public static string name = "Strength";
    public static string desc = "A strength point helps you battle monsters by making you stronger.";

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = (hero.GetType() == typeof(Dwarf) && hero.Cell.Index == 71) ? 1 : 2;

        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            GameManager.instance.photonView.RPC("AddStrengthRPC", RpcTarget.AllViaServer, new object[] {1, hero.TokenName});
        }

        else{
          EventManager.TriggerBuyError(0);
          return;
        }
   }
}
