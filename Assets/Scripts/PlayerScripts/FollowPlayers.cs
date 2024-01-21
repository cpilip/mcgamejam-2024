using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    [SerializeField] private float speed = 10;

    private void Start() {
        mainCamera.enabled = true;
    }

    private void FixedUpdate() {
        Vector3 cameraCoords = (player1.transform.position + player2.transform.position)/2;

        transform.position = Vector3.MoveTowards(transform.position, cameraCoords, Time.deltaTime * speed);
    }
}