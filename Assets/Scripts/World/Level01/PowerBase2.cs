using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBase2 : Subject
{
    private GameObject secondPowerCell = null;
    private bool secondPowerCellFound = false;
   
    private void Start()
    {
        secondPowerCell = GameObject.FindGameObjectWithTag("PowerCell2");
    }

    private void OnTriggerEnter(Collider PowerStation)
    {
        if(PowerStation.gameObject == secondPowerCell && secondPowerCellFound == false)
        {
            secondPowerCellFound = true;
            Notify(NotificationType.POWERBASE_2_DONE);
        }
    }
}