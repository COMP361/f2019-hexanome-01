using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class GameManager2 : MonoBehaviour {
    public List<Hero> heroes = new List<Hero>();
    public Skral monster;

    public Thorald thorald;

    public Hero CurrentPlayer {
        get {
            return Dwarf.Instance;
        }
    }

     public Hero MainHero {
        get {
            return Dwarf.Instance;
        }
    }

    void Awake() {
        heroes.Add(Dwarf.Instance);
        heroes.Add(Mage.Instance);
        heroes.Add(Warrior.Instance);
        heroes.Add(Archer.Instance);

        Dwarf.Instance.Cell = Cell.FromId(25);
        Mage.Instance.Cell = Cell.FromId(25);
        Warrior.Instance.Cell = Cell.FromId(25);
        Archer.Instance.Cell = Cell.FromId(25);

        monster = Skral.Factory(25);
        thorald = Thorald.Instance;
        thorald.Cell = Cell.FromId(25);
    }
}
