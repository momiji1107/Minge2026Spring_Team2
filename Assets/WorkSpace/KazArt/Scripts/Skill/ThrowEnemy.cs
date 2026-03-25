using UnityEngine;

[CreateAssetMenu(fileName = "ThrowEnemy", menuName = "ScriptableObjects/Skill/ThrowEnemy")]
public class ThrowEnemy : EquipmentBase
{
    [Header("敵を投げる用のオブジェクト")]
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private int throwSpeed;

    [Header("当たり判定サイズ")]
    [SerializeField] private float boxWidth;
    [SerializeField] private float boxHeight;
    [SerializeField] private float offSet;

    public override void Activate(PlayerModel model)
    {
        Debug.Log("発動");

        Vector2 ownerPos = model.transform.position;
        Vector2 hitPos = ownerPos + (Vector2)model.transform.right * offSet;
        Vector2 hitSize = new Vector2(boxWidth, boxHeight);

        Collider2D[] targetEnemy = Physics2D.OverlapBoxAll(hitPos, hitSize, 0);
        Collider2D nearest = EnemyDetection(model,targetEnemy);

        ThrowAttack(model,nearest,hitPos);
    }

    private Collider2D EnemyDetection(PlayerModel model, Collider2D[] targetEnemy)
    {
        float minDist = Mathf.Infinity;
        Collider2D nearest = null;

        foreach (var hit in targetEnemy)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(model.transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = hit;
                }
            }
        }
        return nearest;
    }

    private void ThrowAttack(PlayerModel model,Collider2D nearest, Vector2 hitPos)
    {
        if (nearest != null)
        {
            Destroy(nearest.gameObject);

            var bullet = Instantiate(enemyBullet, hitPos, Quaternion.identity);
            var rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<BulletBase>()?.SetDamage(model.Attack);

            rb.AddForce(model.transform.right * throwSpeed, ForceMode2D.Impulse);
        }
    }
}
