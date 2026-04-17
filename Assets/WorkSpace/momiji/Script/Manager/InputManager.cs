using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SceneName currentScene;
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject[] panels;
    
    private readonly float _atractSize = 1.2f; //選択中のパネルの拡大したサイズ
    private readonly float _panelDistance = 1200f; //パネル同士の距離
    private readonly float _panelHeight = -15f; //パネルのY座標
    private readonly float _panelMoveSpeed = 1.0f; //パネルが動く速さ
    private int _selectNumber; //選択中のものを示す値
    private bool _isSliding = false; //パネルが動いてるかどうか
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip confirmClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _selectNumber = 0;
        PanelSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.NONE) return;
        
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (_isSliding) return;
            
            _selectNumber++;
            if(_selectNumber >= panels.Length) _selectNumber = 0;
            
            StartCoroutine(SlidePanel(_selectNumber));
            audioSource.PlayOneShot(selectClip);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            if (_isSliding) return;
            
            _selectNumber--;
            if (_selectNumber < 0) _selectNumber = panels.Length - 1;
            
            StartCoroutine(SlidePanel(_selectNumber));
            audioSource.PlayOneShot(selectClip);
        }
        
        for (int i = 0; i < panels.Length; i++) { 
            panels[i].GetComponent<RectTransform>().localScale = (i == _selectNumber) ? Vector3.one * _atractSize : Vector3.one;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextScene();
        }
    }

    /// <summary>
    /// 選択を決定し、次のシーンに切り替える
    /// </summary>
    private void NextScene()
    {
        if (currentScene == SceneName.CHARACTER_SELECT_SCENE) panels[_selectNumber].GetComponent<CharacterSelect>()?.ChangeCharacter();
        
        audioSource.PlayOneShot(confirmClip);
        StartCoroutine(sceneChanger.ChangeScene());
    }

    /// <summary>
    /// 選択を左右に切り替えた時のパネルムーブ
    /// </summary>
    /// <param name="cur">現在のパネルのインデックス</param>
    private IEnumerator SlidePanel(int cur)
    {
        Vector2[] startPos = new Vector2[panels.Length];
        Vector2[] endPos = new Vector2[panels.Length];
        
        _isSliding = true;
        
        for (int i = 0; i < panels.Length; i++)
        {
            startPos[i] = panels[i].GetComponent<RectTransform>().localPosition;
            
            int diff = i - _selectNumber;
            
            if (diff > panels.Length/2) diff -= panels.Length;
            if (diff < -panels.Length/2) diff += panels.Length;
            
            endPos[i] = new Vector2(diff * _panelDistance, _panelHeight);
        }

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * _panelMoveSpeed;
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].GetComponent<RectTransform>().localPosition = Vector2.Lerp(startPos[i], endPos[i], t);
            }
            yield return null;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].GetComponent<RectTransform>().localPosition = endPos[i];
        }
        
        _isSliding = false;
    }

    /// <summary>
    /// パネルの初期位置設定
    /// </summary>
    private void PanelSetting()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            int diff = i - _selectNumber;
            
            if (diff > panels.Length/2) diff -= panels.Length;
            if (diff < -panels.Length/2) diff += panels.Length;
            
            panels[i].GetComponent<RectTransform>().localPosition = new Vector2(diff * _panelDistance, _panelHeight);
        }
    }
}
