using UnityEngine;

public interface IEnemyCore
{
    public float GetHp();
    public float GetMaxHp();
    public float GetExp();
    public void TakeDamage(float damage);

}

public class EnemyCore : MonoBehaviour, IEnemyCore
{
    [SerializeField] private float maxHp = 1f;
    [SerializeField] private float exp = 1f;
    
    private float _hp;

    void Awake()
    {
        _hp = maxHp;
    }

    // getter
    public float GetHp() => this._hp;
    public float GetMaxHp() => this.maxHp;
    public float GetExp() => this.exp;

    /// <summary>
    ///  ダメージを受ける
    /// </summary>
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
