using UnityEngine;

public class EnemyHitBox : MonoBehaviour ,IDamageable
{
    [SerializeField] private EnemyController controller;

    public void TakeDamage(int damage)
    {
        controller.ReceiveDamage(damage);
    }
}
