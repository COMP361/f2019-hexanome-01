using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject cell;
    private bool isDone;
    private int moveSpeed;
    private Vector3 position;
    private Transform token;
    private string type;

    void Awake() {
      type = transform.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        token = transform.Find("Token");
        moveSpeed = 5;
        Transform slot = cell.transform.FindDeepChild("slot1");
        token.position = slot.position;
        position = token.position;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
      UpdatePosition();
    }

    public void UpdatePosition()
    {
      if(token.position == position) return;
      token.position = Vector2.MoveTowards(token.position, position, moveSpeed * Time.deltaTime);
    }

    public void Move(GameObject c)
    {
      // get slot
      Transform slot = c.transform.FindDeepChild("slot1");
      if(Position == slot.position) return;

      cell = c;
      Position = slot.position;
      IsDone = true;
    }

    public bool IsDone{
      get {
        return isDone;
      }
      set {
        isDone = value;
      }
    }

    public Vector3 Position {
      get {
        return position;
      }
      set {
        position = value;
      }
    }

    public string Type {
      get {
        return type;
      }
    }
}
