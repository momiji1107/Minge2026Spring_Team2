using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRankingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreRankingText;
    private const int rankingLength = 3; //表示するランキングの長さ
    private string noData = "no data"; //スコアが登録されていない時の表示用
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ScoreRanking.isFirstTime) Init();
        else UpdateScoreRanking();
    }

    //ランキングテキストの初期化
    private void Init()
    {
        ScoreRanking.isFirstTime = false;
        for (int i = 0; i < rankingLength; i++)
        {
            ScoreRanking.scoreRanking.Add(noData);
            _scoreRankingText[i].text = ScoreRanking.scoreRanking[i];
        }
    }

    private void UpdateScoreRanking()
    {
        for(int i = 0; i < rankingLength; i++)
        {
            Debug.Log($"text{i}: {_scoreRankingText[i].text} => {ScoreRanking.scoreRanking[i]}");
            _scoreRankingText[i].text = ScoreRanking.scoreRanking[i];
        }
    }
}
