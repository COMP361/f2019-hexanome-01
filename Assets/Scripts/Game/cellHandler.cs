using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class cellHandler : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);
    private Color32 hoverColor = new Color(1f,1f,1f,.2f);

    public int ID;
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

    public GameObject[] AllAccessibleCells(int numberOfMoves)
    {
        GameObject[] accessibleCells = new GameObject[80];
        Queue<GameObject> queue = new Queue<GameObject>();


        foreach (var cell in neighbours)
        {
            queue.Enqueue(cell);
        }


        return accessibleCells;
    }
}
