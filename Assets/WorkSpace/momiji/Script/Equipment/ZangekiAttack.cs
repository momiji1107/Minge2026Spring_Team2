using UnityEngine;

[CreateAssetMenu(fileName = "ZangekiAttack", menuName = "ScriptableObjects/BasicAttack/ZangekiAttack")]
public class ZangekiAttack : EquipmentBase
{
    [SerializeField] private GameObject zangekiPrefab;
    [SerializeField,Tooltip("攻撃範囲の中心のずれ")] private Vector2 offset = new Vector2(0.5f, 0);
    [SerializeField] private float radius = 5.0f;
    
    public ZangekiAttack()
    {
        name = "ZangekiAttack";
    }

    public override void Activate(PlayerModel model)
    {
        Vector2 pos = model.transform.position;
        float dir = model.GetDirection ? 1.0f : -1.0f;
        Vector2 center = pos + new Vector2(dir * offset.x, offset.y);
        
        //プレファブを生成
        GameObject zangeki = Instantiate(zangekiPrefab, center, Quaternion.identity);
        zangeki.gameObject.GetComponent<SpriteRenderer>().flipX = !model.GetDirection;
        Destroy(zangeki, 0.7f);
        
        //範囲内にいた敵にダメージを与える
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
