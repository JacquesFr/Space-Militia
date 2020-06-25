using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_2 : MonoBehaviour
{
    private GameObject chicken;
    private bool platform2Done = false; 
    private GameObject engine;


    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindGameObjectWithTag("Chicken_lvl2");
        engine = GameObject.FindGameObjectWithTag("engine");
    }
    
    // Update is called once per frame
    private void OnTriggerEnter(Collider platform)
    {
        if( (platform.gameObject.tag == "engine") || (platform.gameObject.tag == "Chicken_lvl2")){       
            platform2Done = true;
            print("Platform done");
        }
    }

     public bool getObjectStatus()
    {
        return platform2Done;
    }
}
