using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token {
    //protected List<GameObject> tokens = new List<GameObject>();
    //protected List<int> cellsID = new List<int>();
    GameObject token;
    public string id;
    public string effect;
    public string description;

    public Token(string id, Color color) {
        this.id = id;
        description = "does it work";
        token = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        token.transform.localScale = new Vector3(2, 0.1f, 2);
        token.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        var tokenRenderer = token.GetComponent<Renderer>();
        tokenRenderer.material.SetColor("_Color", color);
        tokenRenderer.material.shader = Shader.Find("UI/Default");
    }

    public Token(string id, GameObject token) {
        this.id = id;
        this.token = token;
    }

    public void addToCell(int cellID) {
        Cell cell = Cell.FromId(cellID);
        //cellsID.Add(cellID);

        token.transform.position = cell.gameObject.transform.position;
        token.transform.parent = cell.gameObject.transform;
        //token.name = "Well" + cellID;
    }
}
