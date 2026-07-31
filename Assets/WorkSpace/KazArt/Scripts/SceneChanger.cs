using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//Build Profiles�̃V�[�����ƈ�v�����ĊǗ�����enum
public enum SceneName
{
    NONE = 0,
    TITLE_SCENE,
    TUTORIAL_SCENE,
    STAGE_SELECT_SCENE,
    MODE_SELECT_SCENE,
    CHARACTER_SELECT_SCENE,
    INGAME_SCENE,
    STAGE_ONE,
    STAGE_TWO,
    STAGE_THREE,
    CREDIT_SCENE
};

//�V�[���ύX���s���v���O����
public class SceneChanger : MonoBehaviour
{
    public SceneName nextScene;
    public GAMESTATE nextGameState;
    [SerializeField] private float changeTime = 1.0f;
    [SerializeField] private Image fadeImage;

    void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TITLE_SCENE":
                GameManagement.SetCurrentScene(SceneName.TITLE_SCENE);
                break;
            case "TUTORIAL_SCENE":
                GameManagement.SetCurrentScene(SceneName.TUTORIAL_SCENE);
                break;
            case "STAGE_SELECT_SCENE":
                GameManagement.SetCurrentScene(SceneName.STAGE_SELECT_SCENE);
                break;
            case "MODE_SELECT_SCENE":
                GameManagement.SetCurrentScene(SceneName.MODE_SELECT_SCENE);
                break;
            case "CHARACTER_SELECT_SCENE":
                GameManagement.SetCurrentScene(SceneName.CHARACTER_SELECT_SCENE);
                break;
            case "INGAME_SCENE":
                GameManagement.SetCurrentScene(SceneName.INGAME_SCENE);
                break;
            case "STAGE_ONE":
                GameManagement.SetCurrentScene(SceneName.STAGE_ONE);
                break;
            case "STAGE_TWO":
                GameManagement.SetCurrentScene(SceneName.STAGE_TWO);
                break;
            case "STAGE_THREE":
                GameManagement.SetCurrentScene(SceneName.STAGE_THREE);
                break;
            default:
                GameManagement.SetCurrentScene(SceneName.NONE);
                break;
        }
    }
    
    public IEnumerator ChangeScene()
    {
        if(fadeImage == null) Debug.Log("fadeImage is null");
        fadeImage.DOFade(1, changeTime).SetUpdate(true);
        yield return new WaitForSecondsRealtime(changeTime);
        fadeImage.color = new Color(0, 0, 0, 1f); 
        GameManagement.LoadScene(nextScene);
        GameManagement.GameState = nextGameState;
    }
}