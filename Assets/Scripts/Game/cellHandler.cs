﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class cellHandler : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);
    private Color32 hoverColor = new Color(1f,1f,1f,.2f);

    public GameObject[] neighbours;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    void OnMouseEnter()
    {
        sprite.color = hoverColor;

    }

    void OnMouseExit()
    {
        sprite.color = color;
    }
}
