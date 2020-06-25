using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCompactorUI : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Notify(NotificationType.TRASH_COMPACTOR_UI);

        }
    }
}
