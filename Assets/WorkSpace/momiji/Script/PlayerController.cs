using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject Player;
    private Rigidbody2D rb;
    
    [Header("ステータス表示")]
    [SerializeField] private PlayerStatus status;
    [SerializeField] private TextMeshProUGUI text;

    [Header("縦移動")]
    [SerializeField,Tooltip("レーン移動後に再びレーン移動できるようになるまでの時間")] private float laneMoveTime = 0.5f;
    private float laneMoveTimer = 0.0f;
    [SerializeField,Tooltip("レーンのY座標配列")] private float[] lanePositions;
    private int laneidx = 0; //今いるレーンのインデックス
    
    private float horizontal;
    private float vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        laneMoveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + status.Level + "   exp/requireExp: " + status.Exp + "/" + status.RequireExp;
        laneMoveTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        HorizontalMove();
        VerticalMove();
    }

    //左右移動
    void HorizontalMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        
        Vector2 velocity = new Vector2(horizontal, 0);
        rb.linearVelocity = velocity * status.MoveSpeed;
        //rb.MovePosition(rb.position + velocity * status.MoveSpeed * Time.deltaTime);
    }

    //上下移動
    //座標を切り替える方法
    //後ほどジャンプで地面をすり抜ける方法に変更する可能性あり
    void VerticalMove()
    { 
        vertical = Input.GetAxis("Vertical");
        if (vertical > 0 && laneMoveTimer >= laneMoveTime)
        {
            laneMoveTimer = 0.0f;
            laneidx++;
            if(laneidx >= lanePositions.Length) laneidx = lanePositions.Length - 1;
        }
        if (vertical < 0 && laneMoveTimer >= laneMoveTime)
        {
            laneMoveTimer = 0.0f;
            laneidx--;
            if(laneidx < 0) laneidx = 0;
        }
            
        Player.transform.position = new Vector2(Player.transform.position.x, lanePositions[laneidx]);
    }
}
