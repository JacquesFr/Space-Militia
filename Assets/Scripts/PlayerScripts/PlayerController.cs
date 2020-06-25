using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    // Movement-related members
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float jump_force = 5.0f;
    [SerializeField] private float vertical_velocity = 0.0f;
    [SerializeField] private float sprint_speed = 10.0f;
    [SerializeField] private float move_speed = 5.0f;
    [SerializeField] private float crouch_speed = 2.0f;
    [SerializeField] private float sprint_value = 100.0f;
    [SerializeField] private float sprint_threshold = 5.0f;

    private Transform look_root;
    private float stand_height = 1.6f;
    private float crouch_height = 1.0f;
    private bool is_crouching = false;

    private bool jumped = false; //added to make water work
    private CharacterController character_controller;
    private Vector3 move_direction;

    // Audio-related members
    private PlayerFootSteps player_footstep;
    private float sprint_volume = 1.0f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.2f;
    private float walk_volume_max = 0.6f;
    private float walk_step_distance = 0.4f;
    private float sprint_step_distance = 0.25f;
    private float crouch_step_distance = 0.5f; //larger value because you step less often

    // Attack-related members
    [SerializeField] private GameObject arrow_prefab = null;
    [SerializeField] private GameObject spear_prefab = null;
    [SerializeField] private Transform arrow_spear_start_position = null;
    [SerializeField] private float damage = 5.0f;
    [SerializeField] private float fire_rate = 15.0f;
    private float next_time_to_fire = 10.0f;
    private Animator zoomCameraAnimator = null;
    private bool zoomed = false;
    [SerializeField] private Camera main_camera = null;
    private GameObject crosshair = null;
    private bool is_aiming = false;

    // Health-related members
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float healthRegenerationRate = 1.0f;

    //Carrying-related members
    [SerializeField] private Transform pickupDestination = null;
    private bool canCarry = false;
    private bool isCarrying = false;
    private bool isColliding = false;
    private bool disarmed = false;
    private float throwStrength = 10.0f;

    // Weapon Handling
    [SerializeField] private WeaponHandler[] weapons = null;
    private static bool reset = false;
    private int current_weapon_index = 0;

    #region [MONOBEHAVIORS]
    private void Awake()
    {
        character_controller = GetComponent<CharacterController>();
        player_footstep = GetComponentInChildren<PlayerFootSteps>();
        look_root = transform.GetChild(0);
        
        zoomCameraAnimator = transform.Find(Tags.LOOK_VIEW).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        current_weapon_index = 0;
        weapons[current_weapon_index].gameObject.SetActive(true);

        disarmed = false;
    }
    
    private void Start()
    {
        player_footstep.volume_min = walk_volume_min;
        player_footstep.volume_max = walk_volume_max;
        player_footstep.step_distance = walk_step_distance;
    }
    
    private void Update()
    {
        Move();
        Sprint();
        Crouch();
        ShootWeapon();
        Zoom();
        PickUp();

        #region [Health Stuff]
        if (health <= 0f)
        {
            Notify(NotificationType.PLAYER_DEAD);
        }
        else 
        {
            if(health != 100f && health > 0)
            {
                health += (healthRegenerationRate / 2) * Time.deltaTime;
                if (health >= 100f)
                {
                    health = 100f;
                }
            }
        }
        #endregion

        #region [Weapon Select Stuff]
        if (!disarmed)
        {
            if (reset)
            {
                weapons[current_weapon_index].gameObject.SetActive(true);
                reset = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnOnSelectedWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TurnOnSelectedWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TurnOnSelectedWeapon(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TurnOnSelectedWeapon(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                TurnOnSelectedWeapon(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                TurnOnSelectedWeapon(5);
            }
        }
        else
        {
            DisarmPlayer();
        }
        #endregion
    }
    #endregion

    #region [HEALTH]
    public void ApplyDamage(float damage)
    {
        print("Player health : " + health);
        health = health - damage;

        if (health <= 0.0f)
        {
            Notify(NotificationType.PLAYER_DEAD);
        }
    }

    public float GetHealth()
    {
        return this.health;
    }

    public float GetStamina()
    {
        return this.sprint_value;
    }
    #endregion

    #region [MOVEMENT FUNCTIONS]
    private void Move()
    {
        move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); //get left and right from keyboard

        move_direction = transform.TransformDirection(move_direction); //transform from local space to world space
        move_direction = move_direction * speed * Time.deltaTime; //multiply move direction by speed by frames per second

        ApplyGravity(); //check if we jumped

        character_controller.Move(move_direction); //move the character
    }

    private void ApplyGravity()
    {
        if (character_controller.isGrounded)
        {
            vertical_velocity = -gravity * Time.deltaTime; //clear gravity from vertical velocity

            jumped = false;

            Jump(); //check if we jump
        }

        else
        {
            if (jumped)
            {
                vertical_velocity -= gravity * Time.deltaTime;
            }
            else
            {
                vertical_velocity = -gravity * 30.5f * Time.deltaTime;
            }
        }

        move_direction.y = vertical_velocity * Time.deltaTime; //translate in the y direction
    }

    private void Jump()
    {
        if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        { //check if we pressed space bar
            vertical_velocity += jump_force * 1.5f;
            jumped = true;
        }
    }

    private void Sprint()
    {
        if (sprint_value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
            {
                speed = sprint_speed;
                player_footstep.step_distance = sprint_step_distance;
                player_footstep.volume_min = sprint_volume;
                player_footstep.volume_max = sprint_volume;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
        {
            speed = move_speed;
            player_footstep.volume_min = walk_volume_min;
            player_footstep.volume_max = walk_volume_max;
            player_footstep.step_distance = walk_step_distance;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !is_crouching)
        {
            sprint_value -= sprint_threshold * Time.deltaTime;

            if (sprint_value <= 0.0f)
            {
                sprint_value = 0.0f;
                speed = move_speed;
                player_footstep.volume_min = walk_volume_min;
                player_footstep.volume_max = walk_volume_max;
                player_footstep.step_distance = walk_step_distance;
            }
        }
        else
        {
            if (sprint_value != 100.0f)
            {
                sprint_value += (sprint_threshold / 2) * Time.deltaTime;
                if (sprint_value > 100.0f)
                {
                    sprint_value = 100.0f;
                }
            }
        }

    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) //if we press C
        {
            if (is_crouching) //stand up
            {
                look_root.localPosition = new Vector3(0f, stand_height, 0.0f);
                speed = move_speed;

                player_footstep.step_distance = walk_step_distance;
                player_footstep.volume_min = walk_volume_min;
                player_footstep.volume_max = walk_volume_max;

                is_crouching = false;
            }
            else //crouch
            {
                look_root.localPosition = new Vector3(0f, crouch_height, 0.0f);
                speed = crouch_speed;
                player_footstep.volume_min = crouch_volume;
                player_footstep.volume_max = crouch_volume;
                player_footstep.step_distance = crouch_step_distance;

                is_crouching = true;
            }
        }
    }
    #endregion

    #region [WEAPON FUNCTIONS]
    private WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_weapon_index];
    }

    private void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (current_weapon_index == weaponIndex)
        {
            return;
        }

        weapons[current_weapon_index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        current_weapon_index = weaponIndex;
    }

    private void DisarmPlayer()
    {
        weapons[current_weapon_index].gameObject.SetActive(false);
    }

    private void ShootWeapon()
    {
        if (GetCurrentSelectedWeapon().fire_type == WeaponFireType.MULTIPLE)
        {
            //Assult Rifle
            if (Input.GetMouseButton(0) && Time.time > next_time_to_fire) //Time.time is how many seconds has passed since the beginning of the game
            {
                next_time_to_fire = Time.time + 1f / fire_rate;
                GetCurrentSelectedWeapon().ShootAnimation();
                GetCurrentSelectedWeapon().playMuzzleFlash();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Axe
                if (GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    GetCurrentSelectedWeapon().ShootAnimation();
                }

                //Revolver or Shotgun
                if (GetCurrentSelectedWeapon().bullet_type == WeaponBulletType.BULLET)
                {
                    GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                    GetCurrentSelectedWeapon().playMuzzleFlash();
                }

                //Arrow or Spear
                else
                {
                    if (is_aiming)
                    {
                        GetCurrentSelectedWeapon().ShootAnimation();

                        if (GetCurrentSelectedWeapon().bullet_type == WeaponBulletType.ARROW)
                        {
                            ThrowArrowOrSpear(true);
                        }

                        else if (GetCurrentSelectedWeapon().bullet_type == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }

    private void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = arrow_spear_start_position.position;

            arrow.GetComponent<BowAndArrow>().Launch(main_camera);
        }

        else
        {
            GameObject spear = Instantiate(spear_prefab);
            spear.transform.position = arrow_spear_start_position.position;

            spear.GetComponent<BowAndArrow>().Launch(main_camera);
        }
    }

    private void Zoom()
    {
        //We are going to aim with our camera on the weapon
        if (GetCurrentSelectedWeapon().weapon_aim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnimator.Play(AnimationTags.ZOOM_IN_ANIMATION);
                crosshair.SetActive(false);
            }
            //release right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnimator.Play(AnimationTags.ZOOM_OUT_ANIMATION);
                crosshair.SetActive(true);
            }
        }

        if (GetCurrentSelectedWeapon().weapon_aim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetCurrentSelectedWeapon().Aim(true);
                is_aiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                GetCurrentSelectedWeapon().Aim(false);
                is_aiming = false;
            }
        }
    }

    private void BulletFired()
    {
        RaycastHit hit;

        if (Physics.Raycast(main_camera.transform.position, main_camera.transform.forward, out hit))
        {
            if (hit.transform.GetComponent<EnemyController>() != null)
            {
                hit.transform.GetComponent<EnemyController>().ApplyDamage(damage);
            }
        }
    }
    #endregion

    #region [OBJECT PICKUP FUNCTIONS]
    private void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(main_camera.transform.position, Vector3.forward, Color.red);
            if (Physics.Raycast(main_camera.transform.position, main_camera.transform.forward, out hit, 10))
            {
                if (hit.transform != null)
                {
                    float distance = Vector3.Distance(this.transform.position, hit.transform.position);

                    if (distance >= 1.0f)
                    {
                        canCarry = true;
                        if (hit.transform.GetComponent<Rigidbody>() != null)
                        {
                            hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                            hit.transform.parent = pickupDestination.transform;
                            hit.transform.position = pickupDestination.transform.position;
                            isCarrying = true;
                            canCarry = false;
                            disarmed = true;
                        }
                    }
                    else
                    {
                        canCarry = false;
                    }
                }
            }
        }

        if (isCarrying == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                pickupDestination.transform.GetChild(0).transform.GetComponent<Rigidbody>().isKinematic = false;
                pickupDestination.transform.GetChild(0).transform.transform.parent = null;
                isCarrying = false;
                reset = true;
                disarmed = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MOVEABLE_OBJECT" && isCarrying)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }
    #endregion
}