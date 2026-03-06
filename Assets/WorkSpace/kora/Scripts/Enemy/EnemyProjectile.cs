using System.Collections;
using UnityEngine;

public enum EnemyProjectileType
{
    Straight,
    Bounce
}

public class EnemyProjectile : MonoBehaviour
{
    [Header("弾の挙動")] [SerializeField] private EnemyProjectileType type = EnemyProjectileType.Straight;
    [Header("Hit時のダメージ")] [SerializeField] private float damage = 1f;
    [Header("速度")][SerializeField] private float speed = 3f;
    [Header("弾が消えるまでの時間")][SerializeField] private float lifeTime = 20f;
    
    private Vector3 _direction;

    private readonly string _playerTag = "Player";
    private readonly string _bottomLaneTag = "BottomLane";
    
    /// <summary>
    /// 発射方向を受け取り、弾が動き出す
    /// </summary>
    public void Init(Vector3 direction)
    {
        _direction = direction.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        StartCoroutine(Move());
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
            
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void MoveStraight()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag))
        { 
            OnHitPlayer();
        }

        if (type == EnemyProjectileType.Bounce)
        {
            if (other.CompareTag(_bottomLaneTag))
            {
                Bounce();
            }
        }
    }

    private void OnHitPlayer()
    {
        Debug.Log(damage + "Damageを与える");
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
