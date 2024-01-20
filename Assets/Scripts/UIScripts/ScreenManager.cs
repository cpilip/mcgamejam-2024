using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;


    public void StartGameScene()
    {
        Debug.Log("Loading into GameScene.");
        SceneManager.LoadScene("GameScene");  
    } 

    public void OnOpenPanel() {
        creditsPanel.SetActive(true);
    }

    public void OnClosePanel() {
        creditsPanel.SetActive(false);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
