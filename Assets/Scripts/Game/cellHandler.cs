using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class CellHandler : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color32 color = new Color(1f,1f,1f,0f);
    private Color32 hoverColor = new Color(1f,1f,1f,.2f);
    private Color32 moveHoverColor = new Color(0f,1f,0f,.2f);
    GameManager gm;

    void Start() {
        gm = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    void OnMouseEnter()
    {
        if(gm.CurrentPlayerAction == Action.Move) {
            sprite.color = moveHoverColor;
        } else {
            sprite.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        sprite.color = color;
    }

    void OnMouseDown()
    {
        if(gm.CurrentPlayerAction == Action.Move) {
            gm.CurrentPlayer.Move(this.transform.parent.gameObject);
        }
    }
}
