using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI {

    GameObject ui;
    Transform options;
    GameManager gm;

    Button moveBtn, fightBtn, skipBtn;

    Button cancelBtn, confirmBtn;

    public UI() {
        gm = GameManager.instance;
        ui = GameObject.Find("UI");

        moveBtn = ui.transform.Find("Move Button").GetComponent<Button>();
        moveBtn.onClick.AddListener(delegate { gm.SelectAction(Action.Move); });

        fightBtn = ui.transform.Find("Fight Button").GetComponent<Button>();
        fightBtn.onClick.AddListener(delegate { gm.SelectAction(Action.Fight); });

        skipBtn = ui.transform.Find("Skip Button").GetComponent<Button>();
        skipBtn.onClick.AddListener(delegate { gm.SelectAction(Action.Skip); });

        cancelBtn = ui.transform.Find("Move Options/Cancel Button").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate { gm.CancelAction(); });

        confirmBtn = ui.transform.Find("Move Options/Confirm Button").GetComponent<Button>();
        confirmBtn.onClick.AddListener(delegate { gm.ConfirmAction(); });
    }
    
    public void ShowPath(Cell[] path) {
        if(path != null && path.Length > 0) {
            for(int i = 0; i < path.Length - 1; i++) {
                DrawLine(path[i].Waypoint, path[i + 1].Waypoint, Color.red);
            }
        }
    }

    public void HidePath() {
        
    }

    public void ShowOptions(Action action) {
        options = ui.transform.Find(action.ToString() + " Options");
        if(options != null) options.gameObject.SetActive(true);
    }

    public void HideOptions() {
        if(options != null) {
            options.gameObject.SetActive(false);
            options = null;
        }
    }

    public void UpdatePlayerInfo() {
        ui.transform.FindDeepChild("Hero").GetComponent<UnityEngine.UI.Text>().text = gm.CurrentPlayer.Type;
        ui.transform.FindDeepChild("Action").GetComponent<UnityEngine.UI.Text>().text = gm.CurrentPlayer.State.action.Name;
    }

    static public GameObject DrawLine(Vector3 start, Vector3 end, Color color, float width = 0.1f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = color;
        lr.endColor = color;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        return myLine;
    }

    void Popup() {}
}