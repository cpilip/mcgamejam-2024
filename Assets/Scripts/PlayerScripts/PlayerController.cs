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
    
    private Rigidbody rb;
    
    private Vector2 movement;

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
        movement.x = Input.GetAxisRaw(horizontalInput);
        movement.y = Input.GetAxisRaw(verticalInput);
        
        movement.Normalize();

        rb.velocity = movement.x * horizontalMoveSpeed * camAlignedRight
                      + movement.y * verticalMoveSpeed * camAlignedUp;
    }
}
