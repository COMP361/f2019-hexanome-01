using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : Movable {
    static Color color = Color.white;
    bool isAttached;

    Sprite sprite;
    Sprite attachedSprite;
    SpriteRenderer sr;

    void OnEnable() {
        EventManager.CellUpdate += Destroy;
    }

    void OnDisable() {
        EventManager.CellUpdate -= Destroy;
    }

    void Awake() {
        sprite = Resources.Load<Sprite>("Sprites/icons/farmer");
        attachedSprite = Resources.Load<Sprite>("Sprites/icons/farmer-attached");
        sr = gameObject.AddComponent<SpriteRenderer>();
        Detach();
    }
    
    public static Farmer Factory(int cellID) {
        GameObject go = new GameObject("farmer");
        go.transform.parent = GameObject.Find("Tokens").transform;
        Farmer farmer = go.AddComponent<Farmer>();
        go.transform.localScale = new Vector3(15, 15, 15);
        farmer.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        farmer.Cell = cell;

        return farmer;
    }

    void Destroy(Token token) {
        if(Cell.Inventory.Enemies.Count > 0) {
            EventManager.TriggerFarmerDestroyed(this);
            Destroy(gameObject);
        }
    }
    
    public void Attach() {
        if (attachedSprite != null) {
            sr.sprite = attachedSprite;
        }
        isAttached = true;
    }

    public void Detach() {
        if (attachedSprite != null) {
            sr.sprite = sprite;
        }

        isAttached = false;
    }

    public static string Type { get => typeof(Farmer).ToString(); }
}
