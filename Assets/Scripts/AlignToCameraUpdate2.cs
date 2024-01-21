using System;
using UnityEngine;

public class AlignToCamera2 : MonoBehaviour
{
    // this one updates every frame (for the clouds and stars)

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update() {
        if (cam) transform.rotation = cam.transform.rotation;
    }
}
