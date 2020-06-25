using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowPassFilter : MonoBehaviour
{
    public GameObject water;

    private Color normalColor;
    private Color underwaterColor;

    public AudioClip drowningClip;
    private AudioSource underWaterAudioSource;

    void Awake()
    {

        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.55f, 0.55f, 0.75f, 0.5f);
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.13f;

        underWaterAudioSource = new AudioSource();
        underWaterAudioSource.clip = drowningClip;
        underWaterAudioSource.loop = true;
        underWaterAudioSource.volume = 1f;
        underWaterAudioSource.maxDistance = 50;
        underWaterAudioSource.playOnAwake = true;

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject == water)
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 150;
            GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 3;

            
            RenderSettings.fog = true;

            water.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject == water)
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            gameObject.GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 1;

            RenderSettings.fog = false;

            water.GetComponent<AudioSource>().Stop();
        }

        }

}
