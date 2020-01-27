using UnityEngine;

public class ActionDisplay : MonoBehaviour {
    void OnEnable() {
        EventManager.ActionUpdate += UpdateAction; 
        EventManager.PlayerUpdate += UpdatePlayer;   
    }

    void OnDisable() {
        EventManager.ActionUpdate -= UpdateAction;
        EventManager.PlayerUpdate -= UpdatePlayer;
    }

    public void UpdateAction(Action action) {
        transform.Find("Action").GetComponent<UnityEngine.UI.Text>().text = action.Name;
    }

    public void UpdatePlayer(Hero hero) {
        transform.Find("Hero").GetComponent<UnityEngine.UI.Text>().text = hero.Type;
    }
}