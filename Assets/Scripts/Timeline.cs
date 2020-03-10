using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * decrease willpoints if on 8, 9, 10
 * endDay if on 8, 9, 10
 */
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

    void OnEnable()
    {
        EventManager.TimelineUpdate += UpdateTimeOfDay;

    }

    void OnDisable()
    {
        EventManager.TimelineUpdate -= UpdateTimeOfDay;

    }


    void UpdateTimeOfDay(Hero hero, MapPath path)
    {
        int freeHours = hero.State.TimeOfDay.GetFreeHours();
        int extendedHours = hero.State.TimeOfDay.GetExtendedHours();
        int pathLength = path.Cells.Count - 1;

        //Debug.Log("UpdateTimeOfDay: pathLength: " + pathLength);

        if (pathLength != 0 && pathLength <= (freeHours + extendedHours))
        {
            // if path reaches 8, 9 10 decreae willpoints
            if (pathLength > freeHours) { hero.State.decrementWP(2); }
            hero.State.TimeOfDay.update(pathLength);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        foreach (Transform child in transform)
        {
            Gizmos.DrawSphere(child.Find("Mage").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Warrior").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Dwarf").transform.position, 3f);
            Gizmos.DrawSphere(child.Find("Archer").transform.position, 3f);
        }
    }


}
