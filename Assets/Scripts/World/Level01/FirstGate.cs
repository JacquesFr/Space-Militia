using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGate : MonoBehaviour
{
    public GameObject hinge1;

    public GameObject hinge2;

    public GameObject trigger;

    private GameObject chicken_coup;

    private bool complete = false;

    Animator left_door;
    Animator right_door;

    void Start()
    {
        left_door = hinge1.GetComponent<Animator>();
        right_door = hinge2.GetComponent<Animator>();
        chicken_coup = GameObject.FindGameObjectWithTag("Chicken Coup");
    }

    void Update()
    {
        complete = chicken_coup.GetComponent<ChickenCoup>().get_Checkpoint_1_Status();
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete)
        {
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
        left_door.SetBool("open", state);
        right_door.SetBool("open", state);
    }

    public bool GetFirstCheckpoint()
    {
        return complete;
    }
}
