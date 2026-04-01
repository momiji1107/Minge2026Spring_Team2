using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SceneName currentScene;
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject[] panels;
    
    private float atractSize = 1.2f; //選択中のパネルの拡大したサイズ
    private int selectNumber; //選択中のものを示す値
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip confirmClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && selectNumber < panels.Length - 1) {
            selectNumber++;
            audioSource.PlayOneShot(selectClip);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectNumber > 0) { 
            selectNumber--;
            audioSource.PlayOneShot(selectClip);
        }
        
        for (int i = 0; i < panels.Length; i++) { 
            panels[i].GetComponent<RectTransform>().localScale = (i == selectNumber) ? Vector3.one * atractSize : Vector3.one;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextScene();
        }
    }

    private void NextScene()
    {
        if(currentScene == SceneName.CHARACTER_SELECT_SCENE) panels[selectNumber].GetComponent<CharacterSelect>()?.ChangeCharacter();
        audioSource.PlayOneShot(confirmClip);
        StartCoroutine(sceneChanger.ChangeScene());
    }
}
