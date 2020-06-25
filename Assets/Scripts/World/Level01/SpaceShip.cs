using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : Subject
{
    [SerializeField]
    private Transform CameraPosition;

    [SerializeField]
    private GameObject Weapons;

    Animator TakeOff;

    private bool terminal1_complete = false;
    private bool terminal2_complete = false;
    private bool player_entered = false;
    private bool level_complete = false;
    private float timeLeft = 8f;

    private void Start()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Weapons = GameObject.FindGameObjectWithTag("WeaponView");
        TakeOff = this.GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (level_complete)
        {
            LevelComplete();
        }
        
    }

    public void OnNotify(NotificationType type)
    {
        switch(type)
        {
            case NotificationType.TERMINAL_1_DONE:
                terminal1_complete = true;
                break;

            case NotificationType.TERMINAL_2_DONE:
                terminal2_complete = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            player_entered = true;
            takeOff(false);
            CheckTakeOff();
        }
    }

    private void CheckTakeOff()
    {
        if(player_entered == true && terminal1_complete == true && terminal2_complete == true)
        {
            print("Player Entered");

            Weapons.SetActive(false);
            takeOff(true);
            level_complete = true;
        }
    }

    void takeOff(bool state)
    {
        Notify(NotificationType.LEVEL_1_TEXT);
        TakeOff.SetBool("Take Off", state);
    }

    void LevelComplete()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            Notify(NotificationType.LEVEL1_COMPLETE);
        }


    }

}