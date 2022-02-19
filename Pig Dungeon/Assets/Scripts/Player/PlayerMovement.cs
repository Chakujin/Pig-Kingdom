using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runspeed;

    private bool b_jump;
    private bool b_crouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        f_horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        if (Input.GetButtonDown("Jump"))
        {
            b_jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            b_crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            b_crouch = false;
        }
    }

    private void FixedUpdate()
    {
        //Movement
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;
    }

}
