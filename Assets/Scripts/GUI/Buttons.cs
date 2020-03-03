using UnityEngine;
using UnityEngine.UI;

class Buttons {

    public static void Unlock(Button btn) {
        btn.interactable = true;
    }

    public static void Lock(Button btn) {
        btn.interactable = false;
    }
}