using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTags
{
    public const string WALK_PARAM = "Walk";
    public const string RUN_PARAM = "Run";
    public const string ATTACK_PARAM = "Attack";
    public const string DEAD_PARAM = "Dead";
    public const string SHOOT_PARAM = "Shoot";
    public const string DEAD_TRIGGER = "Dead";
}

public class EnemyAnimation : MonoBehaviour
{
    private Animator Anima;
    
    void Awake()
    {
        Anima = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        Anima.SetBool(EnemyAnimationTags.WALK_PARAM, walk);
    }

    public void Run(bool run)
    {
        Anima.SetBool(EnemyAnimationTags.WALK_PARAM, run);
    }

    public void Attack()
    {
        Anima.SetTrigger(EnemyAnimationTags.ATTACK_PARAM);
    }

    public void Dead()
    {
        Anima.SetTrigger(EnemyAnimationTags.DEAD_TRIGGER);
    }

    public void playShootSound()
    {
        // Only here to suppress missing animator error.
    }
}