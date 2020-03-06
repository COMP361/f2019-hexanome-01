using UnityEngine;

public class ActionDisplay : MonoBehaviour {
    void OnEnable() {
        EventManager.ActionUpdate += UpdateAction; 
        EventManager.CurrentPlayerUpdate += UpdateCurrentPlayer;   
    }

    void OnDisable() {
        EventManager.ActionUpdate -= UpdateAction;
        EventManager.CurrentPlayerUpdate -= UpdateCurrentPlayer; 
    }

    public void UpdateAction(int actionID) {
        Action action = Action.FromValue<Action>(actionID);
        transform.Find("Action").GetComponent<UnityEngine.UI.Text>().text = action.Name;
    }

    public void UpdateCurrentPlayer(Hero hero) {
        transform.Find("Hero").GetComponent<UnityEngine.UI.Text>().text = hero.Type;
    }
}