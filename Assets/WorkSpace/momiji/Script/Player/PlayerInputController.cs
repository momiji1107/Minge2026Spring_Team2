using System.Collections;
using UnityEngine;
using TMPro;
public class PlayerInputController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerModel model;
    private Rigidbody2D rb;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private SceneChanger sceneChanger;

    [Header("縦移動")]
    [SerializeField,Tooltip("レーン移動後に再びレーン移動できるようになるまでの時間")] private float laneMoveTime = 0.5f;
    private float laneMoveTimer = 0.0f;
    [SerializeField,Tooltip("レーンオブジェクトの配列")] private GameObject[] lanes;
    private int laneidx = 0; //今いるレーンのインデックス
    
    private float horizontal; //横入力
    private float vertical; //縦入力
    private float heightAdjust = 0.1f; //レーンからの高さ補正
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip laneChangeClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        laneMoveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        laneMoveTimer += Time.deltaTime;

        //Shiftキーを押すとオブジェクトを反転させる
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && GameManagement.GameState == GAMESTATE.INGAME)
        {
            model.TurnAround();
            Player.transform.Rotate(0, 180, 0);
        }
        
        //アップグレード中の操作に切り替える
        if (GameManagement.GameState == GAMESTATE.ISUPGRADE)
        {
            upgradeManager.UpgradeInput();
        }

        //ゲームオーバー時にEnterを押すとキャラ選択画面に戻る
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManagement.GameState == GAMESTATE.GAMEOVER)
            {
                StartCoroutine(sceneChanger.ChangeScene());
            }
        }
    }

    void FixedUpdate()
    {

        if (GameManagement.GameState != GAMESTATE.INGAME) return;
        
        HorizontalMove();
        VerticalMove();
        
    }

    //左右移動
    void HorizontalMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        
        Vector2 velocity = new Vector2(horizontal, 0) * model.MoveSpeed;
        rb.linearVelocity = velocity;
        
        //移動音を鳴らす
        if(velocity.magnitude > 0 && !audioSource.isPlaying) audioSource.PlayOneShot(moveClip);
    }

    //上下移動
    void VerticalMove()
    { 
        vertical = Input.GetAxis("Vertical");
        if (vertical > 0 && laneMoveTimer >= laneMoveTime)
        {
            if (laneidx >= lanes.Length - 1) return;
            laneMoveTimer = 0.0f;
            laneidx++;
            audioSource.PlayOneShot(laneChangeClip);
        }
        if (vertical < 0 && laneMoveTimer >= laneMoveTime)
        {
            if (laneidx <= 0) return;
            laneMoveTimer = 0.0f;
            laneidx--;
            audioSource.PlayOneShot(laneChangeClip);
        }
            
        Player.transform.position = new Vector2(Player.transform.position.x, lanes[laneidx].transform.position.y + heightAdjust);
    }
}
