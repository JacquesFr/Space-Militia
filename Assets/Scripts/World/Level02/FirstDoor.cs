using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : Subject
{
    public GameObject trigger;
    public GameObject topdoor;
    public GameObject bottomdoor;

    private bool complete = false;
    private bool checkpoint_2 = false;
    private GameObject console;
    private GameObject checkpoint2;

    Animator bottom_door;
    Animator top_door;

    // Start is called before the first frame update
    void Start()
    {
        top_door = topdoor.GetComponent<Animator>();
        bottom_door = bottomdoor.GetComponent<Animator>();
        console = GameObject.FindGameObjectWithTag("Console_1");
        checkpoint2 = GameObject.FindGameObjectWithTag("Checkpoint_2");
    }

    // Update is called once per frame
    void Update()
    {
        complete = console.GetComponent<Console>().get_Console_Status();
        checkpoint_2 = checkpoint2.GetComponent<ExitDoor>().GetPuzzle2Status();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete)
        {
            print("Open Door");
            OpenDoor(true);

        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete)
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        bottom_door.SetBool("open", state);
        top_door.SetBool("open", state);
    }

    public bool GetConsoleStatus()
    {
        return complete;
    }
}
