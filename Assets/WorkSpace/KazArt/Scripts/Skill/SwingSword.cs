using UnityEngine;

[CreateAssetMenu(fileName = "SwingSword", menuName = "ScriptableObjects/Skill/SwingSword")]

public class SwingSword : EquipmentBase
{
    [Header("�����蔻��T�C�Y")]
    [SerializeField] private float radius;

    [SerializeField] private GameObject effect;

    public override void Activate(PlayerModel model)
    {
        //Debug.Log("����");

        Vector2 hitPos = model.transform.position;
        
        //init effect
        var obj = Instantiate(effect, model.transform.position, Quaternion.identity);
        if (!model.GetDirection)
        {
            var l = obj.transform.localScale;
            obj.transform.localScale = new Vector3(l.x * -1, l.y, l.z);
        }
        obj.transform.SetParent(model.transform);
        
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
