using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ranking
{
    
    public bool isFirstTime = true; //ゲーム開始後1回目のプレイかどうか
    public List<string> score =  new List<string>();
}

public class ScoreRanking : MonoBehaviour
{
    public static ScoreRanking Instance; //シングルトンのインスタンス
    private const string noData = "no data"; //スコアが登録されていない時の表示用
    
    private Ranking[] _ranking; //ステージごとのランキング
    public Ranking[] Ranking => _ranking; //getter

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _ranking = new Ranking[3];

        for (int i = 0; i < _ranking.Length; i++)
        {
            _ranking[i] = new Ranking();
        }

        DontDestroyOnLoad(gameObject);

        Debug.Log("ScoreRanking set");
    }
    
    //スコアをListに追加
    public void AddScoreRanking(int score, SceneName stageName)
    {
        int rankingIdx;
        switch (stageName)
        {
            case SceneName.STAGE_ONE:
                rankingIdx = 0;
                break;
            case SceneName.STAGE_TWO:
                rankingIdx = 1;
                break;
            case SceneName.STAGE_THREE:
                rankingIdx = 2;
                break;
            default:
                rankingIdx = -1;
                break;
        }
        _ranking[rankingIdx].score.Add(score.ToString());
        
        //Listをスコアの降順でソート
        _ranking[rankingIdx].score.Sort((a, b) =>
        {
            if (a.Equals(noData)) return 1;
            if (b.Equals(noData)) return -1;
            int intA = int.Parse(a);
            int intB = int.Parse(b);
            return intB.CompareTo(intA);
        });
    }
    
}
