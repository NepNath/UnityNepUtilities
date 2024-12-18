//made by NepNath 
//Creation Date: 03/12/2024
//last edited: 18/12/2024

//this script has been made for keyboard input, which mean they have not been tested with a controller.
//another script shall be made for controller input.

using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{

    [Header("Player Movement details")]
    public float speed = 10f;
    public float JumpForce = 10;
    public Rigidbody Rigidbody;

    private bool isGrounded;


    void Start()
    {
        
    }

    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        

        transform.Translate(new Vector3(HorizontalInput, 0, VerticalInput) * speed * Time.deltaTime);
    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
           Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

        // debug log for key presses
        //Jump keys
        if (Input.GetKeyDown("space") && isGrounded == false) 
        {
            Debug.Log("Jump key pressed But Cannot jump");
        }

        

        
   }
    // outsite update
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


