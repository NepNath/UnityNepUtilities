//made by NepNath 
//Creation Date: 03/12/2024
//last edited: 18/12/2024

//this script has been made for keyboard input, which mean they have not been tested with a controller.
//another script shall be made for controller input.
//⚠️ This script is made for a static camera due to the player's rotation that would not follow a 3d person camera
// (Inputs would not follow the camera rotation so would be inverted because of the rotation)

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
        //--------------------------------------input declartion--------------------------------------
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        
        //--------------------------------------Raycast creation--------------------------------------
        ray = new Ray(transform.position, Vector3.down);
        Vector3 rayOrigin = transform.position;
        Debug.DrawLine(rayOrigin,rayOrigin + Vector3.down * MaxRayDist, Color.blue);

        //--------------------------------------Player Movement--------------------------------------
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
        }

        //--------------------------------------jump raycast--------------------------------------
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

        //--------------------------------------jump input--------------------------------------
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
           Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

   }
    // outsite update
}


