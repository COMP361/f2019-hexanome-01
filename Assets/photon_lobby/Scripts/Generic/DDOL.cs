using Photon.Pun;
using UnityEngine;

public class DDOL : MonoBehaviourPunCallbacks
{

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
