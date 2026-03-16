using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SceneName currentScene;
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject[] panels;
    
    private float atractSize = 1.2f; //選択中のパネルの拡大したサイズ
    private int selectNumber; //選択中のものを示す値
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && selectNumber < panels.Length - 1) {
            selectNumber++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectNumber > 0) { 
            selectNumber--;
        }
        
        for (int i = 0; i < panels.Length; i++) { 
            panels[i].GetComponent<RectTransform>().localScale = (i == selectNumber) ? Vector3.one * atractSize : Vector3.one;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(currentScene == SceneName.CHARACTER_SELECT_SCENE) panels[selectNumber].GetComponent<CharacterSelect>()?.ChangeCharacter();
            sceneChanger.ChangeScene();
        }
    }
}
