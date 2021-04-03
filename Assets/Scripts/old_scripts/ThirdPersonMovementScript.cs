using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Third Person Controller inspired by https://www.youtube.com/watch?v=4HpC--2iowE

public class ThirdPersonMovementScript : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    [SerializeField] public float speed = 6f;
    [SerializeField] public float jumpSpeed = 0.5f;
    [SerializeField] public float gravity = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    
    private float fallingSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calcuate the desired movement angle based on key input and camera position
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Smooth the angle and apply
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        
        // Implement jumping and gravity without falling over
        fallingSpeed -= gravity * Time.deltaTime;
        controller.Move( new Vector3(0f, fallingSpeed, 0f) );
        if (controller.isGrounded)
        {
            fallingSpeed = 0f;
            if (Input.GetKeyDown("space"))
            {
                fallingSpeed = jumpSpeed;
            }
        }
    }
}
