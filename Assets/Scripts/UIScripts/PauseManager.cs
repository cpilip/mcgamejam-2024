// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;

    private bool paused;
    private bool pressed; // avoid flashing issues 

    void Start()
    {
        pausePanel.SetActive(false);
        paused = false;
        pressed = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            if (!pressed) {
                pressed = true;
                
                FlipPauseState();
            }
        } else {
            pressed = false;
        }
    }

    public void FlipPauseState() {
        paused = !paused;
        Time.timeScale = paused? 0 : 1;
        pausePanel.SetActive(paused);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
