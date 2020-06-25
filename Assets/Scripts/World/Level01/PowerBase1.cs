using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBase1 : Subject
{
    private GameObject firstPowerCell = null;
    private bool firstPowerCellFound = false;
   
    private void Start()
    {
        firstPowerCell = GameObject.FindGameObjectWithTag("PowerCell1");
    }

    private void OnTriggerEnter(Collider PowerStation)
    {
        if (PowerStation.gameObject == firstPowerCell && firstPowerCellFound == false)
        {
            firstPowerCellFound = true;
            Notify(NotificationType.POWERBASE_1_DONE);
        }
    }
}