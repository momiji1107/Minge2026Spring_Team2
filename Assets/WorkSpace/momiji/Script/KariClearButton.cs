using System;
using UnityEngine;

public class KariClearButton : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    
    public void OnClick()
    {
        scoreManager.DisplayScore();
    }
}
