using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelState
{
    MainMenu,
    InGame
}

public class Level : MonoBehaviour
{
    private LevelState currentLevelState = LevelState.MainMenu;

    void Start()
    {

    }

    public void SetLevelState(LevelState newState)
    {
        // Additional logic related to state transitions can be added here

        // Update the current level state
        currentLevelState = newState;

        // Handle state-specific actions
        switch (currentLevelState)
        {
            case LevelState.MainMenu:
                LoadScene("Menu");
                break;

            case LevelState.InGame:
                LoadScene("Game");
                break;

        }
    }

    // Method to change the scene
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SetLevelState(LevelState.InGame);
    }


    public void BackToMenu()
    {
        SetLevelState(LevelState.MainMenu);
    }
}
