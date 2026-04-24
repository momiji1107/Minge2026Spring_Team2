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
    
    private bool flag = false; //ゲームをクリアしたかどうか
    
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
            if(!flag) GameClear();
        }
    }

    public void GameOver()
    {
        GameManagement.GameState = GAMESTATE.GAMEOVER;
        Time.timeScale = 0f;
        StartCoroutine(audioManager.GameOver());
        gameOverPanel.SetActive(true);
        StartCoroutine(DropText());
    }

    private IEnumerator DropText()
    {
        var wait = new WaitForSeconds(0.3f);
        Vector2 pos = gameOverText.gameObject.transform.position;
        while (pos.y > 0)
        {
            pos.y -= 0.1f;
            gameOverText.gameObject.transform.position = pos;
            yield return wait;
        }
    }

    public void GameClear()
    {
        flag = true;

        StartCoroutine(audioManager.GameClear());
        scoreManager.DisplayScore();
    }
}
