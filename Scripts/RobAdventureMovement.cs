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
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement details")]
    public float speed = 10f;
    public float JumpForce = 10;
    public Rigidbody Rigidbody;
    public float TurnSpeed;

    private bool isGrounded;
    [Header("Raycast propeties")]

    Ray ray;
    public float MaxRayDist = 100;
    public string groundTag = "JumpTrigger";
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        //raycast declaration   
        ray = new Ray(transform.position, Vector3.down);
        Vector3 rayOrigin = transform.position;
        Debug.DrawLine(rayOrigin,rayOrigin + Vector3.down * MaxRayDist, Color.blue);


        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
        }

        //jump for button
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && isGrounded == true) // equivalent of 'X'/'A'
        {
           Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
           Debug.Log("Jump key  pressed");
        }
        //jump for keyboard
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) // equivalent of 'X'/'A'
        {
           Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
           Debug.Log("Jump key  pressed");
        }

        //raycast
        if(Physics.Raycast(ray, out RaycastHit hit, MaxRayDist))
        {
            if (hit.collider.CompareTag(groundTag))
            {
                isGrounded = true;
                Debug.Log("Is Grounded By Ray V 〰️");
                Debug.DrawLine(rayOrigin,rayOrigin + Vector3.down * MaxRayDist, Color.green);
            }
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not Grounded By Ray X 〰️");
            Debug.DrawLine(rayOrigin,rayOrigin + Vector3.down * MaxRayDist, Color.red);
        }
    }

}
