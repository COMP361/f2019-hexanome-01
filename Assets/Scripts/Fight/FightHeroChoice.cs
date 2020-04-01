using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHeroChoice : MonoBehaviour
{
    //public GameObject currentHeroImage;
    // Start is called before the first frame update
    private void Awake()
    {
        var heroes = GameManager.instance.heroes;
        foreach(Hero h in heroes)
        {

        }
        //currentHeroImage.GetComponent<SpriteRenderer>().sprite = GameManager.instance.CurrentPlayer.getSprite(); 

    }
}
