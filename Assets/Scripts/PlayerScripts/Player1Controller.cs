// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody rb;
    Vector3 movement;

    private float activeMoveSpeed;

    [SerializeField]
    public float dashSpeed;

    public float dashLength = 0.2f;
    public float dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    private bool facingRight = true;
    public bool isDashing;
    
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
            movement.x = Input.GetAxisRaw("Player1 Horizontal");
            movement.z = Input.GetAxisRaw("Player1 Vertical");

            movement.Normalize();

            rb.velocity = movement * activeMoveSpeed;
    }
}
