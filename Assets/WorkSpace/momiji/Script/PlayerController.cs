using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerStatus status;
    [SerializeField] private TextMeshProUGUI text;
    
    private float horizontal;
    private float vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + status.Level + "   exp/requireExp: " + status.Exp + "/" + status.RequireExp;
    }

    void FixedUpdate()
    {
        //左右移動
        horizontal = Input.GetAxis("Horizontal");
        
        Vector2 velocity = new Vector2(horizontal, 0).normalized;
        rb.linearVelocity = velocity * status.MoveSpeed;
        
        //上下移動
        //vertical = Input.GetAxis("Vertical");
    }
}
