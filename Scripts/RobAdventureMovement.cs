//made by NepNath 
//Creation Date: 27/11/2024
//last edited: 18/12/2024

// This script is made for a student project called "RobAdventure".
// These inputs are designed for a specific set of controller handmade,
// based on a arcade machine (arcade joystick and 4 buttons)


using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement details")]
    public float speed = 10f;
    public float JumpForce = 10;
    public Rigidbody Rigidbody;

    private bool isGrounded;

    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
            Debug.Log("Horizontal" + Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += new Vector3(0, 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
            Debug.Log("Vertical" + Input.GetAxisRaw("Vertical"));
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && isGrounded == true) // equivalent of 'X'/'A'
        {
           Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
           Debug.Log("Jump key  pressed");
        }
        
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && isGrounded == false) // Debug if the player tries to jump but is not grounded
        {
            Debug.Log("Jump key pressed But Cannot jump");
        }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("JumpTrigger"))
        {
            isGrounded = true;
            Debug.Log("Is Grounded ✅");
        }
    
    }

    void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("JumpTrigger"))
            {
                isGrounded = false;
                Debug.Log("Is Not Grounded ❌");
            }
        }
    }
}
