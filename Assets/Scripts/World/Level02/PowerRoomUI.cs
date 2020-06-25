using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRoomUI : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Notify(NotificationType.LEVEL_2_GO_TO_POWER_ROOM);
        
        }
    }
}
