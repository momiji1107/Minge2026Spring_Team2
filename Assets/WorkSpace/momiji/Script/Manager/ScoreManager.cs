using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("ステータス表示")]
    [SerializeField] private PlayerModel model;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    
    private int score = 0;
    private int killCount = 0;
    
    [Header("スコアパネルを表示する座標")]
    [SerializeField] private Vector3 clearScorePos = new  Vector3(0f, 85f, 0f);
    [SerializeField] private Vector3 gameOverScorePos = new  Vector3(0f, -65f, 0f);

    private void Update()
    {
        levelText.text = "LV : " + model.Level;
        expText.text = "EXP : " + model.Exp + "/300";
        //expText.text = "EXP : " + model.Exp + "/" + model.RequireExp;
    }

    //スコアをリセット
    private void ResetScore()
    {
        score = 0;
        killCount = 0;
        gameManager.gameTimer = 0f;
    }

    //スコアを加算
    public void AddScore(int amount)
    {
        score += amount;
        killCount++;
    }

    //スコア画面を表示する
    public void DisplayScore()
    {
        Time.timeScale = 0f;
        
        //スコアをテキストに反映
        killCountText.text = "Kill Count     " + killCount;
        scoreText.text = "Score     " + score;
        clearTimeText.text = "Clear Time     " + gameManager.gameTimer;
        
        //スコアパネルの座標を設定する
        if(GameManagement.GameState == GAMESTATE.GAMEOVER) scorePanel.GetComponent<RectTransform>().anchoredPosition = gameOverScorePos;
        else if(GameManagement.GameState == GAMESTATE.CLEAR) scorePanel.GetComponent<RectTransform>().anchoredPosition = clearScorePos;
        scorePanel.SetActive(true);
    }

    //スコア画面を閉じ、キャラ選択画面へ移行する
    public void CloseScore()
    {
        scorePanel.SetActive(false);
        ResetScore();
        
        //キャラ選択画面にシーン移行
        StartCoroutine(sceneChanger.ChangeScene());
    }

    void Start()
    {
        ResetScore();
        EnemyCore.AddScoreToPlayer += AddScore;
        scorePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        EnemyCore.AddScoreToPlayer -= AddScore;
    }
}
