using UnityEngine;

[CreateAssetMenu(fileName = "SwingSword", menuName = "ScriptableObjects/Skill/SwingSword")]

public class SwingSword : EquipmentBase
{
    [Header("�����蔻��T�C�Y")]
    [SerializeField] private float radius;

    public override void Activate(PlayerModel model)
    {
        //Debug.Log("����");

        Vector2 hitPos = model.transform.position;

        Collider2D[] targetEnemy = Physics2D.OverlapCircleAll(hitPos, radius);

        SwordAttack(model, targetEnemy);
    }

    private void SwordAttack(PlayerModel model,Collider2D[] targetEnemy)
    {
        foreach (var hit in targetEnemy)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyCore>()?.TakeDamage(model.Attack);
            }
        }
    }
}
