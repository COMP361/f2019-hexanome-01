using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBoard : MonoBehaviour
{
    Text farmerCount;

    void OnEnable() {
        EventManager.FarmersInventoriesUpdate += UpdateFarmerCount;
    }

    void OnDisable() {
        EventManager.FarmersInventoriesUpdate -= UpdateFarmerCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        farmerCount = transform.Find("Farmer/Count").GetComponent<UnityEngine.UI.Text>();
    }

    void UpdateFarmerCount(int attachedFarmers, int noTargetFarmers, int detachedFarmers) {
        farmerCount.text = attachedFarmers.ToString();
    }
}
