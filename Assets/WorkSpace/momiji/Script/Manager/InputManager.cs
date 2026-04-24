using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SceneName currentScene;
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject[] panels;
    private readonly float _atractSize = 1.2f; //選択中のパネルの拡大したサイズ
    private readonly float _nonAtractSize = 0.8f; //非選択中のパネルサイズ
    private readonly float _panelDistance = 1100f; //パネル同士の距離
    private readonly float _panelHeight = 35f; //パネルのY座標
    private readonly float _panelMoveSpeed = 1.5f; //パネルが動く速さ
    private float _limitPosX; //パネルの左右移動量限度
    private int _selectNumber; //選択中のパネルを示すインデックス
    private bool _isSliding = false; //パネルが動いてるかどうか
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip confirmClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        _selectNumber = 0;
        _limitPosX = _panelDistance * panels.Length/2;
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
            
            StartCoroutine(SlidePanel("right"));
            audioSource.PlayOneShot(selectClip);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            if (_isSliding) return;
            
            _selectNumber--;
            if (_selectNumber < 0) _selectNumber = panels.Length - 1;
            
            StartCoroutine(SlidePanel("left"));
            audioSource.PlayOneShot(selectClip);
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
    private IEnumerator SlidePanel(string opt)
    {
        RectTransform[] rt = new RectTransform[panels.Length];
        Vector2[] startPos = new Vector2[panels.Length];
        Vector2[] endPos = new Vector2[panels.Length];
        Vector2[] startScale = new Vector2[panels.Length];
        Vector2[] endScale = new Vector2[panels.Length];
        
        _isSliding = true;
        
        for (int i = 0; i < panels.Length; i++)
        {
            rt[i] = panels[i].GetComponent<RectTransform>();
            startPos[i] = rt[i].localPosition;
            startScale[i] = rt[i].localScale;
            
            endPos[i] = new Vector2(CulucDiff(i, opt) * _panelDistance, _panelHeight);
            endScale[i] = (i == _selectNumber) ? Vector3.one * _atractSize : Vector3.one * _nonAtractSize;
        }

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * _panelMoveSpeed;
            for (int i = 0; i < panels.Length; i++)
            {
                rt[i].localPosition = Vector2.Lerp(startPos[i], endPos[i], t);
                rt[i].localScale = Vector3.Lerp(startScale[i], endScale[i], t);
                
                if(Mathf.Abs(rt[i].localPosition.x) >= _limitPosX)
                {
                    startPos[i] = rt[i].localPosition.x < 0 ? new Vector2(_limitPosX, _panelHeight) : new Vector2(-_limitPosX, _panelHeight);
                    endPos[i] = new Vector2(CulucDiff(i, "loop") * _panelDistance, _panelHeight);
                }
                
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
            int diff = CulucDiff(i, "loop");
            
            panels[i].GetComponent<RectTransform>().localPosition = new Vector2(diff * _panelDistance, _panelHeight);
            panels[i].GetComponent<RectTransform>().localScale = (i == _selectNumber) ? Vector3.one * _atractSize : Vector3.one * _nonAtractSize;
        }
    }

    /// <summary>
    /// 選択中のパネルからそれぞれのパネルの移動先を計算する
    /// </summary>
    /// <param name="i">panelsのインデックス</param>
    /// <param name="opt">計算オプション</param>
    /// <returns>移動先</returns>
    private int CulucDiff(int i, string opt)
    {
        int diff = i - _selectNumber;

        switch(opt)
        {
            case "right":
                if(diff > 0) diff -= panels.Length;
                break;
            case "left":
                if(diff < 0) diff += panels.Length;
                break;
            case "loop":
                if (diff > panels.Length / 2) diff -= panels.Length;
                if (diff < -panels.Length / 2) diff += panels.Length;
                break;
            default:
                break;
        }

        return  diff;
    }
}
