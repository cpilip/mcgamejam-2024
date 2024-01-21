// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public string horizontalInput = "Player1 Horizontal";
    public string verticalInput = "Player1 Vertical";

    public float horizontalMoveSpeed = 8f;
    public float verticalMoveSpeed = 12f;
    
    public Animator animator;
    private Rigidbody rb;
    
    private Vector2 prevMovement;

    private Camera cam;
    private Vector3 camAlignedRight;
    private Vector3 camAlignedUp;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;
        
        camAlignedRight = cam.transform.right;
        camAlignedUp = Vector3.Cross(camAlignedRight, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement;
        movement.x = Input.GetAxisRaw(horizontalInput);
        movement.y = Input.GetAxisRaw(verticalInput);
        
        movement.Normalize();

        if (movement == Vector2.zero)
        {
            animator.Play("idleAnimation");
        }
        else
        {
            animator.Play("walkingAnimation");
        }

        if (prevMovement.x <= 0 && movement.x > 0)
        {
            animator.Play("turnForward");
        }
        if (prevMovement.x >= 0 && movement.x < 0)
        {
            animator.Play("turnReversed");
        }

        rb.velocity = movement.x * horizontalMoveSpeed * camAlignedRight
                      + movement.y * verticalMoveSpeed * camAlignedUp
                      + (rb.useGravity ? rb.velocity.y : 0) *  Vector3.up;

        prevMovement = movement;
    }

    public void teleportPlayer(Transform target)
    {
        rb.velocity = Vector3.zero;
        rb.position = target.position + new Vector3(-1.5f, 8.0f, 1.5f);
    }
}
