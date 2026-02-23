using UnityEngine;
using TMPro;

public class PlayerInputController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject Player;
    private Rigidbody2D rb;
    
    [Header("ステータス表示")]
    [SerializeField] private PlayerModel model;
    [SerializeField] private TextMeshProUGUI text;

    [Header("縦移動")]
    [SerializeField,Tooltip("レーン移動後に再びレーン移動できるようになるまでの時間")] private float laneMoveTime = 0.5f;
    private float laneMoveTimer = 0.0f;
    [SerializeField,Tooltip("レーンオブジェクトの配列")] private GameObject[] lanes;
    private int laneidx = 0; //今いるレーンのインデックス
    
    private float horizontal; //横入力
    private float vertical; //縦入力
    private float heightAdjust = 0.1f; //レーンからの高さ補正
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        laneMoveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + model.Level + "   exp/requireExp: " + model.Exp + "/" + model.RequireExp;
        laneMoveTimer += Time.deltaTime;

        //Shiftキーを押すとオブジェクトを反転させる
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            model.TurnAround();
            Player.transform.Rotate(0, 180, 0);
        }
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
        rb.linearVelocity = velocity * model.MoveSpeed;
    }

    //上下移動
    void VerticalMove()
    { 
        vertical = Input.GetAxis("Vertical");
        if (vertical > 0 && laneMoveTimer >= laneMoveTime)
        {
            laneMoveTimer = 0.0f;
            laneidx++;
            if(laneidx >= lanes.Length) laneidx = lanes.Length - 1;
        }
        if (vertical < 0 && laneMoveTimer >= laneMoveTime)
        {
            laneMoveTimer = 0.0f;
            laneidx--;
            if(laneidx < 0) laneidx = 0;
        }
            
        Player.transform.position = new Vector2(Player.transform.position.x, lanes[laneidx].transform.position.y + heightAdjust);
    }
}
