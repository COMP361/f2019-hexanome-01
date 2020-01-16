using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Farmer : MonoBehaviour
{
  protected int moveSpeed;
  protected Cell rank;
  protected GameObject token;
  protected Color color = new Color(0, 0, 0, 1);


  public void Awake() {
    moveSpeed = 5;

    token = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    token.transform.localScale = new Vector3(2, 0.1f, 2);
    token.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

    var tokenRenderer = token.GetComponent<Renderer>();
    tokenRenderer.material.SetColor("_Color", Color.white);
    tokenRenderer.material.shader = Shader.Find("UI/Default");

    token.transform.parent = gameObject.transform;
    token.name = "Token";
  }

  void Update() {
    Move();
  }

  public void Move() {
  }

  public void SetRank(int cellID) {
    rank = Cell.FromId(cellID);
    token.transform.position = rank.Waypoint;
  }
}