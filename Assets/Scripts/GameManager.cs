using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public GameObject gameOverPanel;
    private bool hasGameOverOccurred = false;

    void Start()
    {
        // Assuming you've assigned the Timer component in the Unity Editor
        if (timer == null)
            timer = FindObjectOfType<Timer>(); // If not assigned, find it in the scene
    }

    void Update()
{
    if (timer == null)
    {
        Debug.LogError("Timer not assigned in GameManager!");
        return;
    }

    if (GameObject.FindGameObjectWithTag("Player") == null && !hasGameOverOccurred)
    {
        // Activate the game over panel or perform other game over actions
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            timer.StopTimer();
            hasGameOverOccurred = true;  // Set the flag to prevent multiple calls
        }
    }
}
}
