using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreRanking : MonoBehaviour
{
    public static ScoreRanking Instance; //シングルトンのインスタンス
    
    public static bool isFirstTime = true; //ゲーム開始後1回目のプレイかどうか
    private static List<string> _scoreRanking = new List<string>();
    private string noData = "no data"; //スコアが登録されていない時の表示用

    public static List<string> scoreRanking => _scoreRanking; //getter

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    
    //スコアをListに追加
    public void AddScoreRanking(int score)
    {
        _scoreRanking.Add(score.ToString());
        
        //Listをスコアの降順でソート
        _scoreRanking.Sort((a, b) =>
        {
            if (a.Equals(noData)) return 1;
            if (b.Equals(noData)) return -1;
            int intA = int.Parse(a);
            int intB = int.Parse(b);
            return intB.CompareTo(intA);
        });
    }
    
}
