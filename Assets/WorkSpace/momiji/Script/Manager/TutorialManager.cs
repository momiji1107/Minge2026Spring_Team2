using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputCrl;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private float[] stopTiming = new float[5]; //チュートリアルのために時間を止めるタイミング

    [Header("テキストパネル")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI arrows; //スキルスロットを指し示す矢印のテキスト
    
    private bool inputFlag = false; //入力待ち状態かどうか

    void Start()
    {
        arrows.gameObject.SetActive(false);
    }
    
    void Update()
    {
        //経過時間に応じてチュートリアルのために時間を止める
        for (int i = 0; i < stopTiming.Length; i++)
        {
            if (_gameManager.gameTimer >= stopTiming[i] && _playerInputCrl.inputStep == i)
            {
                TutorialTime();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(_sceneChanger.ChangeScene());
        }
        
        if (!inputFlag) return;
        //入力ステップに従ってステップを進めると同時に時間を動かす
        switch (_playerInputCrl.inputStep)
        {
            case 0:
                tutorialText.text = "矢印キー←→ または ADキーで 左右いどう";
                if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
                {
                    OneStep();
                }
                break;
            case 1:
                tutorialText.text = "矢印キー↑↓ または WSキー で 上下いどう";
                if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
                {
                    OneStep();
                }
                break;
            case 2:
                tutorialText.text = "Spaceキー で こうげき";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    OneStep();
                }
                break;
            case 3:
                tutorialText.text = "Shiftキー で 反転";
                if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
                {
                    OneStep();
                    inputFlag = true;
                }
                break;
            case 4:
                if (GameManagement.GameState == GAMESTATE.ISUPGRADE)
                {
                    tutorialPanel.SetActive(true);
                    tutorialText.text = "矢印キー←→ または ADキーで せんたく\nEnterキー で決定";
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        OneStep();
                        Invoke(nameof(TutorialTime), 1.0f);
                    }
                }
                break;
            case 5:
                tutorialText.text = "Xキー で スキル使用";
                if(Input.GetKeyDown(KeyCode.X))
                {
                    OneStep();
                    inputFlag = true;
                }
                break;
            case 6:
                if (GameManagement.GameState == GAMESTATE.ISUPGRADE)
                {
                    tutorialText.text = "スキルは３つまで持つことができる\nXキー, Cキー, Vキーで使い分けよう";
                    arrows.gameObject.SetActive(true);
                    tutorialPanel.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        OneStep();
                        arrows.gameObject.SetActive(false);
                        Invoke(nameof(TutorialTime), 5.0f);
                    }
                }
                break;
            case 7:
                Time.timeScale = 1;
                tutorialText.text = "じゅんびができたら出発しよう!!\n(Enterキー で出発)";
                tutorialPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _audioManager.Confirm();
                    StartCoroutine(_sceneChanger.ChangeScene());
                }
                break;
        }
    }

    void TutorialTime()
    {
        Time.timeScale = 0;
        inputFlag = true;
        tutorialPanel.SetActive(true);
    }
    
    void OneStep()
    {
        _playerInputCrl.inputStep++;
        Time.timeScale = 1;
        inputFlag = false;
        tutorialPanel.SetActive(false);
    }
}
