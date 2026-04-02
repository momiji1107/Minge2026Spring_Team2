using UnityEngine;

public class EnemyContactEffect : EnemyBehaviourBase
{
    [SerializeField] private Collider2D damageCollider;
    
    [SerializeField] private bool isDamageable = true;
    [SerializeField] private bool isDestroy = false;
    [SerializeField] private int damage = 1;
    
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
        //Debug.Log("OnTriggerEnter "  + other.name);
        if (!damageCollider.IsTouching(other)) return;
        
        if (other.CompareTag(PlayerTag))
        {
            OnHitPlayer(other);
        }
    }
    
    void OnHitPlayer(Collider2D other)
    {
        if (isDamageable)
        {
            Debug.Log(other.name +"に" + damage + "ダメージを与える");
            // playerにダメージを与える処理を追記
            other.gameObject.GetComponentInChildren<PlayerModel>().Damage(damage);
        }

        if (isDestroy)
        {
            Destroy(gameObject);
        }
    }
}
