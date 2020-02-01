using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInventory : MonoBehaviour
{

  string description;

  void Start() {
    transform.Find("cellsDescription").gameObject.SetActive(false);
  }

  void OnEnable() {
    EventManager.CellMouseEnter += Show;
    EventManager.CellMouseLeave += UnShow;
  }

  void OnDisable() {
    EventManager.CellMouseEnter -= Show;
    EventManager.CellMouseLeave -= UnShow;
  }

  void Show(int CellId){
    formatDescription(CellId);
    transform.Find("cellsDescription").GetComponent<Text>().text = description;
    transform.Find("cellsDescription").gameObject.SetActive(true);
  }

  void UnShow(int CellId){
    transform.Find("cellsDescription").gameObject.SetActive(false);
  }

  public void formatDescription(int CellId) {
    this.description = "Heroes: \n";
    
    foreach (var hero in Cell.FromId(CellId).State.Heroes) {
      this.description = description + "  - " + hero.TokenName + " \n";
    }

    this.description = description + "Item: \n";
    
    foreach (var item in Cell.FromId(CellId).State.Items) {
      this.description = description + "  - " + item.TokenName + " \n";
    }
    
    this.description = description + "Monster: \n";
    foreach (var enemy in Cell.FromId(CellId).State.Enemies) {
      this.description = description + "  - " + enemy.TokenName + " \n";
    }

    this.description = description + "Gold: \n";
    foreach (var gold in Cell.FromId(CellId).State.Golds) {
      this.description = description + "  - " + gold.TokenName + " \n";
    }
  }
}
