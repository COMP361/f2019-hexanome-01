using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    static Mage _instance;

    static void Factory()
    {
        Color color = new Color(0.6f, 0.2f, 1, 1);
        //GameObject go = Geometry.Disc(Vector3.zero, color);

        Sprite sprite = Resources.Load<Sprite>("Sprites/heroes/male_wizard");
        GameObject go = new GameObject("Warrior");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        go.transform.localScale = new Vector3(10, 10, 10);

        Mage mage = go.AddComponent<Mage>();
        mage.Color = color;

        mage.Type = typeof(Mage).ToString();
        mage.TokenName = mage.Type;

        mage.rank = 34;
        Cell cell = Cell.FromId(mage.rank);
        mage.Cell = cell;
        mage.State = new HeroState(cell, color, mage.name, mage.Type.ToString());

        //mage.IsDone = false;

        mage.Dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1
        };

        mage.names = new string[2] {
            "Eara",
            "Liphardus"
        };
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Mage) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Mage Instance
    {
        get
        {
            if (!_instance)
            {
                Mage.Factory();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}