using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { PATROL, FOLLOW, ATTACK }

public class EnemyController : Subject
{
    [SerializeField] private float enemy_damage = 10.0f;
    [SerializeField] private GameObject plasma_bullet = null;
    [SerializeField] private GameObject attack_point = null;
    [SerializeField] private Transform plasma_bullet_start_position = null;
    [SerializeField] private AudioSource shoot_sound;

    private EnemyAnimation enemy_Anima = null;
    private NavMeshAgent navAgent = null;
    private EnemyState enemy_State = EnemyState.PATROL;
    private Transform target = null;
    private GameObject gun = null;

    private float walk_speed = 0.5f;
    private float run_speed = 2.0f;
    private float chase_distance = 6.0f;
    private float current_chase_dist = 0.0f;
    private float attack_distance = 3.0f;
    private float follow_after_attack_dist = 5.0f;
    private float patrol_radius_min = 2.0f;
    private float patrol_radius_max = 20.0f;
    private float patrol_time_limit = 5.0f;
    private float patrol_timer = 0.0f;
    private float attack_timer = 0.0f;
    private float wait_before_attack = 2.0f;

    private float health = 100.0f;

    private Rigidbody bullet;
    private float bullet_speed = 100f;
    private float deactivate_timer = 3f;

    public EnemyState Enemy_State
    {
        get; set;
    }

    #region [MONOBEHAVIORS]
    private void Awake()
    {
        enemy_Anima = GetComponent<EnemyAnimation>();
        navAgent = GetComponent<NavMeshAgent>();
        gun = GameObject.FindWithTag("AlienGun");
        target = GameObject.FindObjectOfType<PlayerController>().transform;
        bullet = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        enemy_State = EnemyState.PATROL;
        patrol_timer = patrol_time_limit;
        attack_timer = wait_before_attack;
        current_chase_dist = chase_distance;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + attack_distance,
                                                       transform.position.y + attack_distance,
                                                       transform.position.z + attack_distance), Color.red);

        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.FOLLOW)
        {
            Follow();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            this.transform.LookAt(target);
            Attack();
        }
    }
    #endregion

    #region [HEALTH FUNCTIONS]
    public void ApplyDamage(float damage)
    {
        health = health - damage;
        
        if(health <= 0.0f)
        {
            Notify(NotificationType.ENEMY_DEAD);
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Anima.Dead();
            StartCoroutine(CleanUp(3.0f));
        }

        if (enemy_State == EnemyState.PATROL)
        {
            chase_distance = 45f;
        }
    }
    #endregion
    
    #region [ENEMY BEHAVIOR FUNCTIONS]
    private void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        patrol_timer += Time.deltaTime;

        if (patrol_timer > patrol_time_limit)
        {
            NewRandomDestination();
            patrol_timer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anima.Walk(true);
        }
        else
        {
            enemy_Anima.Walk(false);
        }

        //when player gets close to enemy go to follow
        if (Vector3.Distance(transform.position, target.position) <= chase_distance)
        {
            enemy_Anima.Walk(false);
            enemy_State = EnemyState.ATTACK;

            //we can play audio for enemy here if you want to play something
            //when enemy finds the player
        }
    }

    private void Follow()
    {
        navAgent.isStopped = false;
        navAgent.speed = run_speed;

        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anima.Run(true);
        }
        else
        {
            enemy_Anima.Run(false);
        }

        //when enemy close enough to player then go to attack state
        // if not then 
        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {
            enemy_Anima.Walk(false);
            enemy_Anima.Run(false);
            enemy_State = EnemyState.ATTACK;

            if (chase_distance != current_chase_dist)
            {
                chase_distance = current_chase_dist;
            }

            //we can play audio for enemy here if you want to play something
            //when enemy attacks the player
        }
        else if (Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            enemy_Anima.Run(false);
            enemy_State = EnemyState.PATROL;
            patrol_timer = patrol_time_limit;

            if (chase_distance != current_chase_dist)
            {
                chase_distance = current_chase_dist;
            }
        }
    }

    private void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_timer += Time.deltaTime;

        //attack once for the enemy
        if (attack_timer > wait_before_attack)
        {
            enemy_Anima.Attack();

            RaycastHit hit;
            Vector3 direction = target.position - this.transform.position;

            if (Physics.Raycast(plasma_bullet_start_position.position, direction, out hit))
            {
                GameObject plasma = GameObject.Instantiate(plasma_bullet, plasma_bullet_start_position.position, transform.rotation);
                plasma.GetComponent<Rigidbody>().AddForce(direction * bullet_speed);

                if (hit.collider.gameObject.GetComponent<PlayerController>() != null)
                {
                    print("GOTEM");
                    hit.collider.gameObject.GetComponent<PlayerController>().ApplyDamage(enemy_damage);
                }
            }

            attack_timer = 0.0f;
            shoot_sound.Play();
        }

        if (Vector3.Distance(transform.position, target.position) > attack_distance + follow_after_attack_dist)
        {
            enemy_State = EnemyState.FOLLOW;
        }
    }
    #endregion

    #region [HELPER FUNCTIONS]
    private void NewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_radius_min, patrol_radius_max);

        Vector3 rand_direction = Random.insideUnitSphere * rand_Radius;
        rand_direction += transform.position;

        NavMeshHit navInside;

        NavMesh.SamplePosition(rand_direction, out navInside, rand_Radius, -1);

        navAgent.SetDestination(navInside.position);
    }

    private IEnumerator CleanUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject.Destroy(this.gameObject);
    }

    private void TurnOnAttackPoint()
    {
        attack_point.SetActive(true);
    }

    private void TurnOffAttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }
    #endregion
}