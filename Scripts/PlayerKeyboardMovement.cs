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

    [Header("Raycast propeties")]

    Ray ray;
    public float MaxRayDist;
    public string groundTag = "JumpTrigger";
    void Start()
    {
        //is empty
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
    // outsite update
}


