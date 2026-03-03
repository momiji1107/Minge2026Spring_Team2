using UnityEngine;

public class EnemyContactEffect : EnemyBehaviourBase
{
    [SerializeField] private bool isDamageable = true;
    [SerializeField] private float damage = 1f;
    
    readonly string PlayerTag = "Player";
    private EnemyCore _core;

    void Awake()
    {
        if (!gameObject.TryGetComponent<EnemyCore>(out _core))
        {
            Debug.Log("Not Found EnemyCore");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            OnHitPlayer(other);
        }
    }
    
    void OnHitPlayer(Collider2D other)
    {
        if (isDamageable)
        {
            Debug.Log("playerにダメージを与える");
            // playerにダメージを与える処理を追記
        }

        Destroy(gameObject);
    }
}
