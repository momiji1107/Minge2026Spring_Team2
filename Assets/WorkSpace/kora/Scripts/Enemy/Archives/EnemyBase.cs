using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの親クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    [SerializeField] protected float hp = 1;

    protected readonly string PlayerTag = "Player";

    public float Hp => hp;
    public Vector3 Position => transform.position;

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        
        if (hp <= 0) Die();
    }
    
    protected virtual void Update()
    {
        OnActive();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            OnHitPlayer(other);
        }
    }

    protected virtual void OnHitPlayer(Collider2D other)
    {
        Debug.Log("[EnemyBase] playerにダメージを与える");
        // playerにダメージを与える処理を追記
        
        Die();
    }

    /// <summary>
    /// Update内の処理
    /// </summary>
    protected virtual void OnActive() {}
}
