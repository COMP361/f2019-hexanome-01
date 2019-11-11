using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonHandler : MonoBehaviour {
    GameManager gm;

    void Start() {
        gm = GameManager.Instance;
    }

    // Start is called before the first frame update
    public void Move() {
      gm.CurrentPlayerAction = Action.Move;
      gm.UpdateUI();
    }
}
