using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "ScriptableObjects/Skill/Stun")]
public class Stun : EquipmentBase
{
    [Header("スタン持続時間")]
    [SerializeField] private float duration;

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

        StunSkill(targetEnemy);
    }

    private void StunSkill(Collider2D[] targetEnemy)
    {
        foreach(var hit in targetEnemy)
        {
            if(hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyCore>()?.Stun(duration);
            }
        }
    }
}
