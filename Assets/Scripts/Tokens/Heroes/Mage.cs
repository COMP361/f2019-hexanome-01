using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    static Mage _instance;

    static void Factory()
    {
        Color color = new Color(0.6f, 0.2f, 1, 1);

        GameObject go = new GameObject();
        Mage mage = go.AddComponent<Mage>();
        mage.Type = typeof(Mage).ToString();
        go.name = mage.Type.ToString();

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + mage.Type.ToString() + "-" + mage.Sex.ToString());
        renderer.sprite = sprite;
        renderer.sortingOrder = 3;
        go.transform.localScale = new Vector3(10, 10, 10);

        mage.Color = color;
        mage.TokenName = mage.Type;

        mage.rank = 34;
        mage.Cell = Cell.FromId(mage.rank);

        mage.Dices = new int[21] {
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1
        };

        mage.names = new string[2] {
            "Eara",
            "Liphardus"
        };

        mage.heroDescription = "EARA \n Wizard of the north - Rand 34 \n Ability: Immediatately after rolling her die during battle, Eara may flip her die to its opposite side. During a team battle. Eara may flip another's hero die instead of her own.";

        mage.Init();
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
