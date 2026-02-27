using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [Header("ステータス")]
    [SerializeField] private int level; //レベル
    [SerializeField] private int requireExp; //レベルアップに必要な経験値
    [SerializeField] private int exp; //経験値
    [SerializeField] private int maxHp; //現在の体力の最大値
    [SerializeField] private int hp; //現在の体力
    [SerializeField] private int attack; //攻撃力
    [SerializeField] private float moveSpeed; //移動速度
    [SerializeField] private float shootSpeed; //弾速
    [SerializeField] private float rapidFireSpeed; //連射速度
    
    [Header("初期ステータス")]
    [SerializeField] private int firstLevel = 1;
    [SerializeField] private int firstExp = 0;
    [SerializeField] private int firstMaxHp = 100;
    [SerializeField] private int firstAttack = 5;
    [SerializeField] private float firstMoveSpeed = 5.0f;
    [SerializeField] private float firstShootSpeed = 5.0f;
    [SerializeField] private float firstRapidFireSpeed = 5.0f;
    
    [Header("見た目")]
    [SerializeField] private SpriteRenderer sr; //キャラ画像のSpriteRenderer
    [SerializeField] private bool lookAtRight = true; //trueの時右向き
    
    [SerializeField] const int RequireExpPerLevel = 100;
    
    //getter
    public GameObject Player => player;
    public int Level => level;
    public int RequireExp => requireExp;
    public int Exp => exp;
    public int MaxHp => maxHp;
    public int Hp => hp;
    public int Attack => attack;
    public float MoveSpeed => moveSpeed;
    public float ShootSpeed => shootSpeed;
    public float RapidFireSpeed => rapidFireSpeed;
    public bool GetDirection => lookAtRight;

    void Start()
    {
        level = firstLevel;
        requireExp = RequireExpCalc(level);
        exp = firstExp;
        maxHp = firstMaxHp;
        hp = maxHp;
        attack = firstAttack;
        moveSpeed = firstMoveSpeed;
        shootSpeed = firstShootSpeed;
        rapidFireSpeed = firstRapidFireSpeed;
    }
    
    //被ダメージ
    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //死んだ時の処理
        }
    }
    
    //キャラの向きを変える
    public void TurnAround()
    {
        lookAtRight = !lookAtRight;
    }
    
    //経験値取得    
    public void AddExp(int amount)
    {
        exp += amount;
        if(exp >= RequireExpCalc(level)) LevelUp();
    }
    
    //レベルアップするのに必要な経験値を計算して返す
    int RequireExpCalc(int lv) { return lv * RequireExpPerLevel; }
    
    //レベルアップ
    void LevelUp()
    {
        level++;
        requireExp = RequireExpCalc(level);
        exp = 0;
    }
    //最大HP上昇、HP全回復
    public void MaxHpUp(int plusHp) { maxHp += plusHp; hp = maxHp; }
    //攻撃力上昇
    public void AttackUp(int atk) { attack += atk; }
    //移動速度上昇
    public void MoveSpeedUp(float mvSpeed) { moveSpeed += mvSpeed; }
    //弾速上昇
    public void ShootSpeedUp(float shSpeed) { shootSpeed += shSpeed; }
    //連射速度上昇
    public void RapidFireSpeedUp(float rfSpeed) { rapidFireSpeed += rfSpeed; }
}
