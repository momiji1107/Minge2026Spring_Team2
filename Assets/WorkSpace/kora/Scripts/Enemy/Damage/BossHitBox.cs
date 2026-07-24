using UnityEngine;

public class BossHitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private BossPartType type;
    [SerializeField] private EnemyController controller;

    public void TakeDamage(float damage)
    {
        //Debug.Log("Hit " + type.ToString() + ": damage=" + damage);
        controller.ReceiveDamage(damage, type);
    }
}
