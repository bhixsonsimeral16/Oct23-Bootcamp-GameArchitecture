using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isFinalLevel;

    public UnityEvent onLevelStart, onLevelComplete;
    
    public void StartLevel()
    {
        onLevelStart?.Invoke();
    }

    public void CompleteLevel()
    {
        onLevelComplete?.Invoke();
        if(isFinalLevel)
        {
            GameManager.instance.ChangeState(GameManager.GameState.GameComplete, this);
        }
    }
}
