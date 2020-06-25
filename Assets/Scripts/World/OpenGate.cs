using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public GameObject hinge1;
    public GameObject hinge2;
    public GameObject trigger;
    public GameObject player;

    private bool complete_Kills;

    Animator left_door;
    Animator right_door;

    public void SetCompleteKills(bool _complete_Kills)
    {
        complete_Kills = _complete_Kills;
    }

    private void Start()
    {
        left_door = hinge1.GetComponent<Animator>();
        right_door = hinge2.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete_Kills)
        {
            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete_Kills)
        {
            OpenDoor(false);
        }
    }

    private void OpenDoor(bool state)
    {
        left_door.SetBool("open", state);
        right_door.SetBool("open", state);
    }
}