using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : Subject
{
    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.tag == "Player")
        {
            print(GameManager.Instance.GetBuildIndex());
            if (GameManager.Instance.GetBuildIndex() == 2)
            {
                Notify(NotificationType.LEVEL_2_BEGIN_COMPLETED);
            }            
        }
    }
}
