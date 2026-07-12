using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject confirmPanel;

    public void OnOptionButton()
    {
        Debug.Log("オプションボタンが押されました");
    }

    public void OnOperationButton()
    {
        Debug.Log("操作方法ボタンが押されました");
    }

    public void OnHomeButton()
    {
        confirmPanel.SetActive(true);
        pausePanel.SetActive(false);
        Debug.Log("ホームボタンに戻るボダンが押されました");
    }

    public void OnConfirmYes()
    {
        StartCoroutine(sceneChanger.ChangeScene());
    }

    public void OnConfirmNo()
    {
        confirmPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
