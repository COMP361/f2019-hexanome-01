using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : Token
{
  public static string name = "Helm";
  public static string desc = "A helm allows you to total up all identical dice values in a battle.";
   
  public static Helm Factory()
    {
      Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/Gold");
      GameObject go = new GameObject("Helm");
      SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
      renderer.sprite = sprite;
      renderer.sortingOrder = 2;

      Helm helm = go.AddComponent<Helm>();
      helm.TokenName = Type;

      return helm;
    }

    public static Helm Factory(int cellID){
      Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Helmet");
      GameObject go = new GameObject("Helm");
      SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
      renderer.sprite = sprite;
      renderer.sortingOrder = 2;

      Helm helm = go.AddComponent<Helm>();
      helm.TokenName = Type;
      helm.Cell = Cell.FromId(cellID);

      return helm;
    }

    public static string Type { get => typeof(Helm).ToString(); }

    public static void Buy() {
        Hero hero = GameManager.instance.MainHero;
        int cost = 2;
        
        if(hero.heroInventory.numOfGold >= cost) {
            hero.heroInventory.RemoveGold(cost);
            // add Helm to Inventory
        }
    }
  }
