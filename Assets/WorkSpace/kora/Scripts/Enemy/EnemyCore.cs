using System;
using UnityEngine;

public interface IEnemyCore
{
    public int GetHp();
    public int GetMaxHp();
    public int GetExp();
    public void TakeDamage(int damage);
    public void Stun(float time);
    public void Slow(float time, float per);
    public void SpawnMove(float time, Vector3 vector);
}

public class EnemyCore : MonoBehaviour, IEnemyCore
{
    [SerializeField] private int maxHp = 50;
    [SerializeField,Tooltip("経験値")] private int exp = 100;
    [SerializeField] private int score = 100;

    public static event Action<int> AddExpToPlayer;
    public static event Action<int> AddScoreToPlayer;
    
    private int _hp;
    
    private EnemyController _controller;

    void Awake()
    {
        _hp = maxHp;
        _controller = GetComponent<EnemyController>();
    }

    // getter
    public int GetHp() => this._hp;
    public int GetMaxHp() => this.maxHp;
    public int GetExp() => this.exp;
    // setter
    public void SetIsRight(bool right) => _controller.SetIsRight(right);
    
    /// <summary>
    /// 移動、攻撃などがtime秒間動かない
    /// </summary>
    public void Stun(float time) {_controller.Stun(time);}
    
    /// <summary>
    /// 移動速度、発射速度などがtime秒間per(%)減少する
    /// </summary>
    public void Slow(float time, float per) {_controller.Slow(time, per);}
    
    /// <summary>
    /// vector(Local座標)に向かってtime秒で移動する
    /// </summary>
    public void SpawnMove(float time, Vector3 vector) {_controller.SpawnMove(time, vector);}

    /// <summary>
    ///  ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    ///  やられた時
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
        
        //ScoreをScoreManagerに加算する
        AddScoreToPlayer?.Invoke(score);
        //プレイヤーに経験値を加算する
        AddExpToPlayer?.Invoke(exp);
    }
}
