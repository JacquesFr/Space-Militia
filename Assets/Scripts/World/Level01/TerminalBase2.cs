using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBase2 : Subject
{
    private bool isBasePowered = false;
    private bool canUse = false;
    private bool terminalActivated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUse == true)
        {
            print("terminal 2 activated");
            terminalActivated = true;
            Notify(NotificationType.TERMINAL_2_DONE);
        }
    }

    public void OnNotify(NotificationType type)
    {
        if (type == NotificationType.POWERBASE_2_DONE)
        {
            isBasePowered = true;
        }
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player"&& terminalActivated == false)
        {
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Close enough to use terminal");
            canUse = false;
        }
    }
}
