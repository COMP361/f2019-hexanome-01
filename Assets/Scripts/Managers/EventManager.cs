using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    GameManager gm;

    void Start() {
        gm = GameManager.instance;
    }

    void Update() {
        // Check if click is not over a UI element
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(hit.collider != null) {
                hit.collider.gameObject.GetComponent<IClickHandler>().OnClick();
            }
        }
    }

    // Action needs to be a class
    // OnCellClick
    // Confirm
    // Cancel

    public void OnCellClick(int cellID) {
        if(gm.CurrentPlayer.State.action == Action.Move.SetDest) {
            gm.SetDest(cellID);
        }
    }
}