using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Subject
{
    private bool canBePressed = false;

    private void Start()
    {
        GateController gate = GameObject.FindObjectOfType<GateController>();
        AddObserver(gate);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canBePressed == true)
        {
            Notify(NotificationType.BUTTON_PRESSED);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBePressed = false;
        }
    }
}