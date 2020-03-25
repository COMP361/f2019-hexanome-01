using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerFightPlayer : MonoBehaviour
{
    public Text hero1_name;
    public Text hero1_strength;
    public Text hero1_wp;
    public GameObject hero1_sprite;
    public regularDices[] hero1_dice = new regularDices[4];

    public Text hero2_name;
    public Text hero2_strength;
    public Text hero2_wp;
    public GameObject hero2_sprite;
    public regularDices[] hero2_dice = new regularDices[4];

    public Text hero3_name;
    public Text hero3_strength;
    public Text hero3_wp;
    public GameObject hero3_sprite;
    public regularDices[] hero3_dice = new regularDices[4];

    public Text hero4_name;
    public Text hero4_strength;
    public Text hero4_wp;
    public GameObject hero4_sprite;
    public regularDices[] hero4_dice = new regularDices[4];

    private void Start()
    {
        
    }

}
