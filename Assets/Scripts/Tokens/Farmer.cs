using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : Movable {
    static Color color = Color.white;

    Sprite sprite;
    Sprite attachedSprite;
    SpriteRenderer sr;

    void OnEnable() {
        EventManager.CellUpdate += Destroy;
        base.OnEnable();
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
        Farmer farmer = go.AddComponent<Farmer>();
        go.transform.localScale = new Vector3(15, 15, 15);
        farmer.TokenName = Type;
        farmer.Cell = Cell.FromId(cellID);

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
    }

    public void Detach() {
        if (attachedSprite != null) {
            sr.sprite = sprite;
        }
    }

    public static string Type { get => typeof(Farmer).ToString(); }
}
