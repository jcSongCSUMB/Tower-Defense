using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;

    public int levelToUnlock = 2;
    public string nextLevelName = "Level02";

    public SceneFader sceneFader;

    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
            return;
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevelName);
    }
}
