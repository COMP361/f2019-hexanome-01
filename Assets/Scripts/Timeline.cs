using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;

        foreach (Transform child in transform) {
            Gizmos.DrawSphere(child.Find("Mage").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Warrior").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Dwarf").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Archer").transform.position, 3f);
        }
    }
}
