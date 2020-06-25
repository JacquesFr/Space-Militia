using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor = null;
    [SerializeField] private GameObject rightDoor = null;

    private Animator leftAnim = null;
    private Animator rightAnim = null;

    private void Start()
    {
        leftAnim = leftDoor.GetComponent<Animator>();
        rightAnim = rightDoor.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            OpenDoor(false);
        }
    }

    private void OpenDoor(bool state)
    {
        leftAnim.SetBool("open", state);
        rightAnim.SetBool("open", state);
    }
}
