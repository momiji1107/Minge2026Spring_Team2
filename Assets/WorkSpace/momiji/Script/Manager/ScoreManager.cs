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
    
    private int score = 0;
    private int killCount = 0;

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
        GameManagement.GameState = GAMESTATE.CLEAR;
        Time.timeScale = 0f;
        
        //スコアをテキストに反映
        killCountText.text = "Kill Count     " + killCount;
        scoreText.text = "Score     " + score;
        clearTimeText.text = "Clear Time     " + gameManager.gameTimer;
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
}
