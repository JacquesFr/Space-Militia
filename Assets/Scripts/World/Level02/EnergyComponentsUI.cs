using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyComponentsUI : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Notify(NotificationType.ENERGY_COMPONENTS);

        }
    }
}
