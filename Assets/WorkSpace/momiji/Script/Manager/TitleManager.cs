using UnityEngine;

/// <summary>
/// ゲーム開始時にGAMESTATEを設定する仮のスクリプト
/// タイトル画面に置くオブジェクトにつけたい
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.GameState = GAMESTATE.NONE;
    }

    void Update()
    {
        if (GameManager.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sceneChanger.ChangeScene();
        }
    }
}
