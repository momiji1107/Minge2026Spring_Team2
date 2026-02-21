using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float _speed;
    private float _lifeTime;
    private Vector3 _direction;

    public void Init(Vector3 direction, float speed, float lifeTime)
    {
        _direction = direction;
        _speed = speed;
        _lifeTime = lifeTime;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float timer = 0f;

        while (timer < _lifeTime)
        {
            transform.position += _direction * (_speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("[Enemy Projectile] Damageを与える");
            // 処理を追記
            
            Destroy(gameObject);
        }
    }
}
