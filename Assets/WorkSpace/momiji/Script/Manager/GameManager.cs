using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float clearTime = 300.0f;
    public float gameTimer = 0f;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverClip;
    
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
            GameOver();
        }
    }

    public void GameOver()
    {
        GameManagement.GameState = GAMESTATE.GAMEOVER;
        Time.timeScale = 0f;
        audioSource.PlayOneShot(gameOverClip);
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
}
