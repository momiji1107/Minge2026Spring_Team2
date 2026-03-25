using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "ScriptableObjects/Skill/Slow")]
public class Slow : EquipmentBase
{
    [Header("スロー時間と速度")]
    [SerializeField] private int slowDuration;
    [SerializeField] private int speedDown;

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

        SlowSkill(targetEnemy);
    }

    private void SlowSkill(Collider2D[] targetEnemy)
    {
        foreach (var hit in targetEnemy)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyCore>()?.Slow(slowDuration, speedDown);
            }
        }
    }
}
