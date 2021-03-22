using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;      
    public float speed = 12f;                   
    public float gravity = -9.81f;              
    public float sprint = 1.5f;            

    Vector3 velocity;                 
    public Transform groundCheck;            
    public LayerMask groundMask;          
    bool isGrounded;                      
    public float jumpHigth = 3f;             
    public float groundDistance = 0.2f;         

    public Camera cam;                          
    float baseFOV;                              
    float sprintFOV = 1.25f;                    

    void Start()
    {
        baseFOV = cam.fieldOfView;              
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        if (isGrounded == false)
        {
            x /= 5f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && z > 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV * sprintFOV, 5f * Time.deltaTime);
            controller.Move(move * speed * sprint * Time.deltaTime);
        }
        else    
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV, 5f * Time.deltaTime);
            controller.Move(move * speed * Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
