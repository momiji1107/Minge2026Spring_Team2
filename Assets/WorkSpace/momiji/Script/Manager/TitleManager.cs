using System.Collections;
using UnityEngine;

/// <summary>
/// ゲーム開始時にGAMESTATEを設定する仮のスクリプト
/// タイトル画面に置くオブジェクトにつけたい
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startClip;
    
    [Header("Sprite関係")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManagement.GameState = GAMESTATE.NONE;
    }

    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(NextScene());
        }
    }
    
    private IEnumerator NextScene()
    {
        audioSource.PlayOneShot(startClip);
        StartCoroutine(sceneChanger.ChangeScene());
        sr.sprite = sprite2;
        yield return new WaitForSeconds(0.3f);
        sr.sprite = sprite1;
    }
}
