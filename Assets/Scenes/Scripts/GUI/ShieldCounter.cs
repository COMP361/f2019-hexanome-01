using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShieldCounter : MonoBehaviour
{
    Text shieldCount;

    void OnEnable() {
        EventManager.ShieldsUpdate += UpdateShieldCount;
    }

    void OnDisable() {
        EventManager.ShieldsUpdate -= UpdateShieldCount;
    }

    // Start is called before the first frame update
    void Awake() {
        shieldCount = transform.Find("Count").GetComponent<Text>();
    }

    void UpdateShieldCount(int shields) {
        shieldCount.text = shields.ToString();
    }
}
