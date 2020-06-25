using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBase1 : Subject
{
    private bool isBasePowered = false;
    private bool canUse = false;
    private bool terminalActivated = false;
    private PowerBase1 powerBase = null;

    private void Start()
    {
        powerBase = GameObject.FindObjectOfType<PowerBase1>();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E) && canUse == true)
        {
            terminalActivated = true;
            Notify(NotificationType.TERMINAL_1_DONE);
        }
    }

    public void OnNotify(NotificationType type)
    {
        if (type == NotificationType.POWERBASE_1_DONE)
        {
            isBasePowered = true;
        }
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player" && terminalActivated == false)
        {
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
        }
    }
}

