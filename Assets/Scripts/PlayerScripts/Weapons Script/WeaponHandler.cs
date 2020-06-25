using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem muzzleFlash;

    [SerializeField]
    private AudioSource shoot_sound, reload_sound;

    private Animator animator;
    public WeaponAim weapon_aim;
    public WeaponFireType fire_type;
    public WeaponBulletType bullet_type;
    public GameObject attack_point;

    // These are here to supress warnings
    private void PlaySound()      { }
    private void SpawnObject()    { }
    private void PlayWooshSound() { }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool can_aim)
    {
        animator.SetBool(AnimationTags.AIM_PARAMETER, can_aim);
    }

    void turnOnMuzzleFlash()
    {
        muzzleFlash.gameObject.SetActive(true);
    }

    void turnOffMuzzleFlash()
    {
        muzzleFlash.gameObject.SetActive(false);
    }

    public void playMuzzleFlash()
    {
        turnOnMuzzleFlash();
        muzzleFlash.Play();
    }

    void playShootSound()
    {
        shoot_sound.Play();
    }

    void playReloadSound()
    {
        reload_sound.Play();
    }

    void turnOnAttackPoint()
    {
        attack_point.SetActive(true);
    }

    void turnOffAttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }
}
