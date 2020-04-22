using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    static Archer _instance;

    static void Factory()
    {
        Color color = new Color(0.4f, 0.75f, 0, 1);

        GameObject go = new GameObject();
        Archer archer = go.AddComponent<Archer>();
        archer.Type = typeof(Archer).ToString();
        go.name = archer.Type;

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + archer.Type.ToString() + "-" + archer.Sex.ToString());
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        archer.Color = color;
        archer.TokenName = archer.Type;

        archer.rank = 25;
        archer.Cell = Cell.FromId(archer.rank);

        archer.Dices = new int[21] {
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5
        };

        archer.names = new string[2] {
            "Chada",
            "Pasco"
        };

        archer.heroDescription = archer.HeroName + " \nArcher of the Watchful Forest - Rank " + archer.rank + " \n\n";
        archer.heroDescription += "Ability: During battle, " + archer.HeroName + " must roll " + ((archer.Sex == Sex.Female)?"her":"his") + " dice 1 at a time. ";
        archer.heroDescription += ((archer.Sex == Sex.Female)?"She":"He") + " must choose when to stop rolling and " + ((archer.Sex == Sex.Female)?"she":"he") + " may only use " + ((archer.Sex == Sex.Female)?"her":"his") + " most recent result. ";
        archer.heroDescription += ((archer.Sex == Sex.Female)?"She":"He") + " may fight a monster who is in a adjacent space.";

        archer.Init();
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Archer) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Archer Instance
    {
        get
        {
            if (!_instance)
            {
                Archer.Factory();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}
