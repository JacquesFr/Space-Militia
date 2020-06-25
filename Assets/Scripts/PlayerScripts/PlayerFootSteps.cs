using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_sound;

    [SerializeField]
    private AudioClip[] footstep_clip;

    private CharacterController character_controller;

    [HideInInspector]
    public float volume_min, volume_max;

    private float accumulated_distance;

    [HideInInspector]
    public float step_distance;

    private PlayerFootSteps player_footstep;
    private float sprint_volume = 0.3f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.01f;
    private float walk_volume_max = 0.2f;

    void Awake()
    {
        footstep_sound = GetComponent<AudioSource>();
        character_controller = GetComponentInParent<CharacterController>();
        
    }


    void Update()
    {
        checkToPlayFootstepSound();
    }

    void checkToPlayFootstepSound()
    {
        if (!character_controller.isGrounded)
        {
            return; //
        }

        if(character_controller.velocity.sqrMagnitude > 0)
        {
            accumulated_distance += Time.deltaTime;

            if(accumulated_distance > step_distance)
            {
                footstep_sound.volume = Random.Range(volume_min, volume_max);
                footstep_sound.clip = footstep_clip[Random.Range(0, footstep_clip.Length)];
                footstep_sound.Play();

                accumulated_distance = 0f;
   
            }
        }

        else
        {
            accumulated_distance = 0f;
            footstep_sound.Stop();
        }
    }
}
