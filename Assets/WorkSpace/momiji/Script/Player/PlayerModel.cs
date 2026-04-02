using System;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerAttackController attackController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HPheartView heartView;
    
    [Header("ステータス")]
    [SerializeField] private int level; //レベル
    [SerializeField] private int requireExp; //レベルアップに必要な経験値
    [SerializeField] private int exp; //経験値
    public int HPLIMIT = 10; //HPの最大値制限
    [SerializeField] private int maxHp; //現在の体力の最大値
    [SerializeField] private int hp; //現在の体力
    [SerializeField] private int attack; //攻撃力
    [SerializeField] private float moveSpeed; //移動速度
    [SerializeField] private float shootSpeed; //弾速
    [SerializeField] private float rapidFireSpeed; //連射速度(クールタイム)
    
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

    [Header("ダメージ軽減スキル・無効化設定")]
    [SerializeField] private DamageReduction damageReduction;
    [SerializeField] private DamageNegate damageNegate;
    
    [Header("Audio関係")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageClip;
    
    [Tooltip("レベルごとに増加するレベルアップに必要な経験値量")] const int RequireExpPerLevel = 100;
    
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

        // register
        EnemyCore.AddExpToPlayer += AddExp;
    }
    
    //被ダメージ
    public void Damage(int damage)
    {
        audioSource.PlayOneShot(damageClip);
        
        hp -= CalcDamage(damage);
        heartView.HPView();
        if (hp <= 0)
        {
            //死んだ時の処理
            gameManager.GameOver();
        }
    }
    
    //ダメージ計算
    public int CalcDamage(int damage)
    {
        if (!attackController.HasSkill(damageNegate.name) || !attackController.HasSkill(damageReduction.name)) return damage;
        
        if(damageReduction.IsActive)
        {
            damage = Mathf.Max(0, damage - 1);
        }

        if(damageNegate.IsActive)
        {
            damage = 0;
        }

        Debug.Log(damage + "与えられた");

        return damage;
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
        if(exp >= 300) LevelUp();
        //if(exp >= RequireExpCalc(level)) LevelUp();
    }
    
    //レベルアップするのに必要な経験値を計算して返す
    int RequireExpCalc(int lv) { return lv * RequireExpPerLevel; }
    
    //レベルアップ
    void LevelUp()
    {
        level++;
        requireExp = RequireExpCalc(level);
        exp = 0;
        upgradeManager.DisplayRandomUpgrades();
    }
    
    //HP回復
    public void HpRecovery(int plusHp)
    {
        hp += plusHp;
        if(hp > maxHp) hp = maxHp;
        heartView.HPView();
    }
    //最大HP上昇、HP上昇分回復
    public void MaxHpUp(int plusHp)
    {
        maxHp += plusHp; 
        hp += plusHp;
        heartView.HPView();
    }
    //攻撃力上昇
    public void AttackUp(int atk) { attack += atk; }
    //移動速度上昇
    public void MoveSpeedUp(float mvSpeed) { moveSpeed += mvSpeed; }
    //弾速上昇
    public void ShootSpeedUp(float shSpeed) { shootSpeed += shSpeed; }
    //連射速度上昇(クールタイム短縮)
    public void RapidFireSpeedUp(float rfSpeed) { rapidFireSpeed -= rfSpeed; }
}
