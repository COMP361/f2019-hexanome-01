using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorald : Movable
{
    static Thorald _instance;

    public string Type { get; protected set; }
    public Color Color { get; set; }
    protected int rank;
    
    static void Factory()
    {
        Color color = new Color(0, 0, 0, 1);
        
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Thorald");
        GameObject go = new GameObject("Warrior");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        go.transform.localScale = new Vector3(10, 10, 10);

        Thorald thorald = go.AddComponent<Thorald>();
        thorald.Color = color;

        thorald.Type = typeof(Thorald).ToString();
        thorald.TokenName = thorald.Type;

        thorald.rank = 72;
        thorald.Cell = Cell.FromId(thorald.rank);
        thorald.MovePerHour = 4;
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Thorald) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Thorald Instance
    {
        get
        {
            if (!_instance)
            {
                Thorald.Factory();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
}
