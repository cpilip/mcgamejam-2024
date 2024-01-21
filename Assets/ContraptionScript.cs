// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraptionScript : MonoBehaviour
{
    [SerializeField] private GameObject greenlight; 
    [SerializeField] private GameObject yellowlight; 
    [SerializeField] private GameObject redlight; 


    public void turnOnGreenLight() {
        redlight.SetActive(false);
        yellowlight.SetActive(false);

        greenlight.SetActive(true);

        StartCoroutine(turnoff(greenlight));
    }

    public void turnOnYellowLight() {
        redlight.SetActive(false);
        yellowlight.SetActive(true);
        greenlight.SetActive(false);

        StartCoroutine(turnoff(yellowlight));
    }

    public void turnOnRedLight() {
        redlight.SetActive(true);
        yellowlight.SetActive(false);

        greenlight.SetActive(false);

        StartCoroutine(turnoff(redlight));

    }

    public IEnumerator turnoff(GameObject go)
    {
        yield return new WaitForSeconds(2);

        go.SetActive(false);
    }

}
