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
        go.name = mage.Type;

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Heroes/" + mage.Type.ToString() + "-" + mage.Sex.ToString());
        renderer.sprite = sprite;
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

        mage.heroDescription = mage.HeroName + " \n Wizard of the north - Rand " + mage.rank + " \n\n";
        mage.heroDescription += "Ability: Immediatately after rolling " + ((mage.Sex == Sex.Female)?"her":"his") + " die during battle, " + mage.HeroName + " may flip " + ((mage.Sex == Sex.Female)?"her":"his") + " die to its opposite side. ";
        mage.heroDescription += "During a team battle, " + mage.HeroName + " may flip another's hero die instead of " + ((mage.Sex == Sex.Female)?"her":"his") + " own.";

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
