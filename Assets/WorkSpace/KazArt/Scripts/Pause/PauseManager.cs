using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private PauseMenuSelectManager selectManager;
    private bool isPaused = false;

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;

        selectManager.startSelect();
    }

    private void Resume()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;

        selectManager.stopSelect();
    }
}
