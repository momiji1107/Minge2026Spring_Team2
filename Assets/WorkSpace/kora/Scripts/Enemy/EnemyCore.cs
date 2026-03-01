using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage);
}

public class EnemyCore : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHp = 1f;
    [SerializeField] private float exp = 1f;
    
    private float _hp;

    void Awake()
    {
        _hp = maxHp;
    }

    public float GetHp() => this._hp;
    public float GetMaxHp() => this.maxHp;
    public float GetExp() => this.exp;

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
