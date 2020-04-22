using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Hero
{
    static Dwarf _instance;

    static void Factory()
    {
        Color color = new Color(1, 0.9f, 0, 1);

        GameObject go = new GameObject();
        Dwarf dwarf = go.AddComponent<Dwarf>();
        dwarf.Type = typeof(Dwarf).ToString();
        go.name = dwarf.Type;

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + dwarf.Type.ToString() + "-" + dwarf.Sex.ToString());
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        dwarf.Color = color;
        dwarf.TokenName = dwarf.Type;

        dwarf.rank = 7;
        dwarf.Cell = Cell.FromId(dwarf.rank);

        dwarf.Dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3
        };

        dwarf.names = new string[2] {
            "Brigha",
            "Kram"
        };

        dwarf.heroDescription = dwarf.HeroName + " \n Dwarf of the deep Mines - Rank " + dwarf.rank + " \n\n";
        dwarf.heroDescription += "Ability: When " + dwarf.HeroName + " buys strength points in space 71 (the mine), " + ((dwarf.Sex == Sex.Female)?"she":"he") + " may buy each strength point for 1 gold.";

        dwarf.Init();
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
