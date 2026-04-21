using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float clearTime = 50.0f;
    public float gameTimer = 0f;
    
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioManager audioManager;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    private bool isClear = false; //ゲームをクリアしたかどうか
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.GameState == GAMESTATE.INGAME)
        {
            gameTimer += Time.deltaTime;
        }

        if (gameTimer >= clearTime)
        {
            if(!isClear) GameClear();
        }
    }

    //ゲームオーバー
    public void GameOver()
    {
        GameManagement.GameState = GAMESTATE.GAMEOVER;
        StartCoroutine(audioManager.GameOver());
        gameOverPanel.SetActive(true);
        scoreManager.DisplayScore();
        StartCoroutine(DropText());
    }

    //ゲームオーバーテキストを表示するアニメーション
    private IEnumerator DropText()
    {
        var wait = new WaitForSecondsRealtime(Common.OneFrameTime);
        Vector2 pos = gameOverText.GetComponent<RectTransform>().anchoredPosition;
        while (pos.y > 400f)
        {
            Debug.Log("Panel move");
            pos.y -= 1.0f;
            gameOverText.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return wait;
        }
    }

    private void GameClear()
    {
        GameManagement.GameState = GAMESTATE.CLEAR;
        isClear = true;

        //音を鳴らしてスコアを表示する
        StartCoroutine(audioManager.GameClear());
        scoreManager.DisplayScore();
    }
}
