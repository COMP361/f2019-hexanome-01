using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int moveSpeed;
    public int will;
    public int strength;
    public int reward;
    //int dices;
    Cell rank;
    GameManager gm;
    GameObject token;

    void Awake() {
        moveSpeed = 5;

        token = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        token.transform.localScale = new Vector3(2, 0.1f, 2);
        token.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        var tokenRenderer = token.GetComponent<Renderer>();
        tokenRenderer.material.SetColor("_Color", Color.black);
        tokenRenderer.material.shader = Shader.Find("UI/Default");

        token.transform.parent = gameObject.transform;
        token.name = "Token";
    }

    void Start() {
        gm = GameManager.instance;
    }

    public void SetRank(int cellID) {
        rank = Cell.FromId(cellID);
        token.transform.position = rank.Waypoint;
    }
}