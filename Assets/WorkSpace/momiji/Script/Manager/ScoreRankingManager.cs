using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct ScoreRankingText
{
    [SerializeField] public TextMeshProUGUI[] scoreText;
}
public class ScoreRankingManager : MonoBehaviour
{
    [SerializeField] private ScoreRankingText[] _scoreRankingText;
    private const int rankingLength = 3; //表示するランキングの長さ
    private string noData = "no data"; //スコアが登録されていない時の表示用

    private void Awake()
    {
        if (ScoreRanking.Instance == null)
        {
            GameObject obj = new GameObject("ScoreRanking");
            obj.AddComponent<ScoreRanking>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ScoreRanking.Instance.Ranking == null) Debug.LogError("ScoreRanking is NULL");
        for(int i=0; i < ScoreRanking.Instance.Ranking.Length; i++)
        {
            if(ScoreRanking.Instance.Ranking[i].isFirstTime) Init(i);
            else UpdateScoreRanking(i);
        }
    }

    //ランキングテキストの初期化
    private void Init(int idx)
    {
        ScoreRanking.Instance.Ranking[idx].isFirstTime = false;
        for (int i = 0; i < rankingLength; i++)
        {
            ScoreRanking.Instance.Ranking[idx].score.Add(noData);
            _scoreRankingText[idx].scoreText[i].text = ScoreRanking.Instance.Ranking[idx].score[i];
        }
        DontDestroyOnLoad(gameObject);
    }

    private void UpdateScoreRanking(int idx)
    {
        for(int i = 0; i < rankingLength; i++)
        {
            //Debug.Log($"{i+1}位: {_scoreRankingText[idx].scoreText[i].text} => {ScoreRanking.Instance.Ranking[idx].score[i]}");
            _scoreRankingText[idx].scoreText[i].text = ScoreRanking.Instance.Ranking[idx].score[i];
        }
    }
}
