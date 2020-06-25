using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStretch : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Notify(NotificationType.FINAL_STRETCH);

        }
    }
}
