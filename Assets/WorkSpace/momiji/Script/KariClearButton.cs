using System;
using UnityEngine;

public class KariClearButton : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;
    
    public void Clear()
    {
        scoreManager.DisplayScore();
    }

    public void GameOver()
    {
        gameManager.GameOver();
    }
}
