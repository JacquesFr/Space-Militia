using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeUntilDeath = 6.0f;
    private float deathTimer = 0.0f;

    private void Awake()
    {
        deathTimer = 0;
    }

    private void Update()
    {
        deathTimer += deathTimer * Time.deltaTime;

        if(deathTimer >= timeUntilDeath)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Die();
    }
}