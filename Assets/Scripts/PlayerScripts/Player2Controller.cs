// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody rb;
    Vector3 movement;

    private float activeMoveSpeed;
    
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
            movement.x = Input.GetAxisRaw("Player2 Horizontal");
            movement.z = Input.GetAxisRaw("Player2 Vertical");

            movement.Normalize();

            rb.velocity = movement * activeMoveSpeed;
    }
}
