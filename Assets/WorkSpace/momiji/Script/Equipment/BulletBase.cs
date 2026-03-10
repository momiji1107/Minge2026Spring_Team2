using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private int _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCore>()?.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
