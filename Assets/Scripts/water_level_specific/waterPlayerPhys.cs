using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlayerPhys : MonoBehaviour
{
    private float gravMultiplier = .2f;
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Environment")
        {
            if (Input.GetKey("space"))
            {
                GetComponent<CharacterController>().Move(-Physics.gravity * .0135f);
            }
            else if (Input.GetKey("left ctrl"))
            {
                GetComponent<CharacterController>().Move(Physics.gravity * .01675f);
            }

            GetComponent<CharacterController>().Move(-Physics.gravity * .025f);
        }
    }
}
