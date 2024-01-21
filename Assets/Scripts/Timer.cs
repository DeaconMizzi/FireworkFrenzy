using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float secondsCount;
    private int minuteCount;
    private bool isTimerRunning = true;
    private const string HighScoreFileName = "highScores.json"; // File name for JSON
    public List<float> highScores;
    private const int MaxHighScores = 5;


    private string HighScoreFilePath
    {
        get { return Path.Combine(Application.persistentDataPath, HighScoreFileName); }
    }

    void Start()
    {
        LoadHighScores(); // Load the high scores when the script starts
    }

    void Update()
    {
        UpdateTimerUI();
    }

    public void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            SaveHighScore();
            SaveHighScoresToJson();
        }
    }

    // Call this on update
    public void UpdateTimerUI()
    {
        if (!isTimerRunning)
            return;
        // Set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            minuteCount = 0;
        }
    }

[Serializable]
public class HighScoreData
{
    public List<float> highScores;
}

  private void LoadHighScores()
    {
    if (File.Exists(HighScoreFilePath))
    {
        string jsonString = File.ReadAllText(HighScoreFilePath);

        try
        {
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(jsonString);
            highScores = data != null ? data.highScores : new List<float>();
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading high scores: " + e.Message);
            highScores = new List<float>();
        }
    }
    else
    {
        highScores = new List<float>();
    }

    Debug.Log("Loaded high scores: " + highScores.Count);
    }

    // Save the timer value as a high score to the JSON file
    public void SaveHighScore()
{
    float totalSeconds = (minuteCount * 60) + secondsCount;

    // Load existing high scores
    LoadHighScores();

    // Add the new score to the list
    highScores.Add(totalSeconds);

    // Sort the list in descending order (highest to lowest)
    highScores.Sort((a, b) => b.CompareTo(a));

    // Keep only the top N scores
    if (highScores.Count > MaxHighScores)
    {
        highScores.RemoveRange(MaxHighScores, highScores.Count - MaxHighScores);
    }

    // Save the updated high scores
    SaveHighScoresToJson();

    Debug.Log("High score saved successfully. Total high scores: " + highScores.Count);
}

    // Save the high scores to the JSON file
    public void SaveHighScoresToJson()
    {
        try
        {
            HighScoreData data = new HighScoreData
            {
                highScores = this.highScores
            };

            string jsonString = JsonUtility.ToJson(data);
            Debug.Log("High scores saved JSON: " + jsonString);

            File.WriteAllText(HighScoreFilePath, jsonString);
            Debug.Log("High scores saved to: " + HighScoreFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving high scores: " + e.Message);
        }
    }
}