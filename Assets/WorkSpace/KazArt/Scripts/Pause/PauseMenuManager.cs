using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
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
        Debug.Log("ホームボタンに戻るボダンが押されました");
    }
}
