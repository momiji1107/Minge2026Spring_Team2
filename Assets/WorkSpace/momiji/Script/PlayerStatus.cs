using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] private int level; //レベル
    [SerializeField] private int requireExp; //レベルアップに必要な経験値
    [SerializeField] private int exp; //経験値
    [SerializeField] private int maxHp; //現在の体力の最大値
    [SerializeField] private int hp; //現在の体力
    [SerializeField] private int attack; //攻撃力
    [SerializeField] private int moveSpeed; //移動速度
    [SerializeField] private float shootSpeed; //弾速
    
    [Header("初期ステータス")]
    [SerializeField] private int firstLevel;
    [SerializeField] private int firstExp;
    [SerializeField] private int firstMaxHp;
    [SerializeField] private int firstAttack;
    [SerializeField] private int firstMoveSpeed;
    [SerializeField] private float firstShootSpeed;
    
    //getter
    public int Level => level;
    public int RequireExp => requireExp;
    public int Exp => exp;
    public int MaxHP => maxHp;
    public int HP => hp;
    public int Attack => attack;
    public int MoveSpeed => moveSpeed;
    public float ShootSpeed => shootSpeed;

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
    
    //経験値取得    
    public void AddExp(int amount)
    {
        exp += amount;
        if(exp >= RequireExpCalc(level)) LevelUp();
    }
    //レベルアップするのに必要な経験値を計算して返す
    int RequireExpCalc(int lv) { return lv * 100; }
    //レベルアップ
    void LevelUp()
    {
        level++;
        requireExp = RequireExpCalc(level);
        exp = 0;
        maxHp += 10;
        hp = maxHp;
    }
    //攻撃力上昇
    public void AttackUp(int atk) { attack += atk; }
    //移動速度上昇
    public void MoveSpeedUp(int mvSpeed) { moveSpeed += mvSpeed; }
    //弾速上昇
    public void ShootSpeedUp(float shSpeed) { shootSpeed += shSpeed; }
}
