using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;
    [SerializeField] private GameObject endingCutscene;
    public bool skipBriefing;

    private GameState currentState;
    private LevelManager currentLevel;
    private int currentLevelIndex = 0;
    public static GameManager instance;

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelInProgress,
        LevelComplete,
        GameOver,
        GameComplete
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        if(levels.Length > 0)
        {
            if(skipBriefing && levels.Length > 1)
            {
                currentLevelIndex = 1;
                ChangeState(GameState.LevelStart, levels[1]);
            }
            else
            {
                ChangeState(GameState.Briefing, levels[0]);
            }
            
        }
    }

    public void ChangeState(GameState newState, LevelManager level)
    {
        currentState = newState;
        currentLevel = level;

        switch (currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                StartLevel();
                break;
            case GameState.LevelInProgress:
                RunLevel();
                break;
            case GameState.LevelComplete:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameComplete:
                GameComplete();
                break;
            default:
                break;
        }
    }

    private void StartBriefing()
    {
        Debug.Log("Briefing started");
        // Start Briefing audio
        ChangeState(GameState.LevelStart, currentLevel);
    }

    private void StartLevel()
    {
        Debug.Log("Level started");
        currentLevel.StartLevel();
        ChangeState(GameState.LevelInProgress, currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("Level in " + currentLevel.gameObject.name);
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Complete");
        currentLevel.CompleteLevel();
        ChangeState(GameState.LevelStart, levels[++currentLevelIndex]);
    }

    private void GameOver()
    {
        Debug.Log("Game Over, Loss Condition");
    }

    private void GameComplete()
    {
        Debug.Log("Game Complete, we won!");
        endingCutscene.SetActive(true);
    }
}
