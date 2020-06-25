using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCoup : Subject
{
    private GameObject chicken_1;
    private GameObject chicken_2;
    private GameObject chicken_3;
    private GameObject chicken_4;
    private GameObject chicken_5;

    private bool chicken_1_Found = false;
    private bool chicken_2_Found = false;
    private bool chicken_3_Found = false;
    private bool chicken_4_Found = false;
    private bool chicken_5_Found = false;
    
    private bool checkpoint_complete = false; //is the puzzle solved? 
    
    private void Start()
    {
        chicken_1 = GameObject.FindGameObjectWithTag("Chicken01");
        chicken_2 = GameObject.FindGameObjectWithTag("Chicken02");
        chicken_3 = GameObject.FindGameObjectWithTag("Chicken03");
        chicken_4 = GameObject.FindGameObjectWithTag("Chicken04");
        chicken_5 = GameObject.FindGameObjectWithTag("Chicken05");
    }

    private void Update()
    {
        // Dev shortcut
        if (Input.GetKeyDown(KeyCode.Z))
        {
            chicken_1_Found = chicken_2_Found = chicken_3_Found = chicken_4_Found = chicken_5_Found = true;
            check_Chickens();
        }
    }

    private void OnTriggerEnter(Collider chicken)
    {
        if (chicken.gameObject == chicken_1)
        {
            chicken_1_Found = true;
            check_Chickens();
        }
        if (chicken.gameObject == chicken_2)
        {
            chicken_2_Found = true;
            check_Chickens();
        }
        if (chicken.gameObject == chicken_3)
        {
            chicken_3_Found = true;
            check_Chickens();
        }
        if (chicken.gameObject == chicken_4)
        {
            chicken_4_Found = true;
            check_Chickens();
        }
        if (chicken.gameObject == chicken_5)
        {
            chicken_5_Found = true;
            check_Chickens();
        }
    }

    private void check_Chickens()
    {
        if (chicken_1_Found && chicken_2_Found && chicken_3_Found && chicken_4_Found && chicken_5_Found)
        {
            Notify(NotificationType.FIRST_CHECKPOINT_DONE);
            checkpoint_complete = true;
        }
    }

    public bool get_Checkpoint_1_Status()
    {
        return checkpoint_complete;
    }
}
