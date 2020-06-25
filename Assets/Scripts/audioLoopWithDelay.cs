using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLoopWithDelay : MonoBehaviour
{

    public float delayTime;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SoundLoop());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SoundLoop()
    {
        while (true)
        { 
            audio.Play();
            yield return new WaitForSeconds(delayTime);
        }
    }
}
