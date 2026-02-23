using UnityEngine;
using UnityEngine.SceneManagement;

//Build Profilesのシーン名と一致させて管理するenum
public enum SceneName
{
    TITLE_SCENE,
    CHARACTER_SCENE
};

//シーン変更を行うプログラム
public class SceneChanger : MonoBehaviour
{
    public SceneName scene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
