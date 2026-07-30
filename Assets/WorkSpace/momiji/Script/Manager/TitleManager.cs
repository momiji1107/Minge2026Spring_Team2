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
    
    static bool isFirstTimePlay = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        
        GameManagement.GameState = GAMESTATE.NONE;
    }

    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isFirstTimePlay)
            {
                sceneChanger.nextScene = SceneName.TUTORIAL_SCENE;
                isFirstTimePlay = false;
                StartCoroutine(NextScene());
            }
            else
            {
                sceneChanger.nextScene = SceneName.CHARACTER_SELECT_SCENE;
                StartCoroutine(NextScene());
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CreditScene());
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

    private IEnumerator CreditScene()
    {
        audioSource.PlayOneShot(startClip);
        sceneChanger.nextScene = SceneName.CREDIT_SCENE;
        StartCoroutine(sceneChanger.ChangeScene());
        yield return new WaitForSeconds(0.3f);
    }
}
