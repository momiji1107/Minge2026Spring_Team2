using System;
using UnityEngine;

public class KariClearButton : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float waitTime = 0f;
    
    public void Clear()
    {
        scoreManager.DisplayScore();
    }

    public void GameOver()
    {
        gameManager.GameOver(waitTime);
    }
}
