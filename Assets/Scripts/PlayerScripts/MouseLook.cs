using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] //This allows us to see private variables in unity UI
    private Transform playerRoot;

    [SerializeField]
    private Transform lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_unlock = true; //This is for mouse curser (see or not see)

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smooth_steps = 10;

    [SerializeField]
    private float smooth_weight = 0.4f;

    [SerializeField]
    private float roll_angle = 0;

    [SerializeField]
    private float roll_speed = 0f;

    [SerializeField]
    private Vector2 default_look_limits = new Vector2(-70f, 80f); //FOV

    private Vector2 look_angles;
    private Vector2 current_mouse_look;
    private Vector2 smooth_move;
    private float current_roll_angle;
    private int last_look_frame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        lockAndUnlockCursor();

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            lookAround();
        }
        
    }

    void lockAndUnlockCursor() //This function allows the user to get their mouse cursor back by pressing 'ESC'
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void lookAround()
    {
        current_mouse_look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X)); //Y in unity gives horizontal, X in unity gives verticle 

        look_angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f); //check if invert is true or false
        look_angles.y += current_mouse_look.y * sensitivity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_look_limits.x, default_look_limits.y); //will not allow x to go below x limit or y limit, sets FOV

        current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_angle, Time.deltaTime * roll_speed); //Interpolates roll angle with verticle and roll values
                                                                                                                                            //sets current roll to verticle input from mouse
        lookRoot.localRotation = Quaternion.Euler(look_angles.x, 0f, current_roll_angle); //sets the players rotation to our updated x and y inputs from the mouse
        playerRoot.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);



    }
}
