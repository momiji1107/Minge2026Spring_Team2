using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyProjectileType
{
    Straight,
    Bounce,
    Rain
}

public class EnemyProjectile : MonoBehaviour
{
    [Header("弾の挙動")] [SerializeField] private EnemyProjectileType type = EnemyProjectileType.Straight;
    [Header("Hit時のダメージ")] [SerializeField] private int damage = 10;
    [Header("速度")][SerializeField] private float speed = 3f;
    [Header("弾が消えるまでの時間")][SerializeField] private float lifeTime = 10f;
    [Header("弾の向き")] [SerializeField] private Vector3 fromDirection = Vector3.up;
    
    private Vector3 _direction;
    private List<GameObject> _bounceLanes = new List<GameObject>();
    
    private readonly string _playerTag = "Player";
    
    /// <summary>
    /// 発射方向を受け取り、弾が動き出す
    /// </summary>
    public void Init(Vector3 direction)
    {
        _direction = direction.normalized;
        transform.rotation = Quaternion.FromToRotation(fromDirection, _direction);
        StartCoroutine(Move());
        
        if (SceneContext.Instance != null) _bounceLanes = SceneContext.Instance.bounceLanes;
    }

    private IEnumerator Move()
    {
        float timer = 0f;

        while (timer < lifeTime)
        {
            switch (type)
            {
                case EnemyProjectileType.Straight:
                    MoveStraight();
                    break;
                case EnemyProjectileType.Bounce:
                    MoveStraight();
                    break;
            }
            
            CheckVisible();
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }

    private void MoveStraight()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }

    private void CheckVisible()
    {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || 1 < pos.x || pos.y < 0 || 1 < pos.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag))
        { 
            Debug.Log("EnemyProjectile: " + damage + "Damageを与える");
            other.GetComponentInChildren<PlayerModel>().Damage(damage);
            Destroy(gameObject);
            OnHitPlayer();
        }

        if (type == EnemyProjectileType.Bounce)
        {
            if (_bounceLanes.Contains(other.gameObject))
            {
                Bounce();
            }
        }
    }
    
    private void OnHitPlayer()
    {
        //Debug.Log(damage + "Damageを与える");
        // 処理を追記
        
        Destroy(gameObject);
    }

    private void Bounce()
    {
        _direction.y = -_direction.y;
        _direction.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }
}
