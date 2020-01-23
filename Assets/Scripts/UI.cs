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

    Button setDestBtn;

    GameObject pathContainer;
    List<GameObject> pathLines;

    public UI() {
        gm = GameManager.instance;
        ui = GameObject.Find("UI");

        pathLines = new List<GameObject>();
        pathContainer = GameObject.Find("Paths");

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

        setDestBtn = ui.transform.Find("Move Options/Set Destination Button").GetComponent<Button>();
        setDestBtn.onClick.AddListener(delegate { gm.SelectAction(Action.Move.SetDest); });
    }
    
    public void DisplayPath(List<Cell> path) {
        HidePath();
        if(path != null && path.Count > 0) {
            for(int i = 0; i < path.Count - 1; i++) {
                pathLines.Add(DrawLine(path[i].Waypoint, path[i + 1].Waypoint, Color.red));
            }
        }
    }

    public void HidePath() {
        for(int i = 0; i < pathLines.Count; i++) {
            GameObject.Destroy(pathLines[i]);
        }
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

    public GameObject DrawLine(Vector3 start, Vector3 end, Color color, float width = 0.1f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.transform.parent = pathContainer.transform;
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