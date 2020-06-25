using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    private Rigidbody my_body;
    public float speed = 30f;
    public float deactivate_timer = 3f;
    public float damage = 15f;

    void Awake()
    {
        my_body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_timer);
    }

    public void Launch(Camera my_camera)
    {
        my_body.velocity = my_camera.transform.forward * speed;
        transform.LookAt(transform.position + my_body.velocity);    
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            // gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        //After we hit enemy delete game object
    }
}
