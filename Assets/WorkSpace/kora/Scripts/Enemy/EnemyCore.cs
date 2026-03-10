using UnityEngine;

public interface IEnemyCore
{
    public int GetHp();
    public int GetMaxHp();
    public int GetExp();
    public void TakeDamage(int damage);

}

public class EnemyCore : MonoBehaviour, IEnemyCore
{
    [SerializeField] private int maxHp = 50;
    [SerializeField,Tooltip("経験値")] private int exp = 100;
    [SerializeField] private PlayerModel model;
    
    private int _hp;

    void Awake()
    {
        _hp = maxHp;
    }

    // getter
    public int GetHp() => this._hp;
    public int GetMaxHp() => this.maxHp;
    public int GetExp() => this.exp;

    /// <summary>
    ///  ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    ///  やられた時
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
        model.AddExp(exp);
    }
}
