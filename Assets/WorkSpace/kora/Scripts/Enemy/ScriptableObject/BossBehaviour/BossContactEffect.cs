using UnityEngine;

[CreateAssetMenu(fileName = "BossContactEffect", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossContactEffectSO")]
public class BossContactEffect : BossBehaviourBaseSO
{
    [SerializeField] bool isDamageable = true;
    [SerializeField] bool isDestroy = false;
    [SerializeField] int damage = 1;

    private Collider2D _damageCollider;
    
    protected override void OnInit()
    {
        _damageCollider = Context.damageCollider;
    }

    public override void OnHitPlayer(Collider2D other)
    {
        if (!_damageCollider.IsTouching(other)) return;

        if (Core.GetIsDead()) return;

        if (isDamageable)
        {
            Debug.Log("EnemyContactEffect: " + other.name + "に" + damage + "ダメージを与える");
            // playerにダメージを与える処理を追記
            other.gameObject.GetComponentInChildren<PlayerModel>().Damage(damage);
        }

        if (isDestroy)
        {
            Context.Destroy();
        }
    }
}
