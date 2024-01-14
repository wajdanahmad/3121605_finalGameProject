using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
       public float turnSpeed = 360.0f;
       public Animator animator;
       private Rigidbody rb;

       void Start()
       {
           
           rb = GetComponent<Rigidbody>();
       }

       void Update()
       {
           float horizontal = Input.GetAxis("Horizontal");
           float vertical = Input.GetAxis("Vertical");
           Vector3 movement = new Vector3(horizontal, 0, vertical);

           // Moving and rotating the character
           //if (movement.magnitude > 0)
           {
               Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
           }

           // Apply the movement
           transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

           // Update the Animator with the movement magnitude
           animator.SetFloat("speed", movement.magnitude);
       }
}
