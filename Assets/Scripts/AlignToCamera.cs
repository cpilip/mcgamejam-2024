using System;
using UnityEngine;

public class AlignToCamera : MonoBehaviour
{
    private void Start()
    {
        var cam = Camera.main;
        if (cam)
        {
            transform.rotation = cam.transform.rotation;
        }
    }
}
