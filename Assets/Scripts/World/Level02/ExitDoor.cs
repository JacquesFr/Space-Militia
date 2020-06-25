using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Subject
{
    public GameObject trigger;
    public GameObject topdoor;
    public GameObject bottomdoor;

    Animator bottom_door;
    Animator top_door;

    private bool isPuzzle2Complete = false;
    private bool isObject1Found = false;
    private bool isObject2Found = false;
    private bool displayDone = false;

    private GameObject platform1;
    private GameObject platform2;

    // Start is called before the first frame update
    void Start()
    {
         top_door = topdoor.GetComponent<Animator>();
         bottom_door = bottomdoor.GetComponent<Animator>();
         platform1 = GameObject.FindGameObjectWithTag("level2_platform1");
         platform2 = GameObject.FindGameObjectWithTag("level2_platform2");
   
    }

    // Update is called once per frame
    void Update()
    {
        isObject1Found = platform1.GetComponent<Platform_1>().getObjectStatus();
        isObject2Found = platform2.GetComponent<Platform_2>().getObjectStatus();
        GetPuzzle2Status();

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && isPuzzle2Complete)
        {
            print("Open Door");
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && isPuzzle2Complete)
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        bottom_door.SetBool("open", state);
        top_door.SetBool("open", state);
    }

    public bool GetPuzzle2Status()
    {
        if(isObject1Found == true && isObject2Found == true){
            isPuzzle2Complete = true;
            DisplayDialogue();
        }

        return isPuzzle2Complete;
    }

    void DisplayDialogue()
    {
        if (!displayDone)
        {
            Notify(NotificationType.POWER_RESTORED);
            displayDone = true;
        }

    }
}
