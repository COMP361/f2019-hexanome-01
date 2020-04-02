using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    static Warrior _instance;

    static void Factory()
    {
        Color color = new Color(0.09f, 0.6f, 1, 1);

        GameObject go = new GameObject();
        Warrior warrior = go.AddComponent<Warrior>();
        warrior.Type = typeof(Warrior).ToString();
        go.name = warrior.Type.ToString();

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + warrior.Type.ToString() + "-" + warrior.sex.ToString());
        renderer.sprite = sprite;
        renderer.sortingOrder = 20;
        go.transform.localScale = new Vector3(10, 10, 1);

        warrior.Color = color;
        warrior.TokenName = warrior.Type;

        warrior.rank = 14;
        warrior.Cell = Cell.FromId(warrior.rank);

        warrior.Dices = new int[21] {
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4
        };

        warrior.names = new string[2] {
            "Mairen",
            "Thorn"
        };

        warrior.heroDescription = "THORN \n Warrior of the Reeded Lands - Rank 14 \n Ability: Each time Thorn empties a well, he gains 5 willpower points (instead of 3)";

        warrior.Init();
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Warrior) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Warrior Instance
    {
        get
        {
            if (!_instance)
            {
                Warrior.Factory();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}
