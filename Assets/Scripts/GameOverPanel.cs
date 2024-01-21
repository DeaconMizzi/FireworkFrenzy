using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Text highScoresText;

    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager not assigned in GameOverPanel!");
            return;
        }

        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        if (gameManager.timer == null)
        {
            Debug.LogError("Timer not assigned in GameManager!");
            return;
        }

        // Access the highScores list directly from the Timer component
        List<float> highScores = gameManager.timer.highScores;

        // Display high scores in your UI
        string scoresText = "High Scores:\n";

        for (int i = 0; i < highScores.Count; i++)
        {
            scoresText += $"{i + 1}. {FormatTime(highScores[i])}\n";
        }

        highScoresText.text = scoresText;
    }

    // Helper method to format time in minutes and seconds
    string FormatTime(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}