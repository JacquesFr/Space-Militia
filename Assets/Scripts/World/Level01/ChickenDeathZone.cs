using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeathZone : MonoBehaviour
{
    [SerializeField] private GameObject respawnLocation = null;

    private void OnTriggerEnter(Collider chicken)
    {
        if (chicken.gameObject.tag.Contains("Chicken"))
        {
            chicken.transform.position = respawnLocation.transform.position;
            chicken.transform.rotation = Quaternion.Euler(new Vector3(0f, chicken.transform.rotation.y, 0f));
        }
    }
}