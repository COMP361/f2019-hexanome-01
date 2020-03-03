using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : Movable {
    static Color color = Color.white;

    protected override void Awake() {
        Sprite iconSprite = Resources.Load<Sprite>("Sprites/icons/farmer");

        if (iconSprite != null) {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = iconSprite;
        }

        transform.localScale.Set(1f, 1f, 1f);
        base.Awake();
    }
    public static Farmer Factory(int cellID) {
        //GameObject go = Geometry.Disc(Vector3.zero, color);
       
        GameObject go = new GameObject("farmer");

        Farmer farmer = go.AddComponent<Farmer>();
        go.transform.localScale = new Vector3(15, 15, 15);
        farmer.TokenName = Type;

        Cell cell = Cell.FromId(cellID);
        farmer.Cell = cell;

        return farmer;
    }

    public static string Type { get => typeof(Farmer).ToString(); }


}
