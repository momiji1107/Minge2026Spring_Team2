using UnityEngine;

public class EnemyHitBox : MonoBehaviour ,IDamageable
{
    [SerializeField] private EnemyController controller;

    public void TakeDamage(float damage)
    {
        controller.ReceiveDamage(damage);
    }
}
