using System.Collections;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] CreditScrollViewer scrollViewer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManagement.GameState = GAMESTATE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.NONE) return;

        if(Input.GetKeyDown(KeyCode.Return) && scrollViewer.IsFinished)
        {
            StartCoroutine(NextScene());
        }
    }

    private IEnumerator NextScene()
    {
        StartCoroutine(sceneChanger.ChangeScene());
        yield return new WaitForSeconds(0.3f);
    }
}
