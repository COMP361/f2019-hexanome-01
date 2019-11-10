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

    public void OnMouseDown()
    {
        AllAccessibleCells();
    }

    void OnMouseEnter()
    {
        sprite.color = hoverColor;

    }

    void OnMouseExit()
    {
        sprite.color = color;
    }

    public HashSet<GameObject> AllAccessibleCells(/*int numberOfMoves*/)
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        HashSet<GameObject> visitedCells = new HashSet<GameObject>();
        string id = this.name;
        
        foreach (var cell in this.neighbours)
        {
            queue.Enqueue(cell);
            sprite.color = Color.magenta;
        }
        int i = 2; // this is just for testing purposes
        while( i > 0) {
            var cell = queue.Dequeue();

            if (visitedCells.Contains(cell))
            {
                continue;
            }

            visitedCells.Add(cell);

            foreach (var neighbor in cell.GetComponent<cellHandler>().neighbours)
            {
                if (!visitedCells.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }

            i--;
        }

        return visitedCells;
    }
}
