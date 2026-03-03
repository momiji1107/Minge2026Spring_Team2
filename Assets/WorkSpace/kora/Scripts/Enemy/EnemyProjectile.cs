using System.Collections;
using UnityEngine;

public enum EnemyProjectileType
{
    Straight
}

public class EnemyProjectile : MonoBehaviour
{
    [Header("弾の挙動")] [SerializeField] private EnemyProjectileType type = EnemyProjectileType.Straight;
    [Header("Hit時のダメージ")] [SerializeField] private float damage = 1f;
    [Header("速度")][SerializeField] private float speed = 3f;
    [Header("弾が消えるまでの時間")][SerializeField] private float lifeTime = 20f;
    
    private Vector3 _direction;

    /// <summary>
    /// 発射方向を受け取り、弾が動き出す
    /// </summary>
    public void Init(Vector3 direction)
    {
        _direction = direction;
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
                    transform.position += _direction * (speed * Time.deltaTime);
                    timer += Time.deltaTime;
                    yield return null;
                    break;
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log(damage + "Damageを与える");
            // 処理を追記
            
            Destroy(gameObject);
        }
    }
}
