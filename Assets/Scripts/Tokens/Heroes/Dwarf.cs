using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Hero
{
    static Dwarf _instance;

    static void Factory()
    {
        Color color = new Color(1, 0.9f, 0, 1);
        //GameObject go = Geometry.Disc(Vector3.zero, color);

        Sprite sprite = Resources.Load<Sprite>("Sprites/heroes/male_dwarf");
        GameObject go = new GameObject("Warrior");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        go.transform.localScale = new Vector3(10, 10, 10);

        Dwarf dwarf = go.AddComponent<Dwarf>();
        dwarf.Color = color;

        dwarf.Type = typeof(Dwarf).ToString();
        dwarf.TokenName = dwarf.Type;

        dwarf.rank = 7;
        Cell cell = Cell.FromId(dwarf.rank);
        dwarf.Cell = cell;
        dwarf.State = new HeroState(color, dwarf.name);

        //dwarf.IsDone = false;

        dwarf.Dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3
        };

        dwarf.names = new string[2] {
            "Brigha",
            "Kram"
        };
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Dwarf) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Dwarf Instance
    {
        get
        {
            if (!_instance)
            {
                Dwarf.Factory();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}
