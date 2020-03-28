using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Witch : Token {
    static Witch _instance;
    public string Type { get; protected set; }
    
    
    public static void Factory(int cellID)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Witch");
        GameObject go = new GameObject( typeof(Witch).ToString());
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        go.transform.localScale = new Vector3(10, 10, 10);

        Witch witch = go.AddComponent<Witch>();
        witch.Type = typeof(Witch).ToString();
        witch.Cell = Cell.FromId(cellID);
    }

    void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(Witch) + "! eliminating " + name + " while preserving " + Instance.name);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static Witch Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }    
}