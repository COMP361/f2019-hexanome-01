using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    GameManager gm;

    //void Start() {
        //gm = GameManager.Instance;
    //}

    void Update() {
        // Check if click is not over a UI element
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(hit.collider != null) {
                Debug.Log(hit.collider.name);

                //hit.collider.gameObject.GetComponent<ClickHandler>().OnClick();
                //Dispatch(hit.collider.gameObject);
            }

            //if(hit2) {
            //    Debug.Log(hit2.transform.gameObject.name);
                //objectHit.SendMessage("LoadScene",1,SendMessageOptions.DontRequireReceiver);
            //}

        } else {

        }
    }

    /*void Dispatch(GameObject go) {
        //switch (gm.CurrentPlayerAction) {
        //    case Action.Move :
        //        gm.Move.Process(go);
        //    default:
        //}
    }*/
}