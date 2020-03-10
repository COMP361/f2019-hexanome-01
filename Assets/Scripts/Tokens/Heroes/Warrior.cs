using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    static Warrior _instance;

    static void Factory()
    {
        Color color = new Color(0.09f, 0.6f, 1, 1);
        //GameObject go = Geometry.Disc(Vector3.zero, color);

        Sprite sprite = Resources.Load<Sprite>("Sprites/heroes/male_warrior");
        GameObject go = new GameObject("Warrior");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 20;
        go.transform.localScale = new Vector3(10, 10, 1);

        Warrior warrior = go.AddComponent<Warrior>();
        warrior.Color = color;

        warrior.Type = typeof(Warrior).ToString();
        warrior.TokenName = warrior.Type;

        warrior.rank = 14;
        Cell cell = Cell.FromId(warrior.rank);
        warrior.Cell = cell;
        warrior.State = new HeroState(cell, color, warrior.name, warrior.Type.ToString());

        //warrior.IsDone = false;

        warrior.Dices = new int[21] {
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4
        };

        warrior.names = new string[2] {
            "Mairen",
            "Thorn"
        };
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