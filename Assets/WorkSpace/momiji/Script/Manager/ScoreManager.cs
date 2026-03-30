using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    
    private int score = 0;
    private int killCount = 0;
    private float clearTimer = 0f;

    //スコアをリセット
    private void ResetScore()
    {
        score = 0; 
        killCount = 0;
        clearTimer = 0f;
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
        GameManager.GameState = GAMESTATE.CLEAR;
        Time.timeScale = 0f;
        
        //スコアをテキストに反映
        killCountText.text = "Kill Count     " + killCount;
        scoreText.text = "Score     " + score;
        clearTimeText.text = "Clear Time     " + clearTimer;
        scorePanel.SetActive(true);
    }

    //スコア画面を閉じ、キャラ選択画面へ移行する
    public void CloseScore()
    {
        scorePanel.SetActive(false);
        ResetScore();
        
        //キャラ選択画面にシーン移行
        GameManager.LoadScene(SceneName.CHARACTER_SELECT_SCENE);
        GameManager.GameState = GAMESTATE.NONE;
    }

    void Start()
    {
        ResetScore();
        EnemyCore.AddScoreToPlayer += AddScore;
        scorePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (GameManager.GameState == GAMESTATE.INGAME)
        {
            clearTimer += Time.deltaTime;
        }
    }
}
