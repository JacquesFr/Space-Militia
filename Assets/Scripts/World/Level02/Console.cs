using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : Subject
{
    public GameObject trigger;

    private GameObject console;

    private bool canUse = false;
    private bool _consoleActivated = false;
   

    // Start is called before the first frame update
    void Start()
    {
        console = GameObject.FindGameObjectWithTag("Console_1");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canUse == true)
        {
            print("ALIEN Console of DEATH Activated");
            _consoleActivated = true;
            //Notify(NotificationType.Console_Done);
        }
    }

    private void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag == "Player")
        {
            Notify(NotificationType.TERMINAL_TEXT);
        }
        
        if (Player.gameObject.tag == "Player" && _consoleActivated == false)
        {
            print("Close enough to use terminal");
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

    public bool get_Console_Status()
    {
        return _consoleActivated; 
    }
    

}
