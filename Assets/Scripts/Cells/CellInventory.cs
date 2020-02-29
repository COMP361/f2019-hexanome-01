using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInventory : MonoBehaviour
{
  #region Fields

  protected string description;
  protected Transform textTransform;
  public InventoryUICell InventoryUICell;

  public List<Items> items = new List<Items>();
  public List<Hero> Heroes { get; private set; }
  public List<Enemy> Enemies { get; private set; }
  public List<Farmer> Farmers { get; private set; }
  public List<Token> Tokens { get; private set; }
  public List<Token> Golds { get; private set; }
  #endregion

  #region Functions [Constructor]
  public CellInventory()
  {
      Heroes = new List<Hero>();
      Enemies = new List<Enemy>();
      Farmers = new List<Farmer>();
      Tokens = new List<Token>();
      Golds = new List<Token>();

      // Should maybe be in inventoryUICell
      textTransform = transform.Find("cellsDescription");
      textTransform.gameObject.SetActive(false);
      //int numGoldenShields;
  }
  #endregion

  protected virtual void Start() {
    /*
    textTransform = transform.Find("cellsDescription");
    textTransform.gameObject.SetActive(false);
    */
  }

  protected virtual void OnEnable() {
    EventManager.CellMouseEnter += Show;
    EventManager.CellMouseLeave += UnShow;
  }

  protected virtual void OnDisable() {
    EventManager.CellMouseEnter -= Show;
    EventManager.CellMouseLeave -= UnShow;
  }

  protected virtual void Show(int CellId){
    formatDescription(CellId);
    textTransform.GetComponent<Text>().text = description;
    textTransform.gameObject.SetActive(true);
  }

  protected virtual void UnShow(int CellId){
    textTransform.gameObject.SetActive(false);
  }

  public virtual void formatDescription(int CellId) {

    Cell describedCell = Cell.FromId(CellId);

    this.description = "Heroes: \n";
    foreach (var hero in describedCell.State.Heroes) {
      this.description += "  - " + hero.TokenName + " \n";
    }

    this.description += "Monster: \n";
    foreach (var enemy in describedCell.State.Enemies) {
      this.description += "  - " + enemy.TokenName + " \n";
    }

    this.description += "Farmers: \n";
    foreach (var farmer in describedCell.State.Farmers) {
      this.description += "  - " + farmer.TokenName + " \n";
    }

    this.description += "Item: \n";
    foreach (var token in describedCell.State.Tokens) {
      this.description += "  - " + token.TokenName + " \n";
    }

    this.description = description + "Gold: \n";
    foreach (var gold in describedCell.State.Golds) {
      this.description += "  - " + gold.TokenName + " \n";
    }
  }

  private void addMerchantDescription(MerchantCell merchCell)
  {
        this.description += "\nMerchant\n[item-cost]";
        foreach(KeyValuePair<string, int> product in merchCell.products) {
            this.description += "\n" + product.Key + " - " + product.Value;
        }
  }

  public void addToken(Token token) {
      Type listType;

      listType = Heroes.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Heroes.Add((Hero)token);
          return;
      }

      listType = Enemies.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Enemies.Add((Enemy)token);
          return;
      }

      listType = Farmers.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Farmers.Add((Farmer)token);
          return;
      }
  }

  public void removeToken(Token token) {
      Type listType;

      listType = Heroes.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Heroes.Remove((Hero)token);
          return;
      }

      listType = Enemies.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Enemies.Remove((Enemy)token);
          return;
      }

      listType = Farmers.GetListType();
      if (listType.IsCompatibleWith(token.GetType())) {
          Farmers.Remove((Farmer)token);
          return;
      }
  }
}
