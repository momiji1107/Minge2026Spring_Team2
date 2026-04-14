using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public int GetHp();
    public int GetMaxHp();
    public int GetExp();
    public void TakeDamage(int damage);
    public void Stun(float time);
    public void Slow(float time, float per);
    public void SpawnMove(float time, Vector3 vector);
}

public class EnemyCore : MonoBehaviour, IEnemy
{
    public static event Action<int> AddExpToPlayer;
    public static event Action<int> AddScoreToPlayer;

    public event Action OnDead;
    public event Action OnAttack;
    public event Action OnMove;
    
    private int _hp;
    private bool _isDead = false;
    private bool _isBoss = false;
    
    private EnemyData _data;
    private EnemyController _controller;
    private EnemyStatusManager _statusManager;
    private IEnemyStateMachine  _stateMachine;
    private List<EnemyBehaviourBase> _behaviours;

    // getter
    public int GetHp() => this._hp;
    public int GetMaxHp() => _data.maxHp;
    public int GetExp() => _data.exp;
    public bool GetIsStun() => _statusManager.IsStun;
    public bool GetIsSlow() => _statusManager.IsSlow;
    public bool GetIsBoss() => _isBoss;
    public bool GetIsDead() => this._isDead;
    public EnemyData GetData() => this._data;
    
    // setter
    public void SetIsRight(bool right)
    {
        _stateMachine.SetIsRignt(right);
        _controller.OnSetIsRight?.Invoke(right);
    }

    /// <summary>
    /// 移動、攻撃などがtime秒間動かない
    /// </summary>
    public void Stun(float time) { _statusManager.Stun(time); }
    
    /// <summary>
    /// 移動速度、発射速度などがtime秒間per(%)減少する
    /// </summary>
    public void Slow(float time, float per) { _statusManager.Slow(time, per); }
    
    /// <summary>
    /// vector(Local座標)に向かってtime秒で移動する
    /// </summary>
    public void SpawnMove(float time, Vector3 vector) {_statusManager.SpawnMove(time, vector);}

    /// <summary>
    ///  ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        //Debug.Log("TakeDamage: " + damage);
        
        if (_hp <= 0 && !_isDead)
        {
            //Debug.Log("Die: " + damage);
            _isDead = true;
            Die();
        }
    }

    public void Init(EnemyData data, EnemyController controller)
    {
        if (data == null) Debug.Log("Data is null!");
        
        _controller = controller;
        _data = data;
        _isBoss = data.isBoss;
        _hp = data.maxHp;
        
        _statusManager = new EnemyStatusManager();
        _statusManager.Init(_controller);

        if (!_isBoss) _stateMachine = new EnemyStateMachine();
        else _stateMachine = new EnemyBossStateMachine();
        
        _stateMachine.Init(data, this, _controller.Context);
    }

    public void Tick()
    {
        _statusManager.Tick();

        float dt = Time.deltaTime;
        
        if (_isDead || _statusManager.IsStun || _statusManager.IsSpawnMove) return;
        if (_statusManager.IsSlow) dt *= _statusManager.SlowPer;

        _stateMachine.Tick(dt);
    }

    public void OnHitPlayer(Collider2D other)
    {
        _stateMachine.OnHitPlayer(other);
    }
    
    public void ActiveDestroy()
    {
        _controller.Context.Destroy();
    }
    
    public void InvokeAttackAnim() {OnAttack?.Invoke();}
    public void InvokeMoveAnim() {OnMove?.Invoke();}
    
    /// <summary>
    ///  やられた時
    /// </summary>
    private void Die()
    {
        OnDead?.Invoke();
        if (OnDead == null) ActiveDestroy();
        
        //ScoreをScoreManagerに加算する
        AddScoreToPlayer?.Invoke(_data.score);
        //プレイヤーに経験値を加算する
        AddExpToPlayer?.Invoke(_data.exp);
    }
}
