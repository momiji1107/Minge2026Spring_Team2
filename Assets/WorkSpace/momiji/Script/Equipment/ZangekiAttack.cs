using UnityEngine;

[CreateAssetMenu(fileName = "ZangekiAttack", menuName = "ScriptableObjects/BasicAttack/ZangekiAttack")]
public class ZangekiAttack : EquipmentBase
{
    [SerializeField] private GameObject zangekiPrefab;
    [SerializeField,Tooltip("攻撃範囲の中心のずれ")] private float offset = 1.0f;
    [SerializeField] private float radius = 1.0f;
    
    public ZangekiAttack()
    {
        name = "ZangekiAttack";
    }

    public override void Activate(PlayerModel model)
    {
        Vector2 pos = model.transform.position;
        Vector2 center = pos + Vector2.right * offset;
        Instantiate(zangekiPrefab, center, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius);

        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyCore>()?.TakeDamage(model.Attack);
                Debug.Log("hit zangeki");
            }
        }
    }
}
